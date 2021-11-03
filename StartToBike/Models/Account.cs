namespace StartToBike.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            AccountTour = new HashSet<AccountTour>();
            Challenge = new HashSet<Challenge>();
            Quest = new HashSet<Quest>();
        }

        public int AccountId { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string BirthDate { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string City { get; set; }

        [Column(TypeName = "image")]
        [Required]
        public byte[] Picture { get; set; }

        public int RoleId { get; set; }

        public int? TrainingId { get; set; }

        public virtual AccountCatalog AccountCatalog { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountTour> AccountTour { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Challenge> Challenge { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Quest> Quest { get; set; }
    }
}
