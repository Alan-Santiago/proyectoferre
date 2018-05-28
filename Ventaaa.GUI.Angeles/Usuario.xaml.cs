using Ferreteria.BIZ;
using Ferreteria.COMMON.Entidades;
using Ferreteria.COMMON.interfaces;
using Ferreteria.COMMON.Modelos;
using Ferreteria.DAL;
using Microsoft.Reporting.WinForms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Ventaaa.GUI.Angeles
{
    /// <summary>
    /// Lógica de interacción para VentaAngeles.xaml
    /// </summary>
    public partial class VentaAngeles : Window
    {
       
        
        
        enum accion
        {
            Nuevo,
            Editar
        }
        
        IManejadorUsuario manejadorUsuario;
        IManejadorVales ManejadorVales;
        accion accionUsuario;



        public VentaAngeles()
        {
            InitializeComponent();
            manejadorUsuario = new ManejadorUsuario(new RepositorioGenerico<Usuario>());
            ManejadorVales = new ManejadorVales(new RepositorioGenerico<Vale>());
            PonerBotonesUsuarioEdicion(false);
            LimpiarCamposUsuario();
            ActualizarTablaUsuario();


        }

        private void ActualizarTablaUsuario()
        {
            dtgUsuario.ItemsSource = null;
            dtgUsuario.ItemsSource = manejadorUsuario.Listar.OrderBy(p => p.UsuarioNombre);
        }

        private void LimpiarCamposUsuario()
        {
            txbUsuarioNombre.Clear();
            txbUsuarioApellidos.Clear();
            txbUsuarioDireccion.Clear();
            txbUsuarioContraseña.Clear();
            imgFoto.Source = null;
        }

        private void PonerBotonesUsuarioEdicion(bool value)
        {
            btnUsuarioCancelar.IsEnabled = value;
            btnUsuarioEditar.IsEnabled = !value;
            btnUsuarioEliminar.IsEnabled = !value;
            btnUsuarioGuardar.IsEnabled = value;
            btnUsuarioNuevo.IsEnabled = !value;
        }

        private void btnUsuarioNuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposUsuario();
            PonerBotonesUsuarioEdicion(true);
            accionUsuario = accion.Nuevo;
        }

        private void btnUsuarioEditar_Click(object sender, RoutedEventArgs e)
        {
            if (dtgUsuario.SelectedItem != null)
            {
                Usuario usua = dtgUsuario.SelectedItem as Usuario;
                txbUsuarioId.Text = usua.Id.ToString();
                txbUsuarioNombre.Text = usua.UsuarioNombre;
                txbUsuarioApellidos.Text = usua.Apellidos;
                txbUsuarioDireccion.Text = usua.Direccion;
                txbUsuarioContraseña.Password = usua.Contraseña;
                imgFoto.Source = ByteToImage(usua.FotoGrafia);
                accionUsuario = accion.Editar;
                PonerBotonesUsuarioEdicion(true);

            }
            else
            {
                MessageBox.Show("No selecciono  ni un elemento de la tabla de los ususarios", "Usuario", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        public ImageSource ByteToImage(byte[] fotoGrafia)
        {
            if (fotoGrafia == null)
            {
                return null;
            }
            else
            {
                BitmapImage biImg = new BitmapImage();
                MemoryStream ms = new MemoryStream(fotoGrafia);
                biImg.BeginInit();
                biImg.StreamSource = ms;
                biImg.EndInit();
                ImageSource imgSrc = biImg as ImageSource;
                return imgSrc;

            }
        }

        private void btnUsuarioGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbUsuarioNombre.Text) || string.IsNullOrEmpty(txbUsuarioApellidos.Text) || string.IsNullOrEmpty(txbUsuarioDireccion.Text) || string.IsNullOrEmpty(txbUsuarioContraseña.Password))
            {
                MessageBox.Show("Faltan Datos...Por favor llene todos los campos", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (accionUsuario == accion.Nuevo)
            {
                Usuario usua = new Usuario()
                {
                    UsuarioNombre = txbUsuarioNombre.Text,
                    Apellidos = txbUsuarioApellidos.Text,
                    Direccion = txbUsuarioDireccion.Text,
                    Contraseña = txbUsuarioContraseña.Password,
                    FotoGrafia = ImageToByte(imgFoto.Source)
                };
                if (manejadorUsuario.Agregar(usua))
                {
                    MessageBox.Show("Usuario agregado correctamente", "Usuario", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTablaUsuario();
                    LimpiarCamposUsuario();
                    PonerBotonesUsuarioEdicion(false);
                }
                else
                {
                    MessageBox.Show("El Usuario no se pudo agregar", "Usuario", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            else
            {
                Usuario usua = dtgUsuario.SelectedItem as Usuario;
                usua.UsuarioNombre = txbUsuarioNombre.Text;
                usua.Apellidos = txbUsuarioApellidos.Text;
                usua.Direccion = txbUsuarioDireccion.Text;
                usua.Contraseña = txbUsuarioContraseña.Password;
                usua.FotoGrafia = ImageToByte(imgFoto.Source);
                if (manejadorUsuario.Modificar(usua))
                {
                    ActualizarTablaUsuario();
                    LimpiarCamposUsuario();
                    PonerBotonesUsuarioEdicion(false);
                    MessageBox.Show("Usuario editado correctamente", "Usuarios", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(" Lo siento No se puedo editar correctamente el usuario", "Usuarios", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
        }
        public byte[] ImageToByte(ImageSource image)
        {
            if (image != null)
            {
                MemoryStream memStream = new MemoryStream();
                JpegBitmapEncoder enconder = new JpegBitmapEncoder();
                enconder.Frames.Add(BitmapFrame.Create(image as BitmapSource));
                enconder.Save(memStream);
                return memStream.ToArray();
            }
            else
            {
                return null;
            }

        }
        private void btnUsuarioCancelar_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("Esta seguro de que  desea cancelar esta operacion", "Usuario", MessageBoxButton.YesNo, MessageBoxImage.Question)) == MessageBoxResult.Yes)
            {
                LimpiarCamposUsuario();
                PonerBotonesUsuarioEdicion(false);
            }
        }

        private void btnUsuarioEliminar_Click(object sender, RoutedEventArgs e)
        {
            Usuario a = dtgUsuario.SelectedItem as Usuario;
            if (a != null)
            {
                if (MessageBox.Show("Realmente esta seguro de eliminar el usuario: " + a.UsuarioNombre, "Eliminar Usuario", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    manejadorUsuario.Eliminar(a.Id);
                    ActualizarTablaUsuario();
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado ningun elemento de la tabla Usuarios", "Usuario", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void btnCargarFoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialogo = new OpenFileDialog();
            dialogo.Title = "Selecciona una foto";
            dialogo.Filter = "Archivo  de imagen|*.jpg; *.png; *.gif";
            if (dialogo.ShowDialog().Value)
            {
                imgFoto.Source = new BitmapImage(new Uri(dialogo.FileName));
            }
        }

        private void btnUsuarioReporte_Click(object sender, RoutedEventArgs e)
        {
            List<ReportDataSource> datos = new List<ReportDataSource>();
            ReportDataSource reporte = new ReportDataSource();
            List<ModeloVales> datosUsuarioss = new List<ModeloVales>();
            foreach (var item in ManejadorVales.Listar)
            {
                datosUsuarioss.Add(new ModeloVales(item));
            }
            reporte.Value = datosUsuarioss;
            reporte.Name = "DataSet1";
            datos.Add(reporte);
            Reporte1 ventana = new Reporte1("Ventaaa.GUI.Angeles.Reportes.ReporteSinParametros.rdlc", datos);//("Torneo.GUI.Reporte.ReporteSinParametros.rdlc", datos);
            ventana.ShowDialog();
        }
    }
}
