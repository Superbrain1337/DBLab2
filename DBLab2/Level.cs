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
        [Key]
        public int LevelId { get; set; }
        
        [Required]
        [Column("NumbOfBirds", TypeName = "int")]
        public int NumbOfBirds { get; set; }
        
        public virtual IList<Player> Players { get; set; }
    }
}
