namespace Billiards.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HandicapMatrix")]
    public partial class HandicapMatrix
    {
        public int HandicapMatrixId { get; set; }

        public int Player1 { get; set; }

        public int Player2 { get; set; }

        public int Player1Wins { get; set; }

        public int Player2Wins { get; set; }
    }
}
