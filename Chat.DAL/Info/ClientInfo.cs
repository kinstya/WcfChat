namespace Chat.DAL.Info
{
    using System;

    /// <summary>
    /// The client info.
    /// </summary>
    public class ClientInfo
    {
        public string Name { get; set; }

        public DateTime Time { get; set; }

        public int ParticipantId { get; set; }
    }
}