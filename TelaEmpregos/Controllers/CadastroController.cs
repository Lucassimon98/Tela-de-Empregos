using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TelaEmpregos.Models;
using TelaEmpregos.Models.data;

namespace TelaEmpregos.Controllers
{
    public class CadastroController : Controller
    {
        private UsuarioModel usuarioModel;

        public CadastroController()
        {
            usuarioModel = new UsuarioModel();
        }

        // GET: Cadastro
        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IncluirUser(FormCollection formCollection)
        {
            ResultAjax result = new ResultAjax();

            try
            {
                string email = formCollection["txtEmailCadastro"];
                string senha = formCollection["txtSenhaCadastro"];
                string senha2 = formCollection["txtSenhaCadastro2"];

                if (senha != senha2)
                {
                    throw new Exception("Senha não conferem");
                }

                Cadastro cadastro = null;

                cadastro = new Cadastro() { senha = "" };

                cadastro.senha = MD5Hash(senha);

                cadastro.email = email;

                if (cadastro.id == 0)
                    usuarioModel.Incluir(cadastro);

                result.Ok = true;
                result.Message = "Usuário salvo com sucesso!";
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


        [HttpPost]


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