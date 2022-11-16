using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using secretSanta_Login.Permissions;
using secretSanta_Login.Models;
using System.Web.Security;
using System.Net.PeerToPeer;
using System.Web.Razor.Text;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;

namespace secretSanta_Login.Controllers
{

    [ValidateSession]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult ListaDeFamiliares()

        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

     //Las siguientes pruebas son para lograr hacer display del NombreCompleto en /Home/Index.
        //NombreCompleto está en Models/Username.cs
        
        
        //Prueba #4 (La más reciente testeada) con SqlConnection, SqlDataReader y uso de Session para guardar el Valor de NombreCompleto

        /*[AllowAnonymous]
        [HttpPost]
        private void ReadNombreCompleto(Username oUsername)
        {
            SqlConnection conn = new SqlConnection("Data Source=(local);Initial Catalog=DB_ACCESS;Integrated Security=true");
            SqlCommand command = new SqlCommand(
                "SELECT NombreCompleto from USERNAME where NombreCompleto = @NombreCompleto", conn
                );

            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string Name = reader["nombreCompleto"];
                }
            }

            catch (SqlException)
            {
                Session["message"] = "Not found";
            }

            finally
            {

                conn.Close();
            }
        }*/

        //De la tabla Pairings lograr hacer display del usuario y a quién le debe regalar de la columna giftee

        public ActionResult Logout()
        {
            Session["username"] = null;
            return RedirectToAction("Login", "Access");
        }
  
    }
}