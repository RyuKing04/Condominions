using ApplicationCore.Service;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryUsuario : IRepositoryUsuario
    {
        public IEnumerable<Usuario> GetUsuario()
        {
            try
            {
                IEnumerable<Usuario> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    //sacar todos menos el admin
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Usuario.Where(l => l.Rol != "Administrador").Where(l=>l.Estado == true).ToList<Usuario>();
                }
                return lista;

            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Usuario GetUsuarioByID(int id)
        {
            Usuario oUsuario = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oUsuario = ctx.Usuario.Include("Residencia").Where(c => c.Id == id).FirstOrDefault();
                }
                return oUsuario;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Usuario CambiarEstado(int Id)
        {
            int retorno = 0;
            Usuario oUsuario = null;
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "(local)";
                builder.UserID = "sa";
                builder.Password = "123456";
                builder.InitialCatalog = "Condominions";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    String sql = "UPDATE Usuario SET Estado = 0 WHERE Id = " + Id;

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                retorno = 1;
                            }
                        }
                    }
                }

                if (retorno >= 0)
                    oUsuario = GetUsuarioByID(Id);

                return oUsuario;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }


        public Usuario Save(Usuario usuario)
        {
            int retorno = 0;
            Usuario oUsuario = null;
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "(local)";
                builder.UserID = "sa";
                builder.Password = "123456";
                builder.InitialCatalog = "Condominions";



                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    String sql = "INSERT into Usuario(Id,Nombre,Apellido, Rol, Contrasenna, Estado, Email, FechaNacimiento) VALUES("
                                    + usuario.Id + ",'" + usuario.Nombre + "','" + usuario.Apellido + "', 'Usuario', dbo.ENCRIPTA_CONTRASENA('"
                                    + usuario.Contrasenna1 + "'), 1, '" + usuario.Email + "','" + usuario.FechaNacimiento + "')";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                retorno = 1;
                            }
                        }
                    }
                }

                if (retorno >= 0)
                    oUsuario = GetUsuarioByID(usuario.Id);

                return oUsuario;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Usuario GetUsuario(string email, string password)
        {
            Usuario oUsuario = null;
            string contra = "";

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "(local)";
                builder.UserID = "sa";
                builder.Password = "123456";
                builder.InitialCatalog = "Condominions";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    String sql = "SELECT id,email,dbo.DESENCRIPTA_CONTRASENA(contrasenna) as contrasena FROM Usuario where email = '" + email + "' AND Estado = 1";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                oUsuario = new Usuario();
                                oUsuario.Id = reader.GetInt32(0);
                                oUsuario.Email = reader.GetString(1);
                                contra = reader.GetString(2);
                            }
                        }
                    }
                }

                if (email.Equals(oUsuario.Email) && password.Equals(contra))
                {
                    return GetUsuarioByID(oUsuario.Id);
                }
                else
                {
                    return null;
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }
    }
}
