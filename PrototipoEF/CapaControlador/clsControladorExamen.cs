using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaModelo;

namespace CapaControlador
{
    public class clsControladorExamen
    {
        clsSentenciasExamen sentencias = new clsSentenciasExamen();
        public int codigoAutomatico()
        {
            int codigo = sentencias.procCodigoA();
            return codigo;
        }
        public List<string> Llenar(string tabla, string campo)
        {
            List<string> id = sentencias.llenarCombo(tabla, campo);

            return id;
        }

        public bool IngresarTransaccion(int codigo, int codigoCuenta, string fecha, int codigoTipoTransaccion, int codigoTipoMoneda, string monto, string descripcion)
        {
            if (sentencias.IngresoDeTransaccion(codigo,codigoCuenta,fecha,codigoTipoTransaccion,codigoTipoMoneda,monto,descripcion))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ActualizarSaldo(float saldo, int numeroCuenta)
        {
            if (sentencias.ActualizarSaldo(saldo,numeroCuenta))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public float ObtnerSaldo(int numeroCuenta)
        {
            float saldo = 0;
            saldo = float.Parse(sentencias.ObtenerSaldo(numeroCuenta));
            return saldo;
        }

        public DataTable llenarTbl()
        {
            OdbcDataAdapter dt = sentencias.llenarTbl();
            DataTable table = new DataTable();
            dt.Fill(table);
            return table;
        }
    }
}
