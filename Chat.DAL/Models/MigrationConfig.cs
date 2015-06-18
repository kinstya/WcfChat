// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MigrationConfig.cs" company="Intermedia">
//   Copyright © Intermedia.net, Inc. 1995 - 2013. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace EmailProtection.Migration.Context.Models
{
    using System;

    /// <summary>
    /// The migration config.
    /// </summary>
    public class MigrationConfig : IEntity
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the account id.
        /// </summary>
        public int? AccountID { get; set; }

        /// <summary>
        /// Gets or sets the partner id.
        /// </summary>
        public int? PartnerID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }
    }
}
