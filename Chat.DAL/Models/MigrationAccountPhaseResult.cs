// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MigrationAccountPhaseResult.cs" company="Intermedia">
//   Copyright © Intermedia.net, Inc. 1995 - 2013. All Rights Reserved.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace EmailProtection.Migration.Context.Models
{
    /// <summary>
    /// The migration account validation result.
    /// </summary>
    public class MigrationAccountPhaseResult : IEntity
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int ID { get; set; }        
        
        /// <summary>
        /// Gets or sets the migration account id.
        /// </summary>
        public int MigrationAccountID { get; set; }

        /// <summary>
        /// Gets or sets the migration phase.
        /// </summary>
        public int MigrationPhase { get; set; }

        /// <summary>
        /// Gets or sets the migration account.
        /// </summary>
        public virtual MigrationAccount MigrationAccount { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the item id.
        /// </summary>
        public string ItemID { get; set; }
    }
}
