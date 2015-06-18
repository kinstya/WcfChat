using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Chat.DAL.Models;
using Microsoft.Practices.Unity.Utility;

namespace Chat.DAL
{
    using System;

    public class ChatDb : DbContext, IChatContext
    {
        static ChatDb()
        {
             System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<ChatDb>());
        }
        
        private ChatDb()
        {}

        public static IChatContext GetContext()
        {
            var chatDb = new ChatDb();
            chatDb.Configuration.AutoDetectChangesEnabled = false;
            chatDb.Configuration.ValidateOnSaveEnabled = false;
            return chatDb;
        }

        public DbSet<Conversation> Conversations { get; set; }

        public DbSet<Participant> Participants { get; set; }

        public DbSet<Message> Messages { get; set; }


        #region IChatContext explicit implementation
        IQueryable<Conversation> IChatContext.Conversations
        {
            get
            {
                return this.Conversations;
            }
        }
        
        IQueryable<Participant> IChatContext.Participants
        {
            get
            {
                return this.Participants;
            }
        }


        IQueryable<Message> IChatContext.Messages
        {
            get
            {
                return this.Messages;
            }
        }
        
        /// <inheritdoc/>
        void IChatContext.SaveChanges()
        {
            this.ChangeTracker.DetectChanges();
            this.SaveChanges();
        }
        #endregion
        
        public IQueryable<T> Query<T>() where T : class, IEntity
        {
           return this.Set<T>();
        }

        /// <inheritdoc/>
        public void Add<T>(T entity) where T : class, IEntity
        {
            Guard.ArgumentNotNull(entity, "entity");
            this.Set<T>().Add(entity);
        }

        /// <inheritdoc/>
        public void Delete<T>(int ID) where T : class, IEntity
        {
            var saved = this.Set<T>().Find(ID);

            this.Set<T>().Remove(saved);
        }

        /// <inheritdoc/>
        public void AddRange<T>(IEnumerable<T> entities) where T : class, IEntity
        {
            Guard.ArgumentNotNull(entities, "entities");

            foreach (T entity in entities)
            {
                this.Set<T>().Add(entity);
            }
        }

        /// <inheritdoc/>
        public void RemoveRange<T>(IEnumerable<T> entities) where T : class, IEntity
        {
            Guard.ArgumentNotNull(entities, "entities");

            foreach (T entity in entities)
            {
                this.Set<T>().Remove(entity);
            }
        }
    }
}
