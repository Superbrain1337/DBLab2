using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab2
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public Score Score { get; set; }
        //public IList<Score> LevelScore { get; set; }
    }
}
