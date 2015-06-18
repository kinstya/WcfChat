// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChatService.cs" company="">
//   
// </copyright>
// <summary>
//   The chat service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Chat.Service
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.ServiceModel;

    using Chat.DAL;
    using Chat.DAL.Info;
    using Chat.Service.Contract;

    using Microsoft.Practices.Unity.Utility;

    using Message = Chat.Service.Contract.Message;

    /// <summary>
    /// The chat service.
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class ChatService : IChat
    {
        #region Fields
        private readonly ConcurrentDictionary<IChatCallback, ClientInfo> clients = new ConcurrentDictionary<IChatCallback, ClientInfo>();

        private readonly object syncObj = new object();
        private int conversationId;

        private IChatDataAccess dal = ChatDataAccess.Create();

        #endregion

        #region Properties
        /// <summary>
        /// Gets the current callback.
        /// </summary>
        public IChatCallback CurrentCallback
        {
            get { return OperationContext.Current.GetCallbackChannel<IChatCallback>(); }
        }

        #endregion

        #region IChat Members

        /// <inheritdoc/>
        public OperationResult Connect(Client client)
        {
            Guard.ArgumentNotNull(client, "client");
            IChatCallback clientCallback;
            if (this.ResolveClient(client.Name, out clientCallback))
            {
                return new OperationResult { Ok = false };
            }

            lock (this.syncObj)
            {
                this.Checkin(client);

                var connected = this.clients.Values.Select(ci => new Client{ Name = ci.Name, Time = ci.Time }).ToList();
                this.Notify(
                    (callback) =>
                        {
                            callback.RefreshClients(connected);
                            callback.UserJoin(client);
                        });
            }

            return new OperationResult { Ok = true };
        }

        /// <inheritdoc/>
        public void Say(Message msg)
        {
            Guard.ArgumentNotNull(msg, "msg");
            IChatCallback clientCallback;
            if (!this.ResolveClient(msg.Sender, out clientCallback))
            {
                throw new FaultException(new FaultReason(string.Format("Unauthorized sender: {0}", msg.Sender)));
            }

            lock (this.syncObj)
            {
                this.dal.AddMessage(msg.ToInfo(), this.clients[clientCallback].ParticipantId);
                this.Notify(
                    (callback) =>
                        {
                            callback.Receive(msg);
                        });
            }
        }

        /// <inheritdoc/>
        public void Whisper(Message msg, Client receiver)
        {
            Guard.ArgumentNotNull(msg, "msg");
            Guard.ArgumentNotNull(receiver, "receiver");
            IChatCallback senderCallback;
            if (!this.ResolveClient(msg.Sender, out senderCallback))
            {
                throw new FaultException(new FaultReason(string.Format("Unauthorized sender: {0}", msg.Sender)));
            }

            ClientInfo receiverInfo;
            IChatCallback receiverCallback;
            if (!this.ResolveReceiver(receiver.Name, out receiverInfo, out receiverCallback))
            {
                throw new FaultException(new FaultReason(string.Format("Unauthorized receiver: {0}", receiver.Name)));
            }

            receiverCallback.ReceiveWhisper(msg, receiver);
            senderCallback.ReceiveWhisper(msg, receiver);
        }

        /// <inheritdoc/>
        public void IsWriting(Client client)
        {
            lock (this.syncObj)
            {
                this.Notify(
                    (callback) => callback.IsWritingCallback(client));
            }
        }

        /// <inheritdoc/>
        public void Disconnect(Client client)
        {
            Guard.ArgumentNotNull(client, "client");
            IChatCallback cb;
            if (!this.ResolveClient(client.Name, out cb))
            {
                throw new FaultException(new FaultReason(string.Format("Unauthorized sender: {0}", client.Name)));
            }

            lock (this.syncObj)
            {
                this.Checkout(cb);
                var connected = this.clients.Values.Select(ci => new Client { Name = ci.Name, Time = ci.Time }).ToList();
                this.Notify(
                    (callback) => 
                        {
                            callback.RefreshClients(connected);
                            callback.UserLeave(client);
                        });
            }
        }

        #endregion

        #region Machinery

        private void Notify(Action<IChatCallback> action)
        {
            var astalavista = new List<IChatCallback>();

            foreach (var callback in this.clients.Keys)
            {
                try
                {
                    action.Invoke(callback);
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.ToString());
                    astalavista.Add(callback);
                }
            }

            foreach (var info in astalavista)
            {
                this.Checkout(info);
            }
        }

        private void Checkout(IChatCallback callback)
        {
            ClientInfo clientInfo = null;
            if (this.clients.TryRemove(callback, out clientInfo))
            {
                this.dal.ParticipantLeft(clientInfo.ParticipantId);
                if (this.clients.Count == 0)
                {
                    this.dal.EndConversation(this.conversationId);
                    this.conversationId = -1;
                }
            }
        }

        private void Checkin(Client client)
        {
            if (this.clients.Count == 0)
            {
                this.conversationId = this.dal.StartConversation(client.Time);
            }

            var clientInfo = client.ToInfo();
            clientInfo.ParticipantId = this.dal.AddParticipant(clientInfo, this.conversationId);
            this.clients.TryAdd(this.CurrentCallback, clientInfo);
        }

        private bool ResolveClient(string name, out IChatCallback callback)
        {
            callback = this.CurrentCallback;
            ClientInfo clientInfo;
            if (this.clients.TryGetValue(this.CurrentCallback, out clientInfo))
            {
                return clientInfo.Name == name;
            }

            return false;
        }

        private bool ResolveReceiver(string name, out ClientInfo receiverInfo, out IChatCallback callback)
        {
            receiverInfo = null;
            callback = null;
            var receiver = this.clients.FirstOrDefault(link => link.Value.Name == name);
            if (receiver.Key != null)
            {
                receiverInfo = receiver.Value;
                callback = receiver.Key;
                return true;
            }
            return true;
        }

        #endregion    
    }
}