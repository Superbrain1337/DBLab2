using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab2
{
    public class Level
    {
        [Required]
        [Key]
        public int LevelId { get; set; }

        [Required]
        public int NumbOfBirds { get; set; }
        
        public Score Score { get; set; }
        //public IList<Score> PlayerScore { get; set; }
    }
}
