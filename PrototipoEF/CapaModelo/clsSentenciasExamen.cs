using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class clsSentenciasExamen
    {
        conexion cn = new conexion();
       public int procCodigoA()
        {
            int numero, codigoA;
            string contador = "SELECT count(pk_id_transaccion) FROM TRANSACCION ";
            OdbcCommand comando = new OdbcCommand(contador, cn.Conexion());
            numero = Convert.ToInt32(comando.ExecuteScalar());
     
            if (numero == 0)
            {
                codigoA = 1;
               
            }
            else
            {
                codigoA = numero + 1;
            }
            return codigoA;
        }

        public List<string> llenarCombo(string tabla, string campo)
        {
            List<string> id = new List<string>();
            string sql = "SELECT " + campo + " FROM " + tabla + "";

            try
            {
                OdbcCommand command = new OdbcCommand(sql, cn.Conexion());
                OdbcDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    id.Add(reader.GetValue(0).ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
            return id;
        }
        public bool IngresoDeTransaccion(int codigo,int codigoCuenta,string fecha,int codigoTipoTransaccion, int codigoTipoMoneda, string monto,string descripcion)
        {
            try
            {
                //se realiza la consulta de insertar en tabla pelicula con sus respectivos campos
                string sql = "INSERT INTO TRANSACCION (pk_id_transaccion,fk_id_numero_cuenta_bancaria,fecha_transaccion,fk_id_tipo_transaccion,fk_id_tipo_moneda,monto_transaccion,descripcion_transaccion) " +
                "VALUES (" + codigo + "," + codigoCuenta + ",'" + fecha + "'," + codigoTipoTransaccion + ",'" + codigoTipoMoneda + "', " + monto + ",'" + descripcion + "' )";
                OdbcCommand command = new OdbcCommand(sql, cn.Conexion());
                OdbcDataReader reader = command.ExecuteReader();
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }

        }

        public bool ActualizarSaldo(float saldo, int numeroCuenta)
        {
            try
            {             
                string sql = "UPDATE cuenta_bancaria set saldo_cuenta_bancaria = " + saldo + " where pk_id_numero_cuenta_bancaria = " + numeroCuenta + " ";
                OdbcCommand command = new OdbcCommand(sql, cn.Conexion());
                OdbcDataReader reader = command.ExecuteReader();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public string ObtenerSaldo(int numeroCuenta)
        {
            string saldo="";
            string sql = "SELECT * FROM CUENTA_BANCARIA WHERE pk_id_numero_cuenta_bancaria = " + numeroCuenta;
            OdbcCommand command = new OdbcCommand(sql, cn.Conexion());
            OdbcDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                  
                saldo = (reader.GetDouble(4)).ToString();
            }
            return saldo;
        }
        public OdbcDataAdapter llenarTbl()
        {    
            string sql = " SELECT T.pk_id_transaccion AS CODIGO, C.pk_id_numero_cuenta_bancaria AS CUENTA_BANCARIA , T.monto_transaccion AS MONTO,TT.nombre_tipo_transaccion AS TIPO_DE_TRANSACCION ,T.descripcion_transaccion AS DESCRIPCION, T.FECHA_TRANSACCION FROM transaccion T ,cuenta_bancaria C ,tipo_transaccion TT WHERE T.fk_id_numero_cuenta_bancaria = C.pk_id_numero_cuenta_bancaria AND T.fk_id_tipo_transaccion = TT.pk_id_tipo_transaccion ";
            OdbcDataAdapter dataTable = new OdbcDataAdapter(sql, cn.Conexion());
            return dataTable;
        }
    }
}
