using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using secretSanta_Login.Models;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services.Description;
using System.Web.Security;
using System.Configuration;
using System.Drawing;

namespace secretSanta_Login.Controllers
{
    public class AccessController : Controller
    {
        static string chain = "Data Source=(local);Initial Catalog=DB_ACCESS;Integrated Security=true";

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Username oUsername)
        {
            bool registered;
            string message;
            if (oUsername.Password == oUsername.ConfirmPassword) {
                oUsername.Password = ConvertSha256(oUsername.Password);
            }
            else
            {
                ViewData["Message"] = "Passwords don't match";
                return View();
            }
            using (SqlConnection cn = new SqlConnection(chain))
            { 
                SqlCommand cmd = new SqlCommand("sp_RegisterUser", cn);
                cmd.Parameters.AddWithValue("NombreCompleto", oUsername.NombreCompleto);
                cmd.Parameters.AddWithValue("Gift", oUsername.Gift);
                cmd.Parameters.AddWithValue("Email", oUsername.Email);
                cmd.Parameters.AddWithValue("Password", oUsername.Password);
                cmd.Parameters.Add("Registered", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Message", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                cmd.ExecuteNonQuery();

                registered = Convert.ToBoolean(cmd.Parameters["Registered"].Value);
                message = cmd.Parameters["Message"].Value.ToString();

            }

            ViewData["Message"] = message;

            if (registered) {
                return RedirectToAction("Login", "Access");
                }
            else
            {
                return View();
            }
            
        }
        [HttpPost]
        public ActionResult Login(Username oUsername)
        {
            oUsername.Password = ConvertSha256(oUsername.Password);
            using (SqlConnection cn = new SqlConnection(chain))
            {
                SqlCommand cmd = new SqlCommand("sp_ValidateUser", cn);
                cmd.Parameters.AddWithValue("Email", oUsername.Email);
                cmd.Parameters.AddWithValue("Password", oUsername.Password);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                oUsername.IdUsername = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }
            if (oUsername.IdUsername != 0)
            {
                Session["username"] = oUsername;
                return RedirectToAction("Index", "Home");

            }
            else {
                ViewData["Message"] = "Username not found";
                return View();
            }

        }

  //Las 3 siguientes pruebas han sido testeadas también en HomeController.cs (Que es donde debería ser el request para que llegue al View /Home/Index)

        //Prueba #1 con Session para display de NombreCompleto en /Home/Index

        /*[HttpPost]
        public ActionResult LoadName(Username oUsername)
        {
            if (oUsername.NombreCompleto != null)
            {
                Session["nombreCompleto"] = oUsername.NombreCompleto;
                return RedirectToAction("Index", "Home");
            }
        }*/


        //Prueba #2 con SqlCommand, SqlDataReader y Session para display de NombreCompleto en /Home/Index

        /*[AllowAnonymous]
        [HttpPost]
        public ActionResult Index(Username oUsername)
        {
            string Myconnection = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(Myconnection);
            string sqlquery = "select NombreCompleto from [dbo].[USERNAME] where NombreCompleto = @NombreCompleto";
            sqlconn.Open();
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlcomm.Parameters.AddWithValue("@NombreCompleto", oUsername.NombreCompleto);
            SqlDataReader sdr = sqlcomm.ExecuteReader();
            if (sdr.Read())
            {
                FormsAuthentication.SetAuthCookie(oUsername.NombreCompleto, true);
                Session["nombreCompleto"] = oUsername.NombreCompleto.ToString();
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewData["message"] = "Nombre inválido";
            }
            sqlconn.Close();
            return View();

        }*/


        //prueba #3 con SqlConnection, SqlDataReader y Session para display de NombreCompleto en /Home/Index

        /*protected void OnLogin(object sender, EventArgs e)
        {
            string Myconnection = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(Myconnection))
            {
                string str = "SELECT COUNT(*) FROM USERNAME WHERE NombreCompleto = @NombreCompleto";
                SqlCommand cmd = new SqlCommand(str, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@NombreCompleto", txtNombreCompleto.Text);
                object obj;
                con.Open();
                obj = cmd.ExecuteScalar();
                con.Close();
                if (Convert.ToInt32(obj) > 0)
                {
                    Session["nombreCompleto"] = txtnombreCompleto.Text;
                    Response.Redirect("Index","Home");
                }
            }
        }*/


        public static string ConvertSha256(string text)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(text));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }

    }
}