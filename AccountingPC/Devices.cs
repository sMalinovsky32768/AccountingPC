using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingPC
{
    public class PC
    {
        public uint InventoryNumber { get; set; }
        public string Name { get; set; }
        public uint Cost { get; set; }
        public string Motherboard { get; set; }
        public string CPU { get; set; }
        
        public PC() { }
        public PC(uint inv, string name, uint cost, string mb, string cpu)
        {
            InventoryNumber = inv;
            Name = name;
            Cost = cost;
            Motherboard = mb;
            CPU = cpu;
        }
    }
}
