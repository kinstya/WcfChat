// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryableExtensions.cs" company="Intermedia">
//   Copyright © Intermedia.net, Inc. 1995 - 2013. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;

namespace Chat.DAL
{
    /// <summary>
    /// The queryable extensions.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// The includer.
        /// </summary>
        internal static IIncluder Includer = new NullIncluder();

        /// <summary>
        /// Include related entities by specifying the expression as a path to reach them.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public static IQueryable<T> Include<T, TProperty>(this IQueryable<T> source, Expression<Func<T, TProperty>> path)
            where T : class
        {
            return Includer.Include(source, path);
        }

        /// <summary>
        /// The Includer interface.
        /// </summary>
        public interface IIncluder
        {
            /// <summary>
            /// The include.
            /// </summary>
            /// <param name="source">
            /// The source.
            /// </param>
            /// <param name="path">
            /// The path.
            /// </param>
            /// <typeparam name="T">
            /// </typeparam>
            /// <typeparam name="TProperty">
            /// </typeparam>
            /// <returns>
            /// The <see cref="IQueryable"/>.
            /// </returns>
            IQueryable<T> Include<T, TProperty>(IQueryable<T> source, Expression<Func<T, TProperty>> path) where T : class;
        }

        /// <summary>
        /// The null includer.
        /// </summary>
        internal class NullIncluder : IIncluder
        {
            /// <summary>
            /// The include.
            /// </summary>
            /// <param name="source">
            /// The source.
            /// </param>
            /// <param name="path">
            /// The path.
            /// </param>
            /// <typeparam name="T">
            /// </typeparam>
            /// <typeparam name="TProperty">
            /// </typeparam>
            /// <returns>
            /// The <see cref="IQueryable"/>.
            /// </returns>
            public IQueryable<T> Include<T, TProperty>(IQueryable<T> source, Expression<Func<T, TProperty>> path)
                where T : class
            {
                return source;
            }
        }
    }
}
