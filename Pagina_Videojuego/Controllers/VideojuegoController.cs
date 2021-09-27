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

namespace Pagina_Videojuego.Controllers
{
    public class VideojuegoController : Controller
    {

        SqlConnection cn = new SqlConnection(ConfigurationManager.
            ConnectionStrings["cnx"].ConnectionString);

        List<Videojuego> ListVideoJuego()
        {
            List<Videojuego> aVideoJuegos = new List<Videojuego>();
            SqlCommand cmd = new SqlCommand("SP_LISTAVIDEOJUEGO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aVideoJuegos.Add(new Videojuego()
                {
                    idVid = dr[0].ToString(),
                    nomVide = dr[1].ToString(),
                    platafor = dr[2].ToString(),
                    desarro = dr[3].ToString(),
                    fechlan = dr[4].ToString(),
                    precio = double.Parse(dr[5].ToString()),
                    stock = int.Parse(dr[6].ToString()),
                    portada = dr[7].ToString(),
                    genero = dr[8].ToString()
                });
            }
            dr.Close();
            cn.Close();
            return aVideoJuegos;
        }

        List<VideojuegoOriginal> ListVideoJuegoOriginal()
        {
            List<VideojuegoOriginal> aVideoJuegos = new List<VideojuegoOriginal>();
            SqlCommand cmd = new SqlCommand("SP_LISTAVIDEOJORIGINAL", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aVideoJuegos.Add(new VideojuegoOriginal()
                {
                    idVid = dr[0].ToString(),
                    nomVide = dr[1].ToString(),
                    platafor = dr[2].ToString(),
                    desarro = dr[3].ToString(),
                    fechlan = dr[4].ToString(),
                    precio = double.Parse(dr[5].ToString()),
                    stock = int.Parse(dr[6].ToString()),
                    portada = dr[7].ToString(),
                    genero = dr[8].ToString()
                });
            }
            dr.Close();
            cn.Close();
            return aVideoJuegos;
        }

        List<Videojuego> ListVideoJuegoxNombre(string nombre)
        {
            List<Videojuego> aVideoJuegos = new List<Videojuego>();
            SqlCommand cmd = new SqlCommand("SP_LISTAVIDEOJUEGOXNOMBRE", cn);
            cmd.Parameters.AddWithValue("@NOM", nombre);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aVideoJuegos.Add(new Videojuego()
                {
                    idVid = dr[0].ToString(),
                    nomVide = dr[1].ToString(),
                    platafor = dr[2].ToString(),
                    desarro = dr[3].ToString(),
                    fechlan = dr[4].ToString(),
                    precio = double.Parse(dr[5].ToString()),
                    stock = int.Parse(dr[6].ToString()),
                    portada = dr[7].ToString(),
                    genero = dr[8].ToString()
                });
            }
            dr.Close();
            cn.Close();
            return aVideoJuegos;
        }

        List<GeneroVideojuego> listGeneroVideojuego()
        {
            List<GeneroVideojuego> aGeneroVideo = new List<GeneroVideojuego>();
            SqlCommand cmd = new SqlCommand("SP_LISTAGENERO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                GeneroVideojuego objC = new GeneroVideojuego()
                {
                    idGenero = dr[0].ToString(),
                    nombre = dr[1].ToString(),
                };
                aGeneroVideo.Add(objC);
            }

            dr.Close();
            cn.Close();
            return aGeneroVideo;
        }

        string codigoCorrelativo()
        {
            string codigo = null;
            SqlCommand cmd = new SqlCommand("SP_ULTIMOCODIGOVIDEOJUEGO", cn);
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
                codigo = "VJUE00000" + s2;
            }
            else if (s2 >= 9)
            {
                s2++;
                codigo = "VJUE0000" + s2;
            }
            else if (s2 >= 99)
            {
                s2++;
                codigo = "VJUE000" + s2;
            }
            else if (s2 >= 999)
            {
                s2++;
                codigo = "VJUE00" + s2;
            }
            else if (s2 >= 9999)
            {
                s2++;
                codigo = "VJUE0" + s2;
            }
            else if (s2 >= 99999)
            {
                s2++;
                codigo = "VJUE" + s2;
            }

            return codigo;
        }

        string CRUD(string proceso, List<SqlParameter> p)
        {
            string mensaje = "No se registro";
            cn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(proceso, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(p.ToArray());
                int n = cmd.ExecuteNonQuery();
                mensaje = n + " Registro actualizado";
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                cn.Close();
            }
            return mensaje;
        }

        public ActionResult ComprasVideojuegos()
        {
            if (Session["carrito"] == null)
            {
                Session["carrito"] = new List<ItemJuego>();
            }
            return View(ListVideoJuego());
        }

        public ActionResult SeleccionaVideojuego(string id)
        {
            Videojuego objV = ListVideoJuego().Where(v => v.idVid == id).FirstOrDefault();
            return View(objV);
        }

        public ActionResult agregarVideojuego(String id, int cant = 0)
        {
            var miVideoJuego = ListVideoJuego().Where(p => p.idVid == id).FirstOrDefault();


            ItemJuego objI = new ItemJuego()
            {
                idVid = miVideoJuego.idVid,
                nomVide = miVideoJuego.nomVide,
                precio = miVideoJuego.precio,
                cantidad = cant,
                portada = miVideoJuego.portada
            };

            var miCarrito = (List<ItemJuego>)Session["carrito"];
            miCarrito.Add(objI);
            Session["carrito"] = miCarrito;
            return RedirectToAction("ComprasVideojuegos");
        }

        public ActionResult comprar()
        {
            if (Session["carrito"] == null)
            {
                return RedirectToAction("ComprasVideojuegos");
            }

            var miCarrito = (List<ItemJuego>)Session["carrito"];
            ViewBag.total = miCarrito.Sum(s => s.subtotal);
            return View(miCarrito);
        }


        public ActionResult eliminaVideojuego(String id)
        {
            if (id == null) return RedirectToAction("ComprasVideojuegos");
            var miCarrito = (List<ItemJuego>)Session["carrito"];
            var item = miCarrito.Where(i => i.idVid == id).FirstOrDefault();
            miCarrito.Remove(item);

            Session["carrito"] = miCarrito;
            return RedirectToAction("comprar");
        }

        /********************CRUD*************************/

        public ActionResult listadoVideojuego()
        {
            return View(ListVideoJuego());
        }

        public ActionResult listaVideojuegoPag(int v = 0)
        {
            List<Videojuego> aVideoJuegos = ListVideoJuego();
            int filas = 5;
            int n = aVideoJuegos.Count;
            int pag = n % filas > 0 ? n / filas + 1 : n / filas;

            ViewBag.pag = pag;
            ViewBag.v = v;
            return View(aVideoJuegos.Skip(v * filas).Take(filas));
        }

        public ActionResult listadoVideojuegoxNombre()
        {
            return View(ListVideoJuegoxNombre(""));
        }

        [HttpPost]
        public ActionResult listadoVideojuegoxNombre(string nombre)
        {
            return View(ListVideoJuegoxNombre(nombre));
        }

        public ActionResult detalleVideojuego(string id)
        {
            Videojuego objP = ListVideoJuego().Where(p => p.idVid == id).FirstOrDefault();
            return View(objP);
        }

        public ActionResult registroVideoJuego()
        {
            ViewBag.codigo = codigoCorrelativo();
            ViewBag.genero = new SelectList(listGeneroVideojuego(), "idGenero", "nombre");
            return View(new VideojuegoOriginal());
        }

        [HttpPost]
        public ActionResult registroVideojuego(VideojuegoOriginal objV, HttpPostedFile f)
        {
            if (!ModelState.IsValid)
            {
                return View(objV);
            }
            cn.Open();
            ViewBag.mensaje = "";

            if (f == null)
            {
                ViewBag.mensaje = "Seleccione una imagen";
                return View(objV);
            }

            if (Path.GetExtension(f.FileName) != ".jpg")
            {
                ViewBag.mensaje = "Debe ser .JPG";
                return View(objV);
            }
            SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                SqlCommand cmd = new SqlCommand("SP_MANTENIMIENTOVIDEOJUEGO", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDVID", objV.idVid);
                cmd.Parameters.AddWithValue("@NOMVID", objV.nomVide);
                cmd.Parameters.AddWithValue("@PLAT", objV.platafor);
                cmd.Parameters.AddWithValue("@DESA", objV.desarro);
                cmd.Parameters.AddWithValue("@FECVID", objV.fechlan);
                cmd.Parameters.AddWithValue("@PREVID", objV.precio);
                cmd.Parameters.AddWithValue("@STOVID", objV.stock);
                cmd.Parameters.AddWithValue("@PORTAVID", "~/Images/portada_juego/" + Path.GetFileName(f.FileName));
                f.SaveAs(Path.Combine(Server.MapPath("~/Images/portada_juego/"), Path.GetFileName(f.FileName)));
                cmd.Parameters.AddWithValue("@GENVID", objV.genero);
                int n = cmd.ExecuteNonQuery();
                tr.Commit();
                ViewBag.mensaje = n.ToString() + "Videojuego Registrado Exitosamente";
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;
                tr.Rollback();
            }
            cn.Close();
            ViewBag.pais = new SelectList(listGeneroVideojuego(), "codigo", "nombre", objV.genero);
            return RedirectToAction("listadoVideojuego");
        }

        public ActionResult editarVideojuego(string id)
        {
            VideojuegoOriginal videO = ListVideoJuegoOriginal().Where(x => x.idVid == id).FirstOrDefault();

            ViewBag.genero = new SelectList(listGeneroVideojuego(), "idGenero", "nombre");
            return View(videO);
        }

        [HttpPost]
        public ActionResult editarVideojuego(VideojuegoOriginal objV, HttpPostedFileBase f)
        {
            if (f == null)
            {
                return View(objV);
            }
            if (Path.GetExtension(f.FileName) != ".jpg")
            {
                return View(objV);
            }
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@IDVID",SqlDbType=SqlDbType.Char, Value=objV.idVid},
                new SqlParameter(){ParameterName="@NOMVID",SqlDbType=SqlDbType.VarChar, Value=objV.nomVide},
                new SqlParameter(){ParameterName="@PLAT",SqlDbType=SqlDbType.VarChar, Value=objV.platafor},
                new SqlParameter(){ParameterName="@DESA",SqlDbType=SqlDbType.VarChar, Value=objV.desarro},
                new SqlParameter(){ParameterName="@FECVID",SqlDbType=SqlDbType.VarChar, Value=objV.fechlan},
                new SqlParameter(){ParameterName="@PREVID",SqlDbType=SqlDbType.SmallMoney, Value=objV.precio},
                new SqlParameter(){ParameterName="@STOVID",SqlDbType=SqlDbType.Int, Value=objV.stock},
                new SqlParameter(){ParameterName="@PORTA",SqlDbType=SqlDbType.VarChar, Value="~/Images/portada_juego/"+Path.GetFileName(f.FileName)},
                new SqlParameter(){ParameterName="@GENVID",SqlDbType=SqlDbType.Char, Value=objV.genero}
            };
            ViewBag.mensaje = CRUD("SP_MANTENIMIENTOVIDEOJUEGO", parameters);
            f.SaveAs(Path.Combine(Server.MapPath("~/Images/portada_juego/"),
                Path.GetFileName(f.FileName)));

            ViewBag.genero = new SelectList(listGeneroVideojuego(), "idGenero", "nombre", objV);
            return RedirectToAction("listadoVideojuego");
        }

        public ActionResult CRUDeliminaVideojuego(string id)
        {
            Videojuego objE = ListVideoJuego().Where(e => e.idVid == id).FirstOrDefault();

            List<SqlParameter> lista = new List<SqlParameter>() {
            new SqlParameter(){ ParameterName="@IDE",SqlDbType=SqlDbType.VarChar,
            Value=objE.idVid }
 };
            SqlCommand cmd = new SqlCommand("SP_ELIMINAVIDEOJUEGO", cn);
            return RedirectToAction("ListaVideojuegoPAG");

        }
    }
}