namespace Chat.ConsoleClient
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Chat.Client;
    using Chat.Client.IISChat;

    public class ConsoleClient
    {
        private readonly ChatCallbackAgent serviceCallback;

        private readonly ChatServiceAgent serviceAgent;

        private Client client;

        static void Main(string[] args)
        {
            new ConsoleClient().Run();
        }

        private ConsoleClient()
        {
            this.serviceCallback = new ChatCallbackAgent(this.HandleCallback);
            this.serviceAgent = new ChatServiceAgent(this.serviceCallback, this.HandleServiceStateChanged, this.HandleConnectCompleted);
        }

        private void Run()
        {
            Console.WriteLine("Welcome to chat!");
            string input;
            do
            {
                Console.WriteLine("Please enter:");
                Console.WriteLine("\t[Quit] to quit");
                Console.WriteLine("\t[CONNECT host \"your nick\"] to connect");
                Console.WriteLine("\t[SAY message] to say");
                Console.WriteLine("\t[Leave] to leave");
                input = this.AskUser();
            }
            while (this.ProcessInput(input));

            this.Leave();
            Console.ReadLine();
        }

        #region UI =)

        private string AskUser()
        {
            return Console.ReadLine();
        }

        private bool ProcessInput(string input)
        {
            var command = input.Trim();
            const string connectPattern = @"^C(ONNECT)*\s(?<host>[A-Z\.0-9]+)\s(?<nick>.+)$";
            const string leavePattern = @"^L(EAVE)*(\s)*$";
            const string sayPattern = @"^S(AY)*\s(?<message>.+)$";
            const string quitPattern = @"^Q(UIT)*\s*$";

            if (string.IsNullOrWhiteSpace(command))
            {
                return true;
            }

            GroupCollection matches;
            if (ParseCommand(command, quitPattern, out matches))
            {
                return false;
            }
            else if (ParseCommand(command, connectPattern, out matches))
            {
                this.Connect(matches["host"].Value, matches["nick"].Value);
                return true;
            }
            else if (ParseCommand(command, leavePattern, out matches))
            {
                this.Leave();
            }
            else if (ParseCommand(command, sayPattern, out matches))
            {
                this.Send(matches["message"].Value);
            }
            else
            {
                Console.WriteLine("Unrecognized input. Please try again...");
            }

            return true;
        }

        private static bool ParseCommand(string command, string sayPattern, out GroupCollection captures)
        {
            captures = null;
            
            var match = Regex.Match(command, sayPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (match.Success)
            {
                captures = match.Groups;
                return true;
            }

            return false;
        }

        #endregion UI =)

        #region Chatting

        private void Connect(string host, string name)
        {
            if (!this.serviceAgent.IsOnline())
            {
                try
                {
                    this.client = new Client
                    {
                        Name = name,
                        Time = DateTime.Now
                    };

                    this.serviceAgent.Connect(host, this.client);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLineAsync(string.Format("Error occured during connect: {0}", ex.Message));
                    Console.WriteLine(string.Format("Error occured during connect: {0}", ex.Message));
                }
            }
            else
            {
                this.CheckServiceState();
            }
        }

        private void Send(string content)
        {
            if (this.serviceAgent.IsOnline())
            {
                this.serviceAgent.IsWriting(this.client);

                this.serviceAgent.Say(new Message { Sender = this.client.Name, Content = content, Time = DateTime.Now });

                //Tell the service to tell back all clients that this client
                //has just finished typing..
                this.serviceAgent.IsWriting(null);
            }
            else
            {
                this.CheckServiceState();
            }
        }

        private void Leave()
        {
            if (this.serviceAgent.IsOnline())
            {
                Console.WriteLine("Disconnecting...");
                this.serviceAgent.DisconnectAsync(this.client);
            }
            else
            {
                this.CheckServiceState();
            }

        }

        #endregion Chat service methods

        #region Chat service callbacks and handlers

        private void HandleConnectCompleted(OperationResult result, Exception exception)
        {
            if (exception != null)
            {
                Console.Error.WriteLine("Failed to connect. Error:{0}", exception.Message);
            }
            else if (result.Ok)
            {
                Console.WriteLine("Connected!");
            }
            else
            {
                Console.WriteLine("Duplicate name found");
            }
        }

        private void HandleServiceStateChanged()
        {
            this.CheckServiceState();
        }

        private void HandleCallback(CallbackInfo info)
        {
            switch (info.Event)
            {
                case CallbackEvent.RefreshClients:
                    Console.WriteLine("Chat participant list refreshed:");
                    info.Clients.ForEach(c => Console.WriteLine("\t{0}", c.Name));
                    break;
                case CallbackEvent.Receive:
                    Console.WriteLine("{0}: {1}", info.Message.Sender, info.Message.Content);
                    break;
                case CallbackEvent.ReceiveWhisper:
                    Console.WriteLine("{0} whispered me: {1}", info.Clients.Single().Name, info.Message.Content);
                    break;
                case CallbackEvent.IsWritingCallback:
                    if (info.Clients.Count != 0 && info.Clients.Single() != null)
                    {
                        Console.WriteLine("{0} is writing now...", info.Clients.Single().Name);
                    }

                    break;
                case CallbackEvent.UserJoin:
                    Console.WriteLine("{0} has just joined the chat.", info.Clients.Single().Name);
                    break;
                case CallbackEvent.UserLeave:
                    Console.WriteLine("{0} has just left the chat.", info.Clients.Single().Name);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CheckServiceState()
        {
            if (this.serviceAgent.IsOnline())
            {
                Console.Out.WriteLineAsync("....Connected....");
            }
            else
            {
                Console.Out.WriteLineAsync("....Connection lost....");
            }

        }
        
        #endregion
    }
}
