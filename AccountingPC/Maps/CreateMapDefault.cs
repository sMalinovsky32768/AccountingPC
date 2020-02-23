using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AccountingPC.Maps
{
    class CreateMapDefault
    {
        public Canvas Canvas { get; private set; }

        public CreateMapDefault()
        {
            Canvas = new Canvas();
        }
    }
}
