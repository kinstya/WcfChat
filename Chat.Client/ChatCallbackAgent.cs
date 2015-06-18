// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChatCallbackAgent.cs" company="">
//   
// </copyright>
// <summary>
//   The chat service agent.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Chat.Client
{
    using System;
    using System.Collections.Generic;

    using Chat.Client.IISChat;

    using Microsoft.Practices.Unity.Utility;

    /// <summary>
    ///     The chat callback agent.
    /// </summary>
    public class ChatCallbackAgent : IChatCallback
    {
        #region Fields

        /// <summary>
        ///     The callback handler.
        /// </summary>
        private readonly Action<CallbackInfo> callbackHandler;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatCallbackAgent"/> class.
        /// </summary>
        /// <param name="callbackHandler">
        /// The callback Handler.
        /// </param>
        public ChatCallbackAgent(Action<CallbackInfo> callbackHandler)
        {
            Guard.ArgumentNotNull(callbackHandler, "callbackHandler");
            this.callbackHandler = callbackHandler;
        }

        #endregion

        #region Explicit Interface Methods

        /// <summary>
        /// The begin is writing callback.
        /// </summary>
        /// <param name="client">
        /// The client.
        /// </param>
        /// <param name="callback">
        /// The callback.
        /// </param>
        /// <param name="asyncState">
        /// The async state.
        /// </param>
        /// <returns>
        /// The <see cref="IAsyncResult"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        IAsyncResult IChatCallback.BeginIsWritingCallback(Client client, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The begin receive.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        /// <param name="callback">
        /// The callback.
        /// </param>
        /// <param name="asyncState">
        /// The async state.
        /// </param>
        /// <returns>
        /// The <see cref="IAsyncResult"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        IAsyncResult IChatCallback.BeginReceive(Message msg, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The begin receive whisper.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        /// <param name="receiver">
        /// The receiver.
        /// </param>
        /// <param name="callback">
        /// The callback.
        /// </param>
        /// <param name="asyncState">
        /// The async state.
        /// </param>
        /// <returns>
        /// The <see cref="IAsyncResult"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        IAsyncResult IChatCallback.BeginReceiveWhisper(
            Message msg, 
            Client receiver, 
            AsyncCallback callback, 
            object asyncState)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The begin refresh clients.
        /// </summary>
        /// <param name="clients">
        /// The clients.
        /// </param>
        /// <param name="callback">
        /// The callback.
        /// </param>
        /// <param name="asyncState">
        /// The async state.
        /// </param>
        /// <returns>
        /// The <see cref="IAsyncResult"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        IAsyncResult IChatCallback.BeginRefreshClients(List<Client> clients, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The begin user join.
        /// </summary>
        /// <param name="client">
        /// The client.
        /// </param>
        /// <param name="callback">
        /// The callback.
        /// </param>
        /// <param name="asyncState">
        /// The async state.
        /// </param>
        /// <returns>
        /// The <see cref="IAsyncResult"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        IAsyncResult IChatCallback.BeginUserJoin(Client client, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The begin user leave.
        /// </summary>
        /// <param name="client">
        /// The client.
        /// </param>
        /// <param name="callback">
        /// The callback.
        /// </param>
        /// <param name="asyncState">
        /// The async state.
        /// </param>
        /// <returns>
        /// The <see cref="IAsyncResult"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        IAsyncResult IChatCallback.BeginUserLeave(Client client, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The end is writing callback.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        void IChatCallback.EndIsWritingCallback(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The end receive.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        void IChatCallback.EndReceive(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The end receive whisper.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        void IChatCallback.EndReceiveWhisper(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The end refresh clients.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        void IChatCallback.EndRefreshClients(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The end user join.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        void IChatCallback.EndUserJoin(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The end user leave.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        void IChatCallback.EndUserLeave(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The is writing callback.
        /// </summary>
        /// <param name="client">
        /// The client.
        /// </param>
        void IChatCallback.IsWritingCallback(Client client)
        {
            this.callbackHandler(
                new CallbackInfo
                    {
                        Event = CallbackEvent.IsWritingCallback, 
                        Clients = new List<Client>(new[] { client })
                    });
        }

        /// <summary>
        /// The receive.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        void IChatCallback.Receive(Message msg)
        {
            this.callbackHandler(new CallbackInfo { Event = CallbackEvent.Receive, Message = msg });
        }

        /// <summary>
        /// The receive whisper.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        /// <param name="receiver">
        /// The receiver.
        /// </param>
        void IChatCallback.ReceiveWhisper(Message msg, Client receiver)
        {
            this.callbackHandler(
                new CallbackInfo
                    {
                        Event = CallbackEvent.ReceiveWhisper, 
                        Message = msg, 
                        Clients = new List<Client>(new[] { receiver })
                    });
        }

        /// <summary>
        /// The refresh clients.
        /// </summary>
        /// <param name="clients">
        /// The clients.
        /// </param>
        void IChatCallback.RefreshClients(List<Client> clients)
        {
            this.callbackHandler(new CallbackInfo { Event = CallbackEvent.RefreshClients, Clients = clients });
        }

        /// <summary>
        /// The user join.
        /// </summary>
        /// <param name="client">
        /// The client.
        /// </param>
        void IChatCallback.UserJoin(Client client)
        {
            this.callbackHandler(
                new CallbackInfo { Event = CallbackEvent.UserJoin, Clients = new List<Client>(new[] { client }) });
        }

        /// <summary>
        /// The user leave.
        /// </summary>
        /// <param name="client">
        /// The client.
        /// </param>
        void IChatCallback.UserLeave(Client client)
        {
            this.callbackHandler(
                new CallbackInfo { Event = CallbackEvent.UserLeave, Clients = new List<Client>(new[] { client }) });
        }

        #endregion
    }
}