using Ferreteria.BIZ;
using Ferreteria.COMMON.Entidades;
using Ferreteria.COMMON.interfaces;
using Ferreteria.DAL;
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

namespace Ventaaa.GUI.Angeles
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (txbUsuario.Text == "ferreteriaangeles" && txbContraseña.Password == "ferreteriaangeles1234") 
            {
                VentaAngeles b = new VentaAngeles();
                b.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("No ha ingresado la contraseña  usuario correcto", "Usuario", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
