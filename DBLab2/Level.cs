﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab2
{
    public class Level
    {
        public int LevelId { get; set; }
        public int NumbOfBirds { get; set; }
        public IList<Player> Players { get; set; }
    }
}
