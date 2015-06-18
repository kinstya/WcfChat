// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChatServiceAgent.cs" company="">
//   
// </copyright>
// <summary>
//   The chat service agent.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Chat.Client
{
    using System;
    using System.ServiceModel;

    using Chat.Client.IISChat;

    using Microsoft.Practices.Unity.Utility;

    using WCF.CustomSerialization;

    /// <summary>
    ///     The chat service agent.
    /// </summary>
    public class ChatServiceAgent
    {
        #region Fields

        /// <summary>
        ///     The callback.
        /// </summary>
        private readonly IChatCallback callback;

        /// <summary>
        ///     The proxy.
        /// </summary>
        private ChatClient proxy;

        private Action onStateChanged;

        private Action<OperationResult, Exception> onConnectedAction;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatServiceAgent"/> class.
        /// </summary>
        /// <param name="callback">
        /// The callback.
        /// </param>
        public ChatServiceAgent(
            IChatCallback callback,
            Action onStateChanged = null,
            Action<OperationResult, Exception> onConnectedAction = null)
        {
            Guard.ArgumentNotNull(callback, "callback");
            this.callback = callback;

            this.onConnectedAction = onConnectedAction;
            this.onStateChanged = onStateChanged;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The connect.
        /// </summary>
        /// <param name="hostName">
        /// The host Name.
        /// </param>
        /// <param name="client">
        /// The local client.
        /// </param>
        /// <param name="onStateChanged">
        /// State changed action.
        /// </param>
        /// <param name="onConnectedAction">
        /// The on Connected Action.
        /// </param>
        public void Connect(
            string hostName, 
            Client client)
        {
            this.proxy = new ChatClient(new InstanceContext(this.callback));

            // As the address in the configuration file is set to localhost
            // we want to change it so we can call a service in internal 
            // network, or over internet
            string servicePath = this.proxy.Endpoint.ListenUri.AbsolutePath;
            string serviceListenPort = this.proxy.Endpoint.Address.Uri.Port.ToString();

            this.proxy.Endpoint.Address =
                new EndpointAddress("net.tcp://" + hostName + ":" + serviceListenPort + servicePath);
            this.proxy.Endpoint.Contract.Behaviors.Add(new JsonSerializerContractBehaviorAttribute());

            this.proxy.Open();

            if (this.onStateChanged != null)
            {
                this.proxy.InnerDuplexChannel.Faulted += (sender, args) => this.onStateChanged();
                this.proxy.InnerDuplexChannel.Opened += (sender, args) => this.onStateChanged();
                this.proxy.InnerDuplexChannel.Closed += (sender, args) => this.onStateChanged();
            }

            this.proxy.ConnectAsync(client);

            if (this.onConnectedAction != null)
            {
                this.proxy.ConnectCompleted += (sender, args) => this.onConnectedAction(args.Result, args.Error);
            }
        }

        /// <summary>
        /// The disconnect.
        /// </summary>
        /// <param name="client">
        /// The client.
        /// </param>
        public void Disconnect(Client client)
        {
            this.SafeExecute(p => p.Disconnect(client));
        }

        /// <summary>
        /// The disconnect async.
        /// </summary>
        /// <param name="client">
        /// The client.
        /// </param>
        public void DisconnectAsync(Client client)
        {
            this.SafeExecute(p => p.DisconnectAsync(client));
        }

        /// <summary>
        /// The is online.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsOnline()
        {
            return this.HandleState();
        }

        /// <summary>
        /// The is writing.
        /// </summary>
        /// <param name="client">
        /// The client.
        /// </param>
        public void IsWriting(Client client)
        {
            this.SafeExecute(p => p.IsWriting(client));
        }

        /// <summary>
        /// The is writing async.
        /// </summary>
        /// <param name="client">
        /// The client.
        /// </param>
        public void IsWritingAsync(Client client)
        {
            this.SafeExecute(p => p.IsWritingAsync(client));
        }

        /// <summary>
        /// The say.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        public void Say(Message msg)
        {
            this.SafeExecute(p => p.Say(msg));
        }

        /// <summary>
        /// The whisper.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        /// <param name="receiver">
        /// The receiver.
        /// </param>
        public void Whisper(Message msg, Client receiver)
        {
            this.SafeExecute(p => p.Whisper(msg, receiver));
        }

        #endregion

        #region Methods

        /// <summary>
        /// The handle state.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool HandleState()
        {
            if (this.proxy != null)
            {
                switch (this.proxy.State)
                {
                    case CommunicationState.Closed:
                        this.proxy = null;
                        break;
                    case CommunicationState.Closing:
                        break;
                    case CommunicationState.Created:
                        break;
                    case CommunicationState.Faulted:
                        this.proxy.Abort();
                        this.proxy = null;
                        break;
                    case CommunicationState.Opened:
                        break;
                    case CommunicationState.Opening:
                        break;
                    default:
                        break;
                }
            }
            return this.proxy != null;
        }

        /// <summary>
        /// The safe execute.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        private void SafeExecute(Action<ChatClient> action)
        {
            if (this.HandleState())
            {
                action.Invoke(this.proxy);
            }
        }

        #endregion
    }
}