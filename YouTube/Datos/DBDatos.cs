﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static System.Configuration.ConfigurationManager;

namespace YouTube.Datos
{
    public class DBDatos
    {
        public static string preconex = ConnectionStrings["stringConexion"].ConnectionString;

        public static Respuesta Ejecutar(string nombreProcedimiento, List<Parametro> parametros, string stringConexion = "")
        {
            Respuesta respuesta = new Respuesta();
            respuesta.message = "";
            SqlConnection conexion = new SqlConnection(string.IsNullOrEmpty(stringConexion) ? preconex : stringConexion);
            conexion.Open();

            try
            {
                SqlCommand cmd = new SqlCommand(nombreProcedimiento, conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                if (parametros != null)
                {
                    foreach (var parametro in parametros)
                    {
                        if (!parametro.Salida)
                        { cmd.Parameters.AddWithValue(parametro.Nombre, parametro.Valor); }
                        else
                        { cmd.Parameters.Add(parametro.Nombre, SqlDbType.VarChar, 100).Direction = ParameterDirection.Output; }
                    }
                }

                int e = cmd.ExecuteNonQuery();

                for (int i = 0; i < parametros.Count; i++)
                {
                    if (cmd.Parameters[i].Direction == ParameterDirection.Output)
                    {
                        string mensaje = cmd.Parameters[i].Value.ToString();

                        if (!string.IsNullOrEmpty(mensaje))
                        {
                            respuesta.message = mensaje;
                        }
                    }
                }

                respuesta.exito = e > 0 ? true : false;
                respuesta.message = e > 0 ? "found data" : "No se encontro el comprobante para actualizarlo";
            }
            catch (Exception EX)
            {
                respuesta.exito = false;
                respuesta.message = EX.Message;
            }
            finally
            {
                conexion.Close();
            }

            return respuesta;
        }

        public static Respuesta Listar(string nombreProcedimiento, List<Parametro> parametros = null, string stringConexion = "")
        {
            Respuesta respuesta = new Respuesta();

            SqlConnection conexion = new SqlConnection(string.IsNullOrEmpty(stringConexion) ? preconex : stringConexion);
            conexion.Open();

            try
            {

                SqlCommand cmd = new SqlCommand(nombreProcedimiento, conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                if (parametros != null)
                {
                    foreach (var parametro in parametros)
                    {
                        cmd.Parameters.AddWithValue(parametro.Nombre, parametro.Valor);
                    }
                }
                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                respuesta.exito = true;
                respuesta.message = tabla.Rows.Count > 0 ? "found data" : "not found data";
                respuesta.result = tabla;
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.exito = false;
                respuesta.message = ex.Message;
                return respuesta;
            }
            finally
            {
                conexion.Close();
            }
        }
    }
}