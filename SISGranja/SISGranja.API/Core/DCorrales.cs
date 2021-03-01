using SISGranja.API.Services;
using SISGranja.Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SISGranja.API.Core
{
    public class DCorrales
    {
        #region MENSAJE SQL
        private void SqlCon_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            this.Mensaje_respuesta = e.Message;
        }
        #endregion

        #region VARIABLES
        private string mensaje_respuesta;
        public string Mensaje_respuesta
        {
            get
            {
                return mensaje_respuesta;
            }

            set
            {
                mensaje_respuesta = value;
            }
        }
        #endregion

        #region CONSTRUCTOR VACIO
        public DCorrales() { }
        #endregion

        #region MÉTODO INSERTAR
        public async Task<(string rpta, int id_corral)> InsertarCorral(Corrales corral)
        {
            int id_corral = 0;
            string consulta = "INSERT INTO Corrales VALUES (@Nombre_corral, @Descripcion_corral, @Estado_corral) " +
                "SET @Id_corral = SCOPE_IDENTITY(); ";

            //asignamos a una cadena string la variable rpta y la iniciamos en vacía
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
            SqlCon.FireInfoMessageEventOnUserErrors = true;
            //Capturador de errores
            try
            {
                SqlCon.ConnectionString = Connection.Cn;
                await SqlCon.OpenAsync();
                //Establecer comando
                SqlCommand SqlCmd = new SqlCommand
                {
                    Connection = SqlCon,
                    CommandText = consulta,
                    CommandType = CommandType.Text,
                };

                SqlParameter Id_corral = new SqlParameter
                {
                    ParameterName = "@Id_corral",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output,
                };
                SqlCmd.Parameters.Add(Id_corral);

                SqlParameter Nombre_corral = new SqlParameter
                {
                    ParameterName = "@Nombre_corral",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = corral.Nombre_corral,
                };
                SqlCmd.Parameters.Add(Nombre_corral);

                SqlParameter Descripcion_corral = new SqlParameter
                {
                    ParameterName = "@Descripcion_corral",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 200,
                    Value = corral.Descripcion_corral,
                };
                SqlCmd.Parameters.Add(Descripcion_corral);


                SqlParameter Estado_corral = new SqlParameter
                {
                    ParameterName = "@Estado_corral",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = corral.Estado_corral,
                };
                SqlCmd.Parameters.Add(Estado_corral);

                //Ejecutamos nuestro comando
                //Se puede ejecutar este metodo pero ya tenemos el mensaje que devuelve sql
                rpta = SqlCmd.ExecuteNonQuery() >= 1 ? "OK" : "NO se Ingreso el Registro";

                if (!rpta.Equals("OK"))
                {
                    if (this.Mensaje_respuesta != null)
                    {
                        rpta = this.Mensaje_respuesta;
                    }
                }
                else
                {
                    id_corral = Convert.ToInt32(SqlCmd.Parameters["@Id_corral"].Value);
                }
            }
            //Mostramos posible error que tengamos
            catch (SqlException ex)
            {
                rpta = ex.Message;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                //Si la cadena SqlCon esta abierta la cerramos
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return (rpta, id_corral);
        }
        #endregion

        #region MÉTODO INSERTAR
        public async Task<string> EditarCorral(int id_corral, Corrales corral)
        {
            string consulta = "UPDATE Corrales SET " +
                "Nombre_corral = @Nombre_corral, " +
                "Descripcion_corral = @Descripcion_corral, " +
                "Estado_corral = @Estado_corral " +
                "WHERE Id_corral = @Id_corral ";

            //asignamos a una cadena string la variable rpta y la iniciamos en vacía
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
            SqlCon.FireInfoMessageEventOnUserErrors = true;
            //Capturador de errores
            try
            {
                SqlCon.ConnectionString = Connection.Cn;
                await SqlCon.OpenAsync();
                //Establecer comando
                SqlCommand SqlCmd = new SqlCommand
                {
                    Connection = SqlCon,
                    CommandText = consulta,
                    CommandType = CommandType.Text,
                };

                SqlParameter Id_corral = new SqlParameter
                {
                    ParameterName = "@Id_corral",
                    SqlDbType = SqlDbType.Int,
                    Value = id_corral,
                };
                SqlCmd.Parameters.Add(Id_corral);

                SqlParameter Nombre_corral = new SqlParameter
                {
                    ParameterName = "@Nombre_corral",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = corral.Nombre_corral,
                };
                SqlCmd.Parameters.Add(Nombre_corral);

                SqlParameter Descripcion_corral = new SqlParameter
                {
                    ParameterName = "@Descripcion_corral",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 200,
                    Value = corral.Descripcion_corral,
                };
                SqlCmd.Parameters.Add(Descripcion_corral);


                SqlParameter Estado_corral = new SqlParameter
                {
                    ParameterName = "@Estado_corral",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = corral.Estado_corral,
                };
                SqlCmd.Parameters.Add(Estado_corral);

                //Ejecutamos nuestro comando
                //Se puede ejecutar este metodo pero ya tenemos el mensaje que devuelve sql
                rpta = SqlCmd.ExecuteNonQuery() >= 1 ? "OK" : "NO se Ingreso el Registro";

                if (!rpta.Equals("OK"))
                {
                    if (this.Mensaje_respuesta != null)
                    {
                        rpta = this.Mensaje_respuesta;
                    }
                }
            }
            //Mostramos posible error que tengamos
            catch (SqlException ex)
            {
                rpta = ex.Message;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                //Si la cadena SqlCon esta abierta la cerramos
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return rpta;
        }
        #endregion

        #region MÉTODO BUSCAR
        public async Task<(string rpta, DataTable dt)> BuscarCorrales(string tipo_busqueda, string texto_busqueda)
        {
            StringBuilder consulta = new StringBuilder();
            consulta.Append("SELECT * FROM Corrales ");

            if (tipo_busqueda.Equals("ID CORRAL"))
            {
                consulta.Append("WHERE Id_corral = @Texto_busqueda ");
            }
            else if (tipo_busqueda.Equals("NOMBRE CORRAL"))
            {
                consulta.Append("WHERE Nombre_corral = @Texto_busqueda ");
            }

            consulta.Append("ORDER BY Id_corral DESC ");

            DataTable dt = new DataTable();
            string rpta = "OK";
            SqlConnection SqlCon = new SqlConnection();
            SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
            SqlCon.FireInfoMessageEventOnUserErrors = true;
            //Capturador de errores
            try
            {
                SqlCon.ConnectionString = Connection.Cn;
                await SqlCon.OpenAsync();
                //Establecer comando
                SqlCommand SqlCmd = new SqlCommand
                {
                    Connection = SqlCon,
                    CommandText = consulta.ToString(),
                    CommandType = CommandType.Text,
                };

                SqlParameter Texto_busqueda = new SqlParameter
                {
                    ParameterName = "@Texto_busqueda",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = texto_busqueda,
                };
                SqlCmd.Parameters.Add(Texto_busqueda);

                SqlDataAdapter SqlData = new SqlDataAdapter(SqlCmd);
                SqlData.Fill(dt);

                if (dt.Rows.Count < 0)
                {
                    dt = null;
                }
            }
            //Mostramos posible error que tengamos
            catch (SqlException ex)
            {
                rpta = ex.Message;
                dt = null;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                dt = null;
            }
            finally
            {
                //Si la cadena SqlCon esta abierta la cerramos
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return (rpta, dt);
        }
        #endregion

    }
}