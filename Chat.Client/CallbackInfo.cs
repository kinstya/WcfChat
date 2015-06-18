// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CallbackInfo.cs" company="">
//   
// </copyright>
// <summary>
//   The callback info.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Chat.Client
{
    using System.Collections.Generic;

    using Chat.Client.IISChat;

    /// <summary>
    /// The callback info.
    /// </summary>
    public class CallbackInfo
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the clients.
        /// </summary>
        public List<Chat.Client.IISChat.Client> Clients { get; set; }

        /// <summary>
        /// Gets or sets the event.
        /// </summary>
        public CallbackEvent Event { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public Message Message { get; set; }

        #endregion
    }
}