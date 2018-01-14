namespace Billiards.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Game")]
    public partial class Game
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Game()
        {
            GameUsers = new HashSet<GameUser>();
        }

        public int GameId { get; set; }

        public int MatchId { get; set; }

        public int Number { get; set; }

        public int? WinnerUserId { get; set; }

        public bool IsActive { get; set; }

        public int Innings { get; set; }

        public virtual Match Match { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GameUser> GameUsers { get; set; }
    }
}
