// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CallbackEvent.cs" company="">
//   
// </copyright>
// <summary>
//   The callback event.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Chat.Client
{
    /// <summary>
    /// The callback event.
    /// </summary>
    public enum CallbackEvent
    {
        /// <summary>
        /// The refresh clients.
        /// </summary>
        RefreshClients, 

        /// <summary>
        /// The receive.
        /// </summary>
        Receive, 

        /// <summary>
        /// The receive whisper.
        /// </summary>
        ReceiveWhisper, 

        /// <summary>
        /// The is writing callback.
        /// </summary>
        IsWritingCallback, 

        /// <summary>
        /// The user join.
        /// </summary>
        UserJoin, 

        /// <summary>
        /// The user leave.
        /// </summary>
        UserLeave
    }
}