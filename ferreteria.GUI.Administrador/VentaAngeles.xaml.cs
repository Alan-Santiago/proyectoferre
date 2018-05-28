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
    /// Lógica de interacción para VentaAngeles.xaml
    /// </summary>
    public partial class VentaAngeles : Window
    {
        List<Venta> venta;
        bool ven;
        enum accion
        {
            Nuevo,
            Editar
        }
       
        IManejadorUsuario manejadorUsuario;
        IManejadorClientes manejadorCliente;
        IManejadorCategoria manejadorCategoria;
        IManejadorMaterial manejadorMaterial;
        IManejadorVenta manejadorVenta;
        IManejadorVales manejadorVales;
        Vale vale = new Vale();
        Venta ventaas= new Venta();
        
        
       
        accion accionCliente;
        //accion accionCategoria;
        //accion accionProducto;
        //accion accionUsuario;
        accion accionVenta;
        accion accionVales;


        public VentaAngeles()
        {
            InitializeComponent();
           
           venta = new List<Venta>();

            manejadorCliente = new ManejadorCliente(new RepositorioGenerico<Cliente>());
            manejadorVenta = new ManejadorVenta(new RepositorioGenerico<Venta>());
            manejadorMaterial = new ManejadorMaterial(new RepositorioGenerico<Material>());
            manejadorUsuario = new ManejadorUsuario(new RepositorioGenerico<Usuario>());
            manejadorCategoria = new ManejadorCategoria(new RepositorioGenerico<Categoria>());
            manejadorVales = new ManejadorVales(new RepositorioGenerico<Vale>());
            Actualizar();
            CargarVenta();

            
            PonerBotonesEdicionCliente(false);
            LimpiarCamposCliente();
            ActualizarTablaCliente();

            PonerBotonesVentaEdicion(false);
            Ventaa(false);
            HabilitarVentaCajas(true);
            CargarVenta();
            //LimpiarCamposdeVenta();
            ActualizarTablaVales();
        }

        private void ActualizarTablaVales()
        {
            dtgVentaAlmacen.ItemsSource = null;
            dtgVentaAlmacen.ItemsSource = manejadorVales.Listar;
        }

        private void Actualizar()
        {
            cmbVentaEmpleado.ItemsSource = null;
            cmbVentaEmpleado.ItemsSource = manejadorUsuario.Listar;
        }

        private void HabilitarVentaCajas(bool v)
        {
            txbVentaFecha.Clear();
            txbVentaFolio.Clear();
            txbVentaCantidadProductos.Clear();
            txbTotal.Clear();
            txbVentaFecha.IsEnabled = !v;
            txbVentaFolio.IsEnabled = !v;
            cmbVentaCliente.IsEnabled = !v;
            cmbVentaEmpleado.IsEnabled = !v;
            // txbVentaCantidadProductos = !v;
        }

        private void Ventaa(bool v)
        {
            btnVentaNueva.IsEnabled = !v;
            HabilitarVentaCajas(false);
          
        }

        private void PonerBotonesVentaEdicion(bool v)
        {
            btnVentaAgregarMaterial.IsEnabled = !v;
            btnVentaNueva.IsEnabled = !v;
            btnVenta.IsEnabled = !v;
            btnVentaCancelar.IsEnabled = !v;
        }

        private void CargarVenta()
        {

            txbVentaFecha.Text = DateTime.Now.ToLongDateString();
            dtgMaterial.ItemsSource = null;
            dtgMaterial.ItemsSource = manejadorMaterial.Listar;
            cmbVentaCliente.ItemsSource = manejadorCliente.Listar;
            cmbVentaEmpleado.ItemsSource = manejadorUsuario.Listar;

            AgregarTabla();
            dtgVenta.ItemsSource = null;
            dtgVenta.ItemsSource = manejadorMaterial.Listar;

            AgregarTabla();
            Random m = new Random();
            int a = m.Next(0, 1000000);
            txbVentaFolio.Text = a.ToString();
        }

        private void ActualizarTablaCliente()
        {
            dtgCliente.ItemsSource = null;
            dtgCliente.ItemsSource = manejadorCliente.Listar;
        }

        private void LimpiarCamposCliente()
        {
            txbClienteNombre.Clear();
            txbClienteDireccion.Clear();
            txbClienteRFC.Clear();
            txbClienteTelefono.Clear();
            txbClienteCorreo.Clear();
            txbClienteId.Text = "";
        }

        private void PonerBotonesEdicionCliente(bool value)
        {
            btnClienteCancelar.IsEnabled = value;
            btnClienteEditar.IsEnabled = !value;
            btnClienteEliminar.IsEnabled = !value;
            btnClienteGuardar.IsEnabled = value;
            btnClienteNuevo.IsEnabled = !value;
        }

        private void btnClienteNuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposCliente();
            PonerBotonesEdicionCliente(true);
            accionCliente = accion.Nuevo;
        }

        private void btnClienteEditar_Click(object sender, RoutedEventArgs e)
        {
            Cliente clie = dtgCliente.SelectedItem as Cliente;
            if (clie != null)
            {
                txbClienteId.Text = clie.Id.ToString();
                txbClienteNombre.Text = clie.Nombre;
                txbClienteDireccion.Text = clie.Direccion;
                txbClienteRFC.Text = clie.RFC;
                txbClienteTelefono.Text = clie.Telefono;
                txbClienteCorreo.Text = clie.Correo;
               accionCliente = accion.Editar;
                PonerBotonesEdicionCliente(true);
            }

            else
            {
                MessageBox.Show("No selecciono  ni un elemento de la tabla de los ususarios", "Usuario", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void btnClienteGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbClienteNombre.Text) || string.IsNullOrEmpty(txbClienteDireccion.Text) || string.IsNullOrEmpty(txbClienteTelefono.Text))
            {
                MessageBox.Show("Faltan Datos...Por favor llene todos los campos", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (accionCliente == accion.Nuevo)
            {
                Cliente clie = new Cliente()
                {
                    Nombre = txbClienteNombre.Text,
                    Direccion = txbClienteDireccion.Text,
                    RFC = txbClienteRFC.Text,
                    Telefono = txbClienteTelefono.Text,
                    Correo = txbClienteCorreo.Text

                };
                if (manejadorCliente.Agregar(clie))
                {
                    MessageBox.Show("Cliente agregado correctamente", "Ferreteria", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposCliente();
                    ActualizarTablaCliente();
                    PonerBotonesEdicionCliente(false);
                }
                else
                {
                    MessageBox.Show("El Cliente no se pudo agregar", "Ferreteria", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            else
            {
                Cliente clie = dtgCliente.SelectedItem as Cliente;
                clie.Nombre = txbClienteNombre.Text;
                clie.Direccion = txbClienteDireccion.Text;
                clie.RFC = txbClienteRFC.Text;
                clie.Telefono = txbClienteTelefono.Text;
                clie.Correo = txbClienteCorreo.Text;
                if (manejadorCliente.Modificar(clie))
                {
                    MessageBox.Show("Cliente modificado correctamente", "Ferreteria", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposCliente();
                    ActualizarTablaCliente();
                    PonerBotonesEdicionCliente(false);
                }
                else
                {
                    MessageBox.Show("El  no se pudo actualizar", "Ferreteria", MessageBoxButton.OK, MessageBoxImage.Error);

                }

            }
        }

        private void btnClienteCancelar_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("Esta seguro de que  desea cancelar esta operacion", "Usuario", MessageBoxButton.YesNo, MessageBoxImage.Question)) == MessageBoxResult.Yes)
            {
                LimpiarCamposCliente();
                PonerBotonesEdicionCliente(false);

            }
        }

        private void btnClienteEliminar_Click(object sender, RoutedEventArgs e)
        {
            Cliente clie = dtgCliente.SelectedItem as Cliente;
            if (clie != null)
            {
                if (MessageBox.Show("Realmente esta seguro de eliminar al cliente " + clie.Nombre, "Eliminar Cliente", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (manejadorCliente.Eliminar(clie.Id))
                    {
                        MessageBox.Show("Cliente eliminado", "Ferreteria", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizarTablaCliente();
                    }
                    else
                    {
                        MessageBox.Show("No se puede eliminar el Cliente", "Ferreteria", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                }
            }
        }

        private void btnVentaAgregarMaterial_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(txbVentaCantidadProductos.Text))
            {
                MessageBox.Show("Ingresar Cantidad", "Venta", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }
            foreach (var item in txbVentaCantidadProductos.Text)
            {
                if (!(char.IsNumber(item)))
                {
                    MessageBox.Show("Solo se permiten números, no caracteres", "Ventas", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            foreach (var item in txbTotal.Text)
            {
                if (!(char.IsNumber(item)))
                {
                    MessageBox.Show("Solo se permiten números, no caracteres", "Ventas", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            if (dtgMaterial.SelectedItem != null)
            {
                Material a = dtgMaterial.SelectedItem as Material;
                Venta b = new Venta();
                b.Producto = a.Nombre;
                b.Categoria = a.Categoria;
                b.cantidad = int.Parse(txbVentaCantidadProductos.Text);
                b.Costo = a.PrecioDeVenta;
                b.Total = (b.cantidad) * (b.Costo);
                venta.Add(b);
                AgregarTabla();
                txbVentaCantidadProductos.Clear();

            }
        }

        private void btnVentaCancelar_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("Esta seguro que desea cancelar esta venta", "venta", MessageBoxButton.YesNo, MessageBoxImage.Question)) == MessageBoxResult.Yes)
            {
                venta = new List<Venta>();
                dtgVenta.ItemsSource = null;
            }

        }
    
        private void btnVentaNueva_Click(object sender, RoutedEventArgs e)
        {
            PonerBotonesVentaEdicion(false);
            HabilitarVentaCajas(true);
            Ventaa(false);
            CargarVenta();
        }

        private void btnVenta_Click(object sender, RoutedEventArgs e)
        {
            Vale vale = new Vale();
            vale.Fecha = txbVentaFecha.Text;
            vale.Folio = txbVentaFolio.Text;
            vale.NombreCliente = cmbVentaCliente.SelectedItem as Cliente;
            vale.NombreEmpleado = cmbVentaEmpleado.SelectedItem as Usuario;
            float total = 0;
            //double iva = 0.16;
            foreach (Venta item in venta)
            {
                total += item.Total;
                txbTotal.Text = total.ToString();

                //vale.Total = txbTotal.Text;
                //ActualizarTablaVales();
            }
            vale.Total = total.ToString();
            if (manejadorVales.Agregar(vale))
            {
                ActualizarTablaVales();
            }
            if (venta.Count == 0)
            {
                MessageBox.Show("No tiene ningun producto en la lista", "venta", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //vale.Total = txbTotal.Text;
            //ActualizarTablaVales();
            MessageBox.Show("La Venta Es:.....\nTotal " + total.ToString(), "venta", MessageBoxButton.OK, MessageBoxImage.Information);
            Tiquet reporte = new Tiquet(txbVentaFolio.Text + ".poo");
            vale.Total = txbTotal.Text;
            ActualizarTablaVales();
            string an = "";
            an = string.Format("FARMACIA\nFecha:{0}\nFolio:{1}\nCliente:{2}\nEmpleado:{3}\n", txbVentaFecha.Text, txbVentaFolio.Text, cmbVentaCliente.Text, cmbVentaEmpleado.Text);
            string al = "";
            foreach (Venta item in venta)
            {
                al += string.Format("producto:{0}\nCategoria:{1}\nPrecio:{2}\nCantidad:{3}\nTotal a Pagar:{4}\n", item.Producto, item.Categoria, item.Costo, item.cantidad, item.Total);

            }
            string san = string.Format("SubTotal:{0}\nTotal:{1}", total.ToString(), (total + (total )));
            reporte.Guardar(an + al + san);
            vale.Total = txbTotal.Text;

            //vale.Total = total.ToString();

            ActualizarTablaVales();
            MessageBox.Show("El Reporte esta listo" + txbVentaFolio + ".poo", "Reporte", MessageBoxButton.OK, MessageBoxImage.Information);
            
            //vale.Total = txbTotal.Text;
            //ActualizarTablaVales();
            txbVentaFolio.Clear();
            PonerBotonesVentaEdicion(true);

            Ventaa(false);
            HabilitarVentaCajas(true);
            CargarVenta();
            txbVentaCantidadProductos.Clear();
            venta = new List<Venta>();
            dtgVenta.ItemsSource = null;
            //vale.Total = txbTotal.Text;
            //ActualizarTablaVales();


        }
        private void AgregarTabla()
        {
            dtgVenta.ItemsSource = null;
            dtgVenta.ItemsSource = venta;

        }
        private void GuadarVentaDatos()
        {
            if (ven)
            {
                if (dtgMaterial.SelectedItem != null)
                {
                    Material a = dtgMaterial.SelectedItems as Material;
                    Venta b = new Venta();
                    b.Producto = a.Nombre;
                    b.Categoria = a.Categoria;
                    b.cantidad = int.Parse(txbVentaCantidadProductos.Text);
                    b.Costo = a.PrecioDeVenta;
                    b.Total = (b.cantidad) * (b.Costo);
                    venta.Add(b);
                    AgregarTabla();
                    txbVentaCantidadProductos.Clear();



                }
                else

                {
                    MessageBox.Show("Seleccionar el Dato", "Ventas", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            }
            else
            {
                Material a = dtgMaterial.SelectedItems as Material;
                Venta b = new Venta();
                Venta c = new Venta();
                b.Producto = a.Nombre;
                b.Categoria = a.Categoria;
                b.cantidad = int.Parse(txbVentaCantidadProductos.Text);
                b.Costo = a.PrecioDeVenta;
                b.Total = (b.cantidad) * (b.Costo);

                foreach (var item in venta)
                {
                    if (a.Nombre == item.Producto)
                    {
                        b = item;
                    }
                }
                b.cantidad = c.cantidad; ;
                b.Categoria = c.Categoria;
                b.Costo = c.Costo;
                b.Producto = c.Producto;
                b.Total = b.cantidad * b.Costo;
                AgregarTabla();

            }

        }



        private void btnValeEliminar_Click_1(object sender, RoutedEventArgs e)
        {
            Vale v = dtgVentaAlmacen.SelectedItem as Vale;
            if (v != null)
            {
                if (MessageBox.Show("Realmente esta seguro de eliminar al cliente ", "Ferreteria", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (manejadorVales.Eliminar(v.Id))
                    {
                        MessageBox.Show("Venta eliminada", "Ferreteria", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizarTablaVales();
                    }
                    else
                    {
                        MessageBox.Show("No se puede eliminar la venta", "Ferreteria", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                }
            }
        }

        
    }
}
