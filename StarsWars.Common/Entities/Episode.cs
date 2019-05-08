using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarsWars.Common.Entities
{
    public class Episode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Character Character { get; set; }

    }
}
