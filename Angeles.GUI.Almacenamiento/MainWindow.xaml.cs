using Ferreteria.BIZ;
using Ferreteria.COMMON.Entidades;
using Ferreteria.COMMON.interfaces;
using Ferreteria.DAL;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Angeles.GUI.Almacenamiento
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum accion
        {
            Nuevo,
            Editar
        }
        IManejadorCategoria manejadorCategoria;
        IManejadorMaterial manejadorMaterial;

        accion accionCategoria;
        accion accionMaterial;
        public MainWindow()
        {
            InitializeComponent();
            InitializeComponent();
            manejadorCategoria = new ManejadorCategoria(new RepositorioGenerico<Categoria>());
            manejadorMaterial = new ManejadorMaterial(new RepositorioGenerico<Material>());

            PonerBotonesCategoriaEdicion(false);
            LimpiarCamposCategoria(false);
            ActualizarTablaCategoria();

            PonerBotonesMaterialEdicion(false);
            LimpiarCamposMaterial();
            ActualizarTablaMaterial();
        }

        private void ActualizarTablaMaterial()
        {
            dtgMaterial.ItemsSource = null;
            dtgMaterial.ItemsSource = manejadorMaterial.Listar;
        }

        private void LimpiarCamposMaterial()
        {
            txbMaterialNombre.Clear();
            txbMaterialDescripcion.Clear();
            txbMaterialPrecioAdquisicion.Clear();
            txbMaterialPrecioVenta.Clear();
            txbMaterialId.Text = "";
            imgFoto.Source = null;
        }

        private void PonerBotonesMaterialEdicion(bool value)
        {
            btnMaterialCancelar.IsEnabled = value;
            btnMaterialEditar.IsEnabled = !value;
            btnMaterialEliminar.IsEnabled = !value;
            btnMaterialGuardar.IsEnabled = value;
            btnMaterialNuevo.IsEnabled = !value;
        }

        private void ActualizarTablaCategoria()
        {
            dtgCategoria.ItemsSource = null;
            dtgCategoria.ItemsSource = manejadorCategoria.Listar;
            cmbMaterialCategoria.ItemsSource = manejadorCategoria.Listar;
        }

        private void LimpiarCamposCategoria(bool v)
        {
            txbNombreCategoria.Clear();
            txbCategoriaId.Text = "";
        }

        private void PonerBotonesCategoriaEdicion(bool value)
        {
            btnCategoriaCancelar.IsEnabled = value;
            btnCategoriaEditar.IsEnabled = !value;
            btnCategoriaEliminar.IsEnabled = !value;
            btnCategoriaGuardar.IsEnabled = value;
            btnDCategoriaNuevo.IsEnabled = !value;
        }

        private void btnDCategoriaNuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposCategoria(true);
            PonerBotonesCategoriaEdicion(true);
            accionCategoria = accion.Nuevo;
        }

        private void btnCategoriaEditar_Click(object sender, RoutedEventArgs e)
        {
            Categoria cate = dtgCategoria.SelectedItem as Categoria;
            if (cate != null)
            {
                txbCategoriaId.Text = cate.Id.ToString();
                txbNombreCategoria.Text = cate.NombreTipoDeMaterial;
                accionCategoria = accion.Editar;
                PonerBotonesCategoriaEdicion(true);

            }
            else
            {
                MessageBox.Show("No ha seleccionado ningun campo de la tabla", "Ferreteria", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void btnCategoriaGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbNombreCategoria.Text))
            {
                MessageBox.Show("Faltan Datos", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (accionCategoria == accion.Nuevo)
            {
                Categoria cate = new Categoria();

                cate.NombreTipoDeMaterial = txbNombreCategoria.Text;

                if (manejadorCategoria.Agregar(cate))
                {
                    MessageBox.Show("Categoria agregada correctamente", "Ferreteria", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposCategoria(false);
                    ActualizarTablaCategoria();
                    PonerBotonesCategoriaEdicion(false);
                }
                else
                {
                    MessageBox.Show("La Categoria no se pudo agregar", "Ferreteria", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            else
            {
                Categoria cate = dtgCategoria.SelectedItem as Categoria;
                cate.NombreTipoDeMaterial = txbNombreCategoria.Text;
                if (manejadorCategoria.Modificar(cate))
                {
                    MessageBox.Show("Categoria modificada correctamente", "Ferreteria", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposCategoria(false);
                    ActualizarTablaCategoria();
                    PonerBotonesCategoriaEdicion(false);
                }
                else
                {
                    MessageBox.Show("La Categoria no se pudo actualizar", "Ferreteria", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
        }

        private void btnCategoriaCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposCategoria(false);
            PonerBotonesCategoriaEdicion(false);
        }

        private void btnCategoriaEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dtgCategoria.SelectedItem != null)

            {
                Categoria cate = dtgCategoria.SelectedItem as Categoria;
                if (MessageBox.Show("Realmente quieres Eliminar esta Categoria?", "Ferreteria", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    manejadorCategoria.Eliminar(cate.Id);
                    ActualizarTablaCategoria();
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado ningun campo de la tabla", "Ferreteria", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnMaterialNuevo_Click(object sender, RoutedEventArgs e)
        {

            LimpiarCamposMaterial();
            PonerBotonesMaterialEdicion(true);
            accionMaterial = accion.Nuevo;
        }

        private void btnMaterialEditar_Click(object sender, RoutedEventArgs e)
        {
            if (dtgMaterial.SelectedItem != null)
            {
                Material mate = dtgMaterial.SelectedItem as Material;

                txbMaterialId.Text = mate.Id.ToString();
                txbMaterialNombre.Text = mate.Nombre;
                txbMaterialDescripcion.Text = mate.DescripcionDeMaterial;
                txbMaterialPrecioAdquisicion.Text = mate.PrecioDeAdquisicion.ToString();
                txbMaterialPrecioVenta.Text = mate.PrecioDeVenta.ToString();
                cmbMaterialCategoria.Text = mate.Categoria;
                imgFoto.Source = ByteToImage(mate.FotoGrafia);

                accionMaterial = accion.Editar;
                PonerBotonesMaterialEdicion(true);

            }
            else
            {

            }
        }

        private ImageSource ByteToImage(byte[] fotoGrafia)
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

        private void btnMaterialGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbMaterialNombre.Text) || string.IsNullOrEmpty(txbMaterialDescripcion.Text))
            {
                MessageBox.Show("Faltan Datos", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txbMaterialPrecioAdquisicion.Text))
            {
                MessageBox.Show("Faltan Datos", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            foreach (var item in txbMaterialPrecioAdquisicion.Text)
            {
                if (!(char.IsNumber(item)))
                {
                    MessageBox.Show("Solo se permiten números , no caracteres", "Material", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            if (string.IsNullOrEmpty(txbMaterialPrecioVenta.Text))
            {
                MessageBox.Show("Faltan Datos", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            foreach (var item in txbMaterialPrecioVenta.Text)
            {
                if (!(char.IsNumber(item)))
                {
                    MessageBox.Show("Solo se permiten números , no caracteres", "Material", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            if (accionMaterial == accion.Nuevo)
            {
                Material mate = new Material()
                {
                    Nombre = txbMaterialNombre.Text,
                    DescripcionDeMaterial = txbMaterialDescripcion.Text,
                    PrecioDeAdquisicion = float.Parse(txbMaterialPrecioAdquisicion.Text),
                    PrecioDeVenta = float.Parse(txbMaterialPrecioVenta.Text),
                    Categoria = cmbMaterialCategoria.Text,
                    FotoGrafia = ImageToByte(imgFoto.Source)

                };
                if (manejadorMaterial.Agregar(mate))
                {
                    MessageBox.Show("Material agregado correctamente", "Ferreteria", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposMaterial();
                    ActualizarTablaMaterial();
                    PonerBotonesMaterialEdicion(false);
                }
                else
                {
                    MessageBox.Show("El Material no se pudo agregar", "Ferreteria", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Material mate = dtgMaterial.SelectedItem as Material;
                mate.Nombre = txbMaterialNombre.Text;
                mate.DescripcionDeMaterial = txbMaterialDescripcion.Text;
                mate.PrecioDeAdquisicion = float.Parse(txbMaterialPrecioAdquisicion.Text);
                mate.PrecioDeVenta = float.Parse(txbMaterialPrecioVenta.Text);
                mate.Categoria = cmbMaterialCategoria.Text;
                mate.FotoGrafia = ImageToByte(imgFoto.Source);
                if (manejadorMaterial.Modificar(mate))
                {
                    MessageBox.Show("Material modificado correctamente", "Ferreteria", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposMaterial();
                    ActualizarTablaMaterial();
                    PonerBotonesMaterialEdicion(false);
                }
                else
                {
                    MessageBox.Show("El Material no se pudo actualizar", "Ferreteria", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
        }

        private byte[] ImageToByte(ImageSource image)
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

        private void btnMaterialCancelar_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("Esta seguro de que  desea cancelar esta operacion", "Ferreteria", MessageBoxButton.YesNo, MessageBoxImage.Question)) == MessageBoxResult.Yes)
            {
                LimpiarCamposMaterial();
                PonerBotonesMaterialEdicion(false);
            }
        }

        private void btnMaterialEliminar_Click(object sender, RoutedEventArgs e)
        {

            Material mate = dtgMaterial.SelectedItem as Material;
            if (mate != null)
            {
                if (MessageBox.Show("Realmente quieres Eliminar este Material?", "Ferreteria", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    manejadorMaterial.Eliminar(mate.Id);
                    ActualizarTablaMaterial();
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionando ningun material de la tabla", "Ferreteria", MessageBoxButton.OK, MessageBoxImage.Error);

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
    }
}
