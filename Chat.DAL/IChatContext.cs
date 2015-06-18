// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMigrationContext.cs" company="Intermedia">
//   Copyright © Intermedia.net, Inc. 1995 - 2013. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Chat.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chat.DAL
{
    /// <summary>
    /// The Chat db context interface.
    /// </summary>
    public interface IChatContext : IDisposable
    {
        /// <summary>
        /// Gets the Conversations.
        /// </summary>
        IQueryable<Conversation> Conversations { get; }

        IQueryable<Participant> Participants { get; }
        IQueryable<Message> Messages { get; }

        /// <summary>
        /// Save changes.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Add entity.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <typeparam name="T">
        /// The entity type.
        /// </typeparam>
        void Add<T>(T entity) where T : class, IEntity;

        /// <summary>
        /// Delete entity.
        /// </summary>
        /// <param name="ID">
        /// The entity ID.
        /// </param>
        /// <typeparam name="T">
        /// The entity type.
        /// </typeparam>
        void Delete<T>(int ID) where T : class, IEntity;

        /// <summary>
        /// Add range of entities.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        void AddRange<T>(IEnumerable<T> entities) where T : class, IEntity;

        /// <summary>
        /// Remove range of entities.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        void RemoveRange<T>(IEnumerable<T> entities) where T : class, IEntity;

        IQueryable<T> Query<T>() where T : class, IEntity;
    }
}
