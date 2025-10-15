using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kino_Vozhakova.Classes
{
    public class KinoteatrClass
    {
        public int id { get; set; }
        public string name { get; set; }
        public int count_zal { get; set; }
        public int count { get; set; }

        public KinoteatrClass(int id, string name, int count_zal, int count)
        {
            this.id = id;
            this.name = name;
            this.count_zal = count_zal;
            this.count = count;
        }
    }
}
