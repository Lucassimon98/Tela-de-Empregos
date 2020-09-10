using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using TelaEmpregos.Models;
using TelaEmpregos.Models.data;

namespace TelaEmpregos.Controllers
{
    public class IndexController : Controller
    {
        private UsuarioModel usuarioModel;

        public IndexController()
        {
            usuarioModel = new UsuarioModel();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(FormCollection formCollection)
        {
            ResultAjax result = new ResultAjax();

            try
            {

                string email = formCollection["txtEmailEntrar"];
                string senha = formCollection["txtSenhaEntrar"];

                email = email.Trim();
                senha = senha.Trim();

                string senhaCriptografada = MD5Hash(senha);

                Cadastro usuario = usuarioModel.Obter(email);

                if (usuario == null)
                    throw new Exception("Usuário não encontrado");

                if (email.Equals(usuario.email, StringComparison.OrdinalIgnoreCase)
                        && senhaCriptografada == usuario.senha)
                {
                    result.Ok = true;
                    result.Message = "Login realizado";

                    FormsAuthentication.SetAuthCookie(email, false);

                    Session["USUARIO"] = email;
                    Session["id"] = usuario.id;
                }
                else
                    throw new Exception("Usuário e Senha não são válidos");

            }
            catch (Exception e)
            {
                result.Ok = false;
                result.Message = e.Message;
            }

            return new ContentResult()
            {
                Content = new JavaScriptSerializer().Serialize(result),
                ContentEncoding = Encoding.UTF8,
                ContentType = "application/json"
            };
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index");
        }

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(Encoding.ASCII.GetBytes(text));

            byte[] result = md5.Hash;

            var strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
                strBuilder.Append(result[i].ToString("x2"));

            return strBuilder.ToString();
        }

    }
}