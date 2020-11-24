using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaControlador;
namespace CapaVista
{
    public partial class Transacciones : Form
    {
        clsControladorExamen controlador = new clsControladorExamen();
        int Codigo;
        float monto, saldo, saldoModificado;
        public Transacciones()
        {
            InitializeComponent();
            Codigo = controlador.codigoAutomatico();
            llenarComboCuenta();
            llenarComboTipoTransaccion();
            llenarComboTipoMoneda();
            MostrarDatosDataGridView();
        }
        public void llenarComboCuenta()
        {
            List<string> id = controlador.Llenar("CUENTA_BANCARIA", "pk_id_numero_cuenta_bancaria");

            foreach (var item in id)
            {
                cmbCodigoCuenta.Items.Add(item);
            }
        }
        public void llenarComboTipoTransaccion()
        {
            List<string> id = controlador.Llenar("TIPO_TRANSACCION", "pk_id_tipo_transaccion");

            foreach (var item in id)
            {
                cmbCodigoTransaccion.Items.Add(item);
            }

            List<string> nombre = controlador.Llenar("TIPO_TRANSACCION", "nombre_tipo_transaccion");

            foreach (var item in nombre)
            {
                cmbTransaccion.Items.Add(item);
            }
        }

        public void llenarComboTipoMoneda()
        {
            List<string> id = controlador.Llenar("TIPO_MONEDA", "pk_id_tipo_moneda");

            foreach (var item in id)
            {
                cmbCodigoMoneda.Items.Add(item);
            }

            List<string> nombre = controlador.Llenar("TIPO_MONEDA", "moneda_tipo_moneda");

            foreach (var item in nombre)
            {
                cmbMoneda.Items.Add(item);
            }
        }
        public void IngresarTransaccion()
        {
            String Fecha = dtpFecha.Value.ToString("yyyy-MM-dd HH:MM");
            if (controlador.IngresarTransaccion(Codigo, Int32.Parse(cmbCodigoCuenta.SelectedItem.ToString()), Fecha, Int32.Parse(cmbCodigoTransaccion.SelectedItem.ToString()), Int32.Parse(cmbCodigoMoneda.SelectedItem.ToString()), txtMonto.Text, txtDescripcion.Text))
            {
                MessageBox.Show("Los Datos se ingresaron correctamente", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Registro no valido", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void ActualizarSaldo()
        {
            if (controlador.ActualizarSaldo(saldoModificado, Int32.Parse(cmbCodigoCuenta.SelectedItem.ToString())))
            {

            }
            else
            {
                MessageBox.Show("Registro no valido", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void MostrarDatosDataGridView()
        {
            DataTable dt = controlador.llenarTbl();
           dgvDatos.DataSource = dt;

        }
        private void cmbTransaccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCodigoTransaccion.SelectedIndex = cmbTransaccion.SelectedIndex;
        }

        private void cmbMoneda_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCodigoMoneda.SelectedIndex = cmbMoneda.SelectedIndex;
        }

        private void cmbCodigoCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            saldo = controlador.ObtnerSaldo(Int32.Parse(cmbCodigoCuenta.SelectedItem.ToString()));
            txtSaldoActual.Text = saldo.ToString();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Esta Seguro de cancelar la transaccion?", "Advertencia", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialogResult == DialogResult.OK)
            {
                cmbCodigoCuenta.Items.Clear();
                cmbCodigoTransaccion.Items.Clear();
                cmbCodigoMoneda.Items.Clear();
                cmbTransaccion.Items.Clear();
                txtDescripcion.Text = "";
                cmbMoneda.Items.Clear();
                txtMonto.Text = "";
                txtSaldoActual.Text = "";
                Codigo = controlador.codigoAutomatico();
                llenarComboCuenta();
                llenarComboTipoTransaccion();
                llenarComboTipoMoneda();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "AyudaFRM/AyudaFRM.chm", "Transacciones.html");
        }

        private void Transacciones_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtDescripcion.Text == "" || txtMonto.Text == "")
            {
                MessageBox.Show("Algunos campos siguen vacios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int OpcionTipoTransaccion = Int32.Parse(cmbCodigoTransaccion.SelectedItem.ToString());
                try
                {
                    float.Parse(txtMonto.Text.ToString());
                    monto = float.Parse(txtMonto.Text.ToString());
                    if (OpcionTipoTransaccion == 1)
                    {
                        saldoModificado = saldo + monto;
                    }
                    else
                  if (OpcionTipoTransaccion == 2)
                    {
                        if (saldo < monto)
                        {
                            MessageBox.Show("El monto del retiro es mayor, al saldo actual de la cuenta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            saldoModificado = saldo - monto;
                        }
                    }
                IngresarTransaccion();
                ActualizarSaldo();
                cmbCodigoCuenta.Items.Clear();
                cmbCodigoTransaccion.Items.Clear();
                cmbCodigoMoneda.Items.Clear();
                cmbTransaccion.Items.Clear();
                txtDescripcion.Text = "";
                cmbMoneda.Items.Clear();
                txtMonto.Text = "";
                txtSaldoActual.Text = "";
                Codigo = controlador.codigoAutomatico();
                llenarComboCuenta();
                llenarComboTipoTransaccion();
                llenarComboTipoMoneda();
                 MostrarDatosDataGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("El monto ingresado no es valido", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }
    }
}
