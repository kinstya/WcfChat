namespace Chat.DAL
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;

    using Chat.DAL.Info;
    using Chat.DAL.Models;

    public class ChatDataAccess : IChatDataAccess
    {
        private ConcurrentDictionary<string, Participant> Participants = new ConcurrentDictionary<string, Participant>();

        private ChatDataAccess()
        {
        }

        public static IChatDataAccess Create()
        {
            return new ChatDataAccess();
        }

        public void AddMessage(MessageInfo msg, int participantId)
        {
            //Participant participant;
            //if (this.Participants.TryGetValue(msg.Sender, out participant))
            //{
                using (var db = ChatDb.GetContext())
                {
                    db.Add(
                        new Chat.DAL.Models.Message
                            {
                                ParticipantId = participantId,
                                Text = msg.Content,
                                Time = msg.Time
                            });
                    db.SaveChanges();
                }
            //}
        }

        public int AddParticipant(ClientInfo clientInfo, int conversationId)
        {
            //this.Participants.TryAdd(client.Name, participant);

            using (var db = ChatDb.GetContext())
            {
                var p = new Participant { ConversationId = conversationId, Joined = clientInfo.Time, Name = clientInfo.Name };
                db.Add(p);
                db.SaveChanges();
                return p.ID;
            }
        }

        public int StartConversation(DateTime time)
        {
            using (var db = ChatDb.GetContext())
            {
                var conversation = new Conversation() { Started = time };
                db.Add(conversation);
                db.SaveChanges();
                return conversation.ConversationId;
            }
        }

        public ClientInfo GetParticipantByName(int conversationId, string name)
        {
            using (var db = ChatDb.GetContext())
            {
                var found = db.Query<Conversation>()
                    .Where(c => c.ConversationId == conversationId)
                    .SelectMany(c => c.Participants)
                    .FirstOrDefault(p => p.Name == name);
                return found != null ? new ClientInfo
                                           {
                                               Name = found.Name,
                                               Time = found.Joined,
                                           } : null;
            }
        }

        public void EndConversation(int conversationId)
        {
            using (var db = ChatDb.GetContext())
            {
                var conversation = db.Query<Conversation>().First(c => c.ConversationId == conversationId);
                conversation.Ended = DateTime.Now;
                db.SaveChanges();
            }
        }

        public void ParticipantLeft(int participantId)
        {
            Participant participant;
            //if (this.Participants.TryRemove(client.Name, out participant))
            //{
                using (var db = ChatDb.GetContext())
                {
                    var p2 = db.Participants.First(p => p.ParticipantId == participantId);
                    p2.Left = DateTime.Now;
                    db.SaveChanges();
                }
            //}
        }

    }
}
