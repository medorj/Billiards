using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billiards.API.Models
{
    public class HandicapMatrixViewModel
    {
        public int HandicapMatrixId { get; set; }
        public int Player1 { get; set; }
        public int Player2 { get; set; }
        public int Player1Wins { get; set; }
        public int Player2Wins { get; set; }
    }

    public static class HandicapMatrixMapper
    {
        public static HandicapMatrixViewModel ToViewModel(this HandicapMatrix handicapMatrix)
        {
            return new HandicapMatrixViewModel
            {
                HandicapMatrixId = handicapMatrix.HandicapMatrixId,
                Player1 = handicapMatrix.Player1,
                Player2 = handicapMatrix.Player2,
                Player1Wins = handicapMatrix.Player1Wins,
                Player2Wins = handicapMatrix.Player2Wins
            };
        }

        public static IEnumerable<HandicapMatrixViewModel> ToViewModel(this IEnumerable<HandicapMatrix> handicapMatrixes)
        {
            return handicapMatrixes.Select(h => h.ToViewModel());
        }
    }
}