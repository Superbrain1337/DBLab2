using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab2
{
    public class Player
    {
        [Required]
        [Key]
        public int PlayerId { get; set; }

        [Required]
        public string Name { get; set; }

        public Score Score { get; set; }
        //public IList<Score> LevelScore { get; set; }
    }
}
