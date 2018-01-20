namespace Billiards.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Match")]
    public partial class Match
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Match()
        {
            Games = new HashSet<Game>();
        }

        public int MatchId { get; set; }

        public DateTime Date { get; set; }

        public int User1Id { get; set; }

        public int User2Id { get; set; }

        public int User1Points { get; set; }

        public int User2Points { get; set; }

        public bool IsActive { get; set; }

        public int MatchType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game> Games { get; set; }

        public virtual User User { get; set; }

        public virtual User User1 { get; set; }
    }
}
