// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MigrationAccount.cs" company="Intermedia">
//   Copyright © Intermedia.net, Inc. 1995 - 2013. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace EmailProtection.Migration.Context.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     The migration account.
    /// </summary>
    public class MigrationAccount : IEntity
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MigrationAccount" /> class.
        /// </summary>
        public MigrationAccount()
        {
            this.MigrationAccountPhaseResults = new List<MigrationAccountPhaseResult>();
        }

        /// <summary>
        ///     Gets or sets the migration account outcome.
        /// </summary>
        public virtual ICollection<MigrationAccountPhaseResult> MigrationAccountPhaseResults { get; set; }

        /// <summary>
        ///     Gets or sets the migration job id.
        /// </summary>
        public int MigrationJobID { get; set; }

        /// <summary>
        ///     Gets or sets the account id.
        /// </summary>
        public int AccountID { get; set; }

        /// <summary>
        ///     Gets or sets the User Name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     Gets or sets the partner id.
        /// </summary>
        public int PartnerID { get; set; }

        /// <summary>
        ///     Gets or sets the phase.
        /// </summary>
        public int Phase { get; set; }

        /// <summary>
        ///     Gets or sets the state.
        /// </summary>
        public int State { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether domains created.
        /// </summary>
        public bool DomainsCreated { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether users created.
        /// </summary>
        public bool UsersCreated { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether mailflow test passed.
        /// </summary>
        public bool MailflowTestPassed { get; set; }

        /// <summary>
        ///     Gets or sets the migration job.
        /// </summary>
        public virtual MigrationJob MigrationJob { get; set; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     Gets or sets the internal state.
        /// </summary>
        public int InternalState { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether customer created.
        /// </summary>
        public bool CustomerCreated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether internal validation complete.
        /// </summary>
        public bool InternalValidationComplete { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether external validation complete.
        /// </summary>
        public bool ExternalValidationComplete { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is account verified.
        /// </summary>
        public bool AccountVerified { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether wizard shown.
        /// </summary>
        public bool? WizardShown { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether migration for account stopped.
        /// </summary>
        public bool Stopped { get; set; }

        /// <summary>
        /// Gets or sets a value for migration job account comments.
        /// </summary>
        public string Comments { get; set; }
    }
}
