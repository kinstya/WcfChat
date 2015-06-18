namespace Chat.DAL.Info
{
    using System;

    /// <summary>
    /// The message info.
    /// </summary>
    public class MessageInfo
    {
        public string Content { get; set; }

        public string Sender { get; set; }

        public DateTime Time { get; set; }
    }
}