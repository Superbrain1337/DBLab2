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
        [Required]
        [ForeignKey("LevelId")]
        public virtual IList<Level> Level { get; set; }
        
        [Required]
        [ForeignKey("PlayerId")]
        public virtual IList<Player> Player { get; set; }

        [Key]
        public int ScoreId { get; set; }
        
    }
}
