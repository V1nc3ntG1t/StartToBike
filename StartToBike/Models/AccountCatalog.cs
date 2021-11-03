namespace StartToBike.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccountCatalog")]
    public partial class AccountCatalog
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AccountCatalog()
        {
            Account = new HashSet<Account>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RoleId { get; set; }

        [Required]
        public string AccountRole { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Account> Account { get; set; }
    }
}
