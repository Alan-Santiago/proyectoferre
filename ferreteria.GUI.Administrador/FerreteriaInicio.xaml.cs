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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ferreteria.GUI.Administrador
{
    /// <summary>
    /// Lógica de interacción para FerreteriaInicio.xaml
    /// </summary>
    public partial class FerreteriaInicio : Window
    {
        public FerreteriaInicio()
        {
            InitializeComponent();
            Loadprogrssbar();
            cargar.ValueChanged += cargar_ValueChanged;
        }

        private void cargar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(cargar.Value==100)
            {
                Inicio ventana = new Inicio();
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
