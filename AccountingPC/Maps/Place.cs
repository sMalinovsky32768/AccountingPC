using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingPC.Maps
{
    public enum TypeOfPlace
    {
        table,
        systemUnit,
        monitor,
        notebook,
        projector,
        printer,
    }
    public class Place
    {
        public int Number { get; set; }
        public TypeOfPlace TypeOf { get; set; }

        public Place(string[] place)
        {
            TypeOf = (TypeOfPlace)Enum.Parse(TypeOf.GetType(), place[0]);
            Number = Convert.ToInt32(place[1]);
        }

        public Place(int num, string type)
        {
            Number = num;
            TypeOf = (TypeOfPlace)Enum.Parse(TypeOf.GetType(), type);
        }
    }
}
