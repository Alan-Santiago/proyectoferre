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

namespace Venta.GUI.Angeles
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IManejadorUsuario manejadorUsuario;

        public MainWindow()
        {
            InitializeComponent();
            manejadorUsuario = new ManejadorUsuario(new RepositorioDeUsuario());
            Actualizar();
        }

        private void Actualizar()
        {
            cmbUsuario.ItemsSource = null;
            cmbUsuario.ItemsSource = manejadorUsuario.Listar;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
             if(cmbUsuario.Text=="")
            {
                MessageBox.Show("Error", "Usuario", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(string.IsNullOrEmpty(txbContraseña.Password))
            {
                MessageBox.Show("No ha ingresado la contraseña", "Usuario", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(cmbUsuario.SelectedItem !=null)
            {
                Usuario a = cmbUsuario.SelectedItem as Usuario;
                if(txbContraseña.Password==a.Contraseña)
                {
                    AngelesVenta b = new AngelesVenta();
                    b.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Contraseña Inconrrecta", "Usuario", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado ningun usuario", "Usario", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();

        }
    }
}
