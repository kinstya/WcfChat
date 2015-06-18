using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat.DAL.Models
{
    public class Message : IEntity
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [NotMapped]
        public int ID
        {
            get { return this.MessageId; }
            set { this.MessageId = value; }
        }

        public int MessageId { get; set; }

        public int ParticipantId { get; set; }

        public string Text { get; set; }

        public DateTime Time { get; set; }

        public virtual Participant Participant { get; set; }
    }
}