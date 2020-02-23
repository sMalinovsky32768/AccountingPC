using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AccountingPC.Maps
{
    /// <summary>
    /// Логика взаимодействия для MapDefault.xaml
    /// </summary>
    public partial class MapDefault : Page
    {
        public MapDefault()
        {
            InitializeComponent();
        }

        private void MouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            new ViewPlaceInfo(new Place(((Path)e.OriginalSource).Name.Split('_'))).ShowDialog();
        }
    }
}
