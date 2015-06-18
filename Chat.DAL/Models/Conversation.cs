using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat.DAL.Models
{
    /// <summary>
    /// The conversation.
    /// </summary>
    public class Conversation : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Conversation"/> class.
        /// </summary>
        public Conversation()
        {
            //this.Participants = new List<Participant>();
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [NotMapped]
        public int ID {
            get { return this.ConversationId; }
            set { this.ConversationId = value;  } 
        }

        public int ConversationId { get; set; }

        /// <summary>
        /// Gets or sets the time when it started.
        /// </summary>
        public DateTime Started { get; set; }

        /// <summary>
        /// Gets or sets the time when it ended.
        /// </summary>
        public DateTime? Ended { get; set; }

        /// <summary>
        /// Gets or sets the participants.
        /// </summary>
        public virtual List<Participant> Participants { get; set; }
    }
}
