namespace Billiards.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GameUser")]
    public partial class GameUser
    {
        public int GameUserId { get; set; }

        public int GameId { get; set; }

        public int UserId { get; set; }

        public int DefensiveShots { get; set; }

        public int Timeouts { get; set; }

        public bool IsActive { get; set; }

        public virtual Game Game { get; set; }

        public virtual User User { get; set; }
    }
}
