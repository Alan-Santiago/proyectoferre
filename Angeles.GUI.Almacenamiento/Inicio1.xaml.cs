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
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace Angeles.GUI.Almacenamiento
{
    /// <summary>
    /// Lógica de interacción para Inicio1.xaml
    /// </summary>
    public partial class Inicio1 : Window
    {
        public Inicio1()
        {
            InitializeComponent();
            Loadprogrssbar();
            cargar.ValueChanged += cargar_ValueChanged;
        }

       
        private void cargar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (cargar.Value == 100)
            {
                Usuario ventana = new Usuario();
                ventana.Show();
                this.Close();
            }
        }
        private void Loadprogrssbar()
        {
            Duration dur = new Duration(TimeSpan.FromSeconds(15));
            DoubleAnimation dblani = new DoubleAnimation(200.0, dur);
            cargar.BeginAnimation(System.Windows.Controls.Primitives.RangeBase.ValueProperty, dblani);
        }

    }
}
