using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldemGym.Acceso.Datos.DAL
{
    class aDatosAcces
    {
        private string RutaDB { set; get; }

        public aDatosAcces(string rutaDB)
        {
            this.RutaDB = rutaDB;
        }

        private System.Data.Odbc.OdbcConnection ConnectToAccessODBC()
        {
            System.Data.Odbc.OdbcConnection conn = new System.Data.Odbc.OdbcConnection();
            // TODO: Modificar la cadena de conexion
            conn.ConnectionString = @"driver={Microsoft Access Driver (*.mdb)};dbq=" + RutaDB + ";Exclusive=0;uid=admin;pwd=;";
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to connect to data source {0}" + ex.Message);
            }
            return conn;
        }

        private System.Data.OleDb.OleDbConnection ConnectToAccess()
        {
            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection();
            // TODO: Modificar la cadena de conexion
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                @"Data source=" + RutaDB + ";User Id=admin;Password=;Jet OLEDB:Database Password=;";
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to connect to data source {0}" + ex.Message);
            }
            return conn;
        }

        public System.Data.DataTable TraerDataTable(string sql)
        {
            System.Data.DataTable tabla = new System.Data.DataTable();
            using (System.Data.OleDb.OleDbConnection conn = ConnectToAccess())
            {
                System.Data.OleDb.OleDbCommand com = new System.Data.OleDb.OleDbCommand(conn.ConnectionString);
                com.Connection = conn;
                com.CommandText = sql;//consulta sql
                System.Data.OleDb.OleDbDataAdapter adaptador = new System.Data.OleDb.OleDbDataAdapter(com.CommandText, com.Connection.ConnectionString);
                adaptador.Fill(tabla);
            }
            return tabla;
        }

        public System.Data.DataTable TraerDataTableODBC(string sql)
        {
            System.Data.DataTable tabla = new System.Data.DataTable();
            using (System.Data.Odbc.OdbcConnection conn = ConnectToAccessODBC())
            {
                System.Data.Odbc.OdbcCommand com = new System.Data.Odbc.OdbcCommand(conn.ConnectionString);
                com.Connection = conn;
                com.CommandText = sql;//consulta sql
                System.Data.Odbc.OdbcDataAdapter adaptador = new System.Data.Odbc.OdbcDataAdapter(com.CommandText, com.Connection.ConnectionString);
                adaptador.Fill(tabla);
            }
            return tabla;
        }
    }
}
