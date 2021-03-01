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
    public class DAnimales
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
        public DAnimales() { }
        #endregion

        #region MÉTODO INSERTAR
        public async Task<(string rpta, int id_animal)> InsertarAnimal(Animales animal)
        {
            int id_animal = 0;
            string consulta = "INSERT INTO Animales VALUES (@Nombre_animal, @Descripcion_animal, @Estado_animal) " +
                "SET @Id_animal = SCOPE_IDENTITY(); ";

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

                SqlParameter Id_animal = new SqlParameter
                {
                    ParameterName = "@Id_animal",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output,
                };
                SqlCmd.Parameters.Add(Id_animal);

                SqlParameter Nombre_animal = new SqlParameter
                {
                    ParameterName = "@Nombre_animal",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = animal.Nombre_animal,
                };
                SqlCmd.Parameters.Add(Nombre_animal);

                SqlParameter Descripcion_animal = new SqlParameter
                {
                    ParameterName = "@Descripcion_animal",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 200,
                    Value = animal.Descripcion_animal,
                };
                SqlCmd.Parameters.Add(Descripcion_animal);

                SqlParameter Estado_animal = new SqlParameter
                {
                    ParameterName = "@Estado_animal",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = animal.Estado_animal,
                };
                SqlCmd.Parameters.Add(Estado_animal);

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
                    //Recuperamos el id autogenerado
                    id_animal = Convert.ToInt32(SqlCmd.Parameters["@Id_animal"].Value);
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
            return (rpta, id_animal);
        }
        #endregion

        #region MÉTODO EDITAR
        public async Task<string> EditarAnimal(int id_animal, Animales animal)
        {
            string consulta = "UPDATE Animales SET " +
                "Nombre_animal = @Nombre_animal, " +
                "Descripcion_animal = @Descripcion_animal, " +
                "Estado_animal = @Estado_animal " +
                "WHERE Id_animal = @Id_animal ";

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

                SqlParameter Id_animal = new SqlParameter
                {
                    ParameterName = "@Id_animal",
                    SqlDbType = SqlDbType.Int,
                    Value = id_animal,
                };
                SqlCmd.Parameters.Add(Id_animal);

                SqlParameter Nombre_animal = new SqlParameter
                {
                    ParameterName = "@Nombre_animal",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = animal.Nombre_animal,
                };
                SqlCmd.Parameters.Add(Nombre_animal);

                SqlParameter Descripcion_animal = new SqlParameter
                {
                    ParameterName = "@Descripcion_animal",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 200,
                    Value = animal.Descripcion_animal,
                };
                SqlCmd.Parameters.Add(Descripcion_animal);

                SqlParameter Estado_animal = new SqlParameter
                {
                    ParameterName = "@Estado_animal",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = animal.Estado_animal,
                };
                SqlCmd.Parameters.Add(Estado_animal);

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
        public async Task<(string rpta, DataTable dt)> BuscarAnimal(string tipo_busqueda, string texto_busqueda)
        {
            StringBuilder consulta = new StringBuilder();
            consulta.Append("SELECT * FROM Animales ");

            if (tipo_busqueda.Equals("ID ANIMAL"))
            {
                consulta.Append("WHERE Id_animal = @Texto_busqueda ");
            }
            else if (tipo_busqueda.Equals("NOMBRE ANIMAL"))
            {
                consulta.Append("WHERE Nombre_animal = @Texto_busqueda ");
            }

            consulta.Append("ORDER BY Id_animal DESC ");

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