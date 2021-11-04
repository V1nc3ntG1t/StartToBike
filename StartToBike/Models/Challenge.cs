namespace StartToBike.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Challenge")]
    public partial class Challenge
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Challenge()
        {
            Account = new HashSet<Account>();
            Tour = new HashSet<Tour>();
        }

        public int ChallengeId { get; set; }

        [StringLength(50)]
        public string ChallengeName { get; set; }

        public DateTime StartDate { get; set; }

        public string Reward { get; set; }

        public string Task { get; set; }
        public string Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Account> Account { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tour> Tour { get; set; }

        public Boolean CreateChallenge()
        {
            ///<summary>
            ///sets startdate
            /// </summary>
            StartDate = DateTime.Now;

            Status = "Open";

            return true;
        }

        public Boolean ChallengeCompleted()
        {
            if (Status == "Challenge Completed")
            {
                return false;
            }

            Status = "Challenge Completed";
            return true;
        }
    }
}
