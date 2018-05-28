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
using System.Windows.Shapes;

namespace ferreteria.GUI.Administrador
{
    /// <summary>
    /// Lógica de interacción para Inicio.xaml
    /// </summary>
    public partial class Inicio : Window
    {
        IManejadorUsuario manejadorUsuario;
        public Inicio()
        {
            InitializeComponent();
            manejadorUsuario = new ManejadorUsuario(new RepositorioGenerico<Usuario>());
            Actualizar();
           //  txbContraseña2.Visibility = Visibility.Collapsed;
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
                    VentaAngeles b = new VentaAngeles();
                    b.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Contraseña Incorrecta", "Usuario", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado ningun usuario", "Usuario", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

       // private void cbxVisualizar_Click(object sender, RoutedEventArgs e)
       // {
           // if (string.IsNullOrEmpty(txbContraseña.Password))
          //  {
             //   MessageBox.Show("Falta ingresar contraseña", "Usuario", MessageBoxButton.OK, MessageBoxImage.Error);
             //   return;
           // }
           // if (txbContraseña2.Visibility == Visibility.Collapsed)
           // {
             //   txbContraseña.Visibility = Visibility.Collapsed;
             //   txbContraseña2.Text = txbContraseña.Password;
             //   txbContraseña2.Visibility = Visibility.Visible;
                
           // }
          //  else
          //  {
          //      txbContraseña.Visibility = Visibility.Visible;
          //      txbContraseña2.Visibility = Visibility.Collapsed;
          //  }
        //}
    }
}

