using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab2
{
    public class Score
    {
        [Key]
        public int ScoreId { get; set; }

        [Required]
        [Column("Moves", TypeName = "int")]
        public int Moves { get; set; }

        [Required]
        public virtual Level Level { get; set; }
        
        
    }
}
