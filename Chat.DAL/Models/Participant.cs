using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat.DAL.Models
{
    public class Participant : IEntity
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [NotMapped]
        public int ID
        {
            get { return this.ParticipantId; }
            set { this.ParticipantId = value; }
        }

        public int ParticipantId { get; set; }

        public string Name { get; set; }

        public DateTime Joined { get; set; }

        public DateTime? Left { get; set; }

        public int ConversationId { get; set; }
        
        public virtual Conversation Conversation { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}