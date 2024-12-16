using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motus.API.Data.Entities
{
    public record HighScoreEntity
    {
        //Haytararum enq DB-i syunaknery,
        public int Id { get; set; }
        public int HighScore { get; set; }

    }
}
