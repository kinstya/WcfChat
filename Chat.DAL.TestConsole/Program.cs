using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.DAL;
using Chat.DAL.Models;

namespace ConsoleApplication1
{
    using EFCache;

    class Program
    {
        static Program()
        {
            Program.Cache = new InMemoryCache();
            ChatDbConfiguration.Cache = Program.Cache;
        }

        private static InMemoryCache Cache { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Items in cache: {0}", Cache.Count);
            using (var db = ChatDb.GetContext())
            {
                var all = db.Conversations.ToList();
                Console.WriteLine("Currently Conversations in DB: {0}", all.Count);
                Console.WriteLine("Items in cache: {0}", Cache.Count);

                // Create and save a new Conversation 
                var conversation = new Conversation { Started = DateTime.Now };

                var participant1 = new Participant { Name = "Pete", Joined = DateTime.Now };
                var participant2 = new Participant { Name = "John", Joined = DateTime.Now };

                conversation.Participants = new List<Participant>(new[] { participant1, participant2 });
                db.Add(conversation);

                db.AddRange(new[]
                    {
                        new Message
                            {
                                Participant = participant1,
                                Time = DateTime.Now.AddMinutes(1),
                                Text = "Hi there"
                            },
                        new Message
                            {
                                Participant = participant2,
                                Time = DateTime.Now.AddMinutes(2),
                                Text = "Hi"
                            },
                    });

                db.SaveChanges();

                // Display all Conversations from the database 
                var query = 
                    from c in db.Conversations
                            join p in db.Participants.Include(m => m.Messages) on c.ConversationId equals p.ConversationId into parties
                            orderby c.Started descending 
                            select new
                            {
                                Conversation = c,
                                Messages = from p in parties
                                           from mes in p.Messages
                                           orderby mes.Time
                                           select mes
                            };

                Console.WriteLine("All Conversations in the database:");
                int totalItems =0;
                foreach (var item in query)
                {
                    Console.WriteLine("ID: {0} Started: {1} Ended: {2} ", item.Conversation.ConversationId, item.Conversation.Started, item.Conversation.Ended);
                    foreach (var message in item.Messages)
                    {
                        totalItems++;
                        Console.WriteLine("  {0} [{1}]: {2}", message.Participant.Name, message.Time, message.Text);
                    }
                }

                Console.WriteLine("Deleting the newly added conversation...");
                db.Delete<Conversation>(conversation.ID);
                Console.WriteLine("Items in cache: {0}", Cache.Count);
                db.SaveChanges();
            }

            Console.WriteLine("Db context disposed.");
            Console.WriteLine("Items in cache: {0}", Cache.Count);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

            using (var db = ChatDb.GetContext())
            {
                var all = db.Conversations.ToList();
                Console.WriteLine("Currently Conversations in DB: {0}", all.Count);
                Console.WriteLine("Items in cache: {0}", Cache.Count);
                int totalItems = 0;
                var query =
                    from c in db.Conversations
                    join p in db.Participants.Include(m => m.Messages) on c.ConversationId equals p.ConversationId into parties
                    orderby c.Started descending
                    select new
                    {
                        Conversation = c,
                        Messages = from p in parties
                                   from mes in p.Messages
                                   orderby mes.Time
                                   select mes
                    };
                // Display all Conversations from the database 
                foreach (var item in query)
                {
                    Console.WriteLine("ID: {0} Started: {1} Ended: {2} ", item.Conversation.ConversationId, item.Conversation.Started, item.Conversation.Ended);
                    foreach (var message in item.Messages)
                    {
                        totalItems++;
                        Console.WriteLine("  {0} [{1}]: {2}", message.Participant.Name, message.Time, message.Text);
                    }
                }
            }
            Console.WriteLine("Db context disposed.");
            Console.WriteLine("Items in cache: {0}", Cache.Count);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

        }
    }
}
