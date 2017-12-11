using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab2
{
    public class Score
    {
        public virtual IList<Level> Levels { get; set; }
        public virtual Player Player { get; set; }
    }
}
