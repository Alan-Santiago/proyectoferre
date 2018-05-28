using Ferreteria.BIZ;
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
using System.Windows.Shapes;

namespace Angeles.GUI.Almacenamiento
{
    /// <summary>
    /// Lógica de interacción para Usuario.xaml
    /// </summary>
    public partial class Usuario : Window
    {
        
        public Usuario()
        {
            InitializeComponent();
            //manejadorUsuario = new ManejadorUsuario(new RepositorioGenerico<Usuario>());
            
        }

       

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (txbUsuario.Text == "angeles" && txbContraseña.Password == "fa1234")
            {
                MainWindow b = new MainWindow();
                b.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("No ha ingresado la contraseña  usuario correcto", "Usuario", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }
    }
}
