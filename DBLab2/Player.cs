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
        [Key]
        public int PlayerId { get; set; }

        [Required]
        [Column("Name", TypeName = "nvarchar")]
        [StringLength(32)]
        public string Name { get; set; }
        
        public virtual IList<Score> PlayerScore { get; set; }
    }
}
