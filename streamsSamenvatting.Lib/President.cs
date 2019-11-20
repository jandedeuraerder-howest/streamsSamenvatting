using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace streamsSamenvatting.Lib
{
    public class President
    {
        public int id { get; set; }
        public int president { get; set; }
        public string nm { get; set; }
        public string pp { get; set; }
        public string tm { get; set; }

        public override string ToString()
        {
            return nm + " " + pp + " " + tm;
        }
    }
}
