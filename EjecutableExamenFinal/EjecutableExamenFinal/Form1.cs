using CapaVistaSeguridad;
using CapaVista;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EjecutableExamenFinal
{
    public partial class Form1 : Form
    {
        clsFuncionesSeguridad seguridad = new clsFuncionesSeguridad();
        clsVistaBitacora bit = new clsVistaBitacora();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            frmLogin frm = new frmLogin();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txtUsuario.Text = frm.usuario();
            }
        }

        private void bancosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (seguridad.PermisosAcceso("1302", txtUsuario.Text) == 1)
            {
                bit.user(txtUsuario.Text);
                bit.insert("Ingreso a la aplicacion Cuenta Bancaria", 1302);
                MantenimientoBanco banco = new MantenimientoBanco(txtUsuario.Text, this);
                banco.MdiParent = this;
                banco.Show();
            }
            else
            {
                bit.user(txtUsuario.Text);
                bit.insert("Trato de ingresar a la aplicacion Bancos", 1302);
                MessageBox.Show("El Usuario No Cuenta Con Permisos De Acceso A La Aplicación");
            }
        }

        private void transaccionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (seguridad.PermisosAcceso("1307", txtUsuario.Text) == 1)
            {
                bit.user(txtUsuario.Text);
                bit.insert("Ingreso al proceso de Transacciones", 1307);
                Transacciones transaccion = new Transacciones();
                transaccion.MdiParent = this;
                transaccion.Show();
            }
            else
            {
                bit.user(txtUsuario.Text);
                bit.insert("Trato de ingresar al proceso de Transacciones", 1307);
                MessageBox.Show("El Usuario No Cuenta Con Permisos De Acceso A La Aplicación");
            }
        }

        private void cambioDeContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCambioContraseña frmCambioContraseña = new frmCambioContraseña(txtUsuario.Text);
            frmCambioContraseña.MdiParent = this;
            frmCambioContraseña.Show();
        }

        private void bitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MostrarBitacora mostrarBitacora = new MostrarBitacora();
            mostrarBitacora.MdiParent = this;
            mostrarBitacora.Show();
        }

        private void transaccionesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormularioReporteTransaccion formularioReporteTransaccion = new FormularioReporteTransaccion();
            formularioReporteTransaccion.MdiParent = this;
            formularioReporteTransaccion.Show();
        }

        private void ayudasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "AyudaFRM/AyudaFRM.chm", "MDI.html");
        }
    }
}
