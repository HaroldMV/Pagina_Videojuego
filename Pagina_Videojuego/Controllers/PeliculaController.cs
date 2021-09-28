using Pagina_Videojuego.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using System.IO;

using System.Linq;

using System.Web;

using System.Web.Mvc;

using System.Web.Services.Description;

namespace Pagina_Videojuego.Controllers
{
    public class PeliculaController : Controller
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager
                            .ConnectionStrings["cnx"].ConnectionString);

        List<Pelicula> ListPelicula()
        {
            List<Pelicula> aPelicula = new List<Pelicula>();
            SqlCommand cmd = new SqlCommand("SP_LISTAPELICULA", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aPelicula.Add(new Pelicula()
                {
                    idPelicula = dr[0].ToString(),
                    nombre = dr[1].ToString(),
                    direct = dr[2].ToString(),
                    duracion = dr[3].ToString(),
                    fechaEstreno = dr[4].ToString(),
                    precio = double.Parse(dr[5].ToString()),
                    stock = int.Parse(dr[6].ToString()),
                    genero = dr[7].ToString(),
                    foto = dr[8].ToString()
                });
            }
            dr.Close();
            cn.Close();
            return aPelicula;
        }

        List<PeliculaOriginal> ListPeliculaOriginal()
        {
            List<PeliculaOriginal> aPelicula = new List<PeliculaOriginal>();
            SqlCommand cmd = new SqlCommand("SP_LISTAPELICULAORIGINAL", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aPelicula.Add(new PeliculaOriginal()
                {
                    idPelicula = dr[0].ToString(),
                    nombre = dr[1].ToString(),
                    direct = dr[2].ToString(),
                    duracion = dr[3].ToString(),
                    fechaEstreno = dr[4].ToString(),
                    precio = double.Parse(dr[5].ToString()),
                    stock = int.Parse(dr[6].ToString()),
                    idgenero = dr[7].ToString(),
                    foto = dr[8].ToString()
                });
            }
            dr.Close();
            cn.Close();
            return aPelicula;
        }

        List<Pelicula> ListPeliculaxNombre(string nombre)
        {
            List<Pelicula> aPelicula = new List<Pelicula>();
            SqlCommand cmd = new SqlCommand("SP_LISTAPELICULAXNOMBRE", cn);
            cmd.Parameters.AddWithValue("@NOM", nombre);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aPelicula.Add(new Pelicula()
                {
                    idPelicula = dr[0].ToString(),
                    nombre = dr[1].ToString(),
                    direct = dr[2].ToString(),
                    duracion = dr[3].ToString(),
                    fechaEstreno = dr[4].ToString(),
                    precio = double.Parse(dr[5].ToString()),
                    stock = int.Parse(dr[6].ToString()),
                    genero = dr[7].ToString(),
                    foto = dr[8].ToString()
                });
            }
            dr.Close();
            cn.Close();
            return aPelicula;
        }

        List<GeneroPelicula> listGenero()
        {
            List<GeneroPelicula> aGeneroPeli = new List<GeneroPelicula>();
            SqlCommand cmd = new SqlCommand("SP_LISTADOGENEROPELI", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                GeneroPelicula objC = new GeneroPelicula()
                {
                    idGenero = dr[0].ToString(),
                    nombre = dr[1].ToString(),
                };
                aGeneroPeli.Add(objC);
            }

            dr.Close();
            cn.Close();
            return aGeneroPeli;
        }

        string codigoCorrelativo()
        {
            string codigo=null;
            SqlCommand cmd = new SqlCommand("SP_ULTIMOCODIGOPELICULA", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                codigo = dr[0].ToString();
            }
            dr.Close();
            cn.Close();

            string s = codigo.Substring(4, 6);
            int s2 = int.Parse(s);
            if (s2 < 9)
            {
                s2++;
                codigo = "PELI00000" + s2;
            }
            else if (s2>=9)
            {
                s2++;
                codigo = "PELI0000" + s2;
            }
            else if (s2>=99)
            {
                s2++;
                codigo = "PELI000" + s2;
            }
            else if (s2 >= 999)
            {
                s2++;
                codigo = "PELI00" + s2;
            }
            else if (s2 >= 9999)
            {
                s2++;
                codigo = "PELI0" + s2;
            }
            else if (s2 >= 99999)
            {
                s2++;
                codigo = "PELI" + s2;
            }

            return codigo;
        }

        /*Vistas del controlador Pelicula*/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult listadoPelicula()
        {
            return View(ListPelicula());
        }

        public ActionResult listadoPeliculaPag(int p = 0)
        {
            List<Pelicula> aPelicula = ListPelicula();
            int filas = 10;
            int n = aPelicula.Count;
            int pag = n % filas > 0 ? n / filas + 1 : n / filas;

            ViewBag.pag = pag;
            ViewBag.p = p;
            return View(aPelicula.Skip(p * filas).Take(filas));

        }

        public ActionResult detallePelicula(string id)
        {
            Pelicula objP = ListPelicula().Where(p => p.idPelicula == id).FirstOrDefault();
            return View(objP);
        }
        /*VR*/
        string CRUD(string proceso, List<SqlParameter> p)
        {
            string mensaje = "Película No Actualizado";
            cn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(proceso, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(p.ToArray());
                int n = cmd.ExecuteNonQuery();
                mensaje = n + "Película Actualizado";
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                cn.Close();
            }
            return mensaje; /*VR*/
        }

        public ActionResult registroPelicula()
        {
            ViewBag.codigo = codigoCorrelativo();
            ViewBag.genero = new SelectList(listGenero(), "idGenero", "nombre");
            return View(new PeliculaOriginal());
        }

        [HttpPost]
        public ActionResult registroPelicula(PeliculaOriginal objP, HttpPostedFileBase f)
        {
            if(f == null)
            {
                ViewBag.mensaje = "Selecciona una foto";
                return View(objP);
            }
            if (Path.GetExtension(f.FileName) != ".jpg")
            {
                ViewBag.mensaje = "Debe ser un jpg";
                return View(objP);
            }

            /*VR*/
            string CRUD(string proceso, List<SqlParameter> p)
            {
                string mensaje = "Registro Película Erroneo";
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand(proceso, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(p.ToArray());
                    int n = cmd.ExecuteNonQuery();
                    mensaje = n + " Registro Película Exitoso";
                }
                catch (SqlException ex)
                {
                    mensaje = ex.Message; 
                }
                finally
                {
                    cn.Close();
                }
                return mensaje; /**/
            }

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@IDPELI",SqlDbType=SqlDbType.Char, Value=objP.idPelicula},
                new SqlParameter(){ParameterName="@NOMPELI",SqlDbType=SqlDbType.VarChar, Value=objP.nombre},
                new SqlParameter(){ParameterName="@DIRECT",SqlDbType=SqlDbType.VarChar, Value=objP.direct},
                new SqlParameter(){ParameterName="@DURACI",SqlDbType=SqlDbType.VarChar, Value=objP.duracion},
                new SqlParameter(){ParameterName="@FECHES",SqlDbType=SqlDbType.VarChar, Value=objP.fechaEstreno},
                new SqlParameter(){ParameterName="@PREPELI",SqlDbType=SqlDbType.SmallMoney, Value=objP.precio},
                new SqlParameter(){ParameterName="@STOPELI",SqlDbType=SqlDbType.Int, Value=objP.stock},
                new SqlParameter(){ParameterName="@GENPELI",SqlDbType=SqlDbType.Char, Value=objP.idgenero},
                new SqlParameter(){ParameterName="@FOTO",SqlDbType=SqlDbType.VarChar, 
                            Value="~/Images/movies/"+Path.GetFileName(f.FileName)}
            };
            ViewBag.mensaje = CRUD("SP_MANTENIMIENTOPELICULA", parameters);
            f.SaveAs(Path.Combine(Server.MapPath("~/Images/movies/"),
                Path.GetFileName(f.FileName)));

            ViewBag.genero = new SelectList(listGenero(), "idGenero", "nombre");
            return RedirectToAction("listadoPelicula");
            /*
            if (!ModelState.IsValid)
            {
                return View(objV);
            }
            cn.Open();
            ViewBag.mensaje = "";
            SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                SqlCommand cmd = new SqlCommand("SP_MANTENIMIENTOPELICULA", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDEPELI", objV.idPelicula);
                cmd.Parameters.AddWithValue("@NOMPELI", objV.nombre);
                cmd.Parameters.AddWithValue("@DIRECT", objV.direct);
                cmd.Parameters.AddWithValue("@DURACI", objV.duracion);
                cmd.Parameters.AddWithValue("@FECHES", objV.fechaEstreno);
                cmd.Parameters.AddWithValue("@PREPELI", objV.precio);
                cmd.Parameters.AddWithValue("@STOPELI", objV.stock);
                cmd.Parameters.AddWithValue("@GENPELI", objV.genero);
                cmd.Parameters.AddWithValue("@FOTO", objV.foto);
                int n = cmd.ExecuteNonQuery();
                tr.Commit();
                ViewBag.mensaje = n.ToString() + "PELICULA REGISTRADA!!";
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;
                tr.Rollback();
            }
            cn.Close();

            return View(objV);*/
        }

        public ActionResult editarPelicula(string id)
        {
            PeliculaOriginal peliO = ListPeliculaOriginal().Where(x => x.idPelicula == id).FirstOrDefault();

            ViewBag.genero = new SelectList(listGenero(), "idGenero", "nombre");
            return View(peliO);
        }

        [HttpPost]
        public ActionResult editarPelicula(PeliculaOriginal objP, HttpPostedFileBase f)
        {
            if (f == null)
            {
                return View(objP);
            }
            if (Path.GetExtension(f.FileName) != ".jpg")
            {
                return View(objP);
            }
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@IDPELI",SqlDbType=SqlDbType.Char, Value=objP.idPelicula},
                new SqlParameter(){ParameterName="@NOMPELI",SqlDbType=SqlDbType.VarChar, Value=objP.nombre},
                new SqlParameter(){ParameterName="@DIRECT",SqlDbType=SqlDbType.VarChar, Value=objP.direct},
                new SqlParameter(){ParameterName="@DURACI",SqlDbType=SqlDbType.VarChar, Value=objP.duracion},
                new SqlParameter(){ParameterName="@FECHES",SqlDbType=SqlDbType.VarChar, Value=objP.fechaEstreno},
                new SqlParameter(){ParameterName="@PREPELI",SqlDbType=SqlDbType.SmallMoney, Value=objP.precio},
                new SqlParameter(){ParameterName="@STOPELI",SqlDbType=SqlDbType.Int, Value=objP.stock},
                new SqlParameter(){ParameterName="@GENPELI",SqlDbType=SqlDbType.Char, Value=objP.idgenero},
                new SqlParameter(){ParameterName="@FOTO",SqlDbType=SqlDbType.VarChar, Value="~/Images/movies/"+Path.GetFileName(f.FileName)}
            };
            ViewBag.mensaje = CRUD("SP_MANTENIMIENTOPELICULA", parameters);
            f.SaveAs(Path.Combine(Server.MapPath("~/Images/movies/"),
                Path.GetFileName(f.FileName)));

            ViewBag.genero = new SelectList(listGenero(), "idGenero", "nombre", objP.idPelicula);
            return RedirectToAction("listadoPeliculaPag");
        }

        public ActionResult eliminarPelicula(string id)
        {
            Pelicula objP = ListPelicula().Where(x => x.idPelicula == id).FirstOrDefault();
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@ID",SqlDbType=SqlDbType.Char, Value=objP.idPelicula},
            };
            CRUD("SP_ELIMINARPELICULA", parameters);
            return RedirectToAction("listadoPelicula");
        }

        public ActionResult listadoPeliculaxNombre()
        {
            return View(ListPeliculaxNombre(""));
        }

        [HttpPost]
        public ActionResult listadoPeliculaxNombre(string nombre)
        {
            return View(ListPeliculaxNombre(nombre));
        }

    }
}