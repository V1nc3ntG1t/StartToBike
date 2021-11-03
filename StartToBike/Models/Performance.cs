namespace StartToBike.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Performance")]
    public partial class Performance
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Performance()
        {
            AccountTour = new HashSet<AccountTour>();
        }

        public int PerformanceId { get; set; }

        public int Distance { get; set; }

        public int Altitude { get; set; }

        [Required]
        public string StartTime { get; set; }

        [Required]
        public string EndTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountTour> AccountTour { get; set; }
    }
}
