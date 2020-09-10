using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TelaEmpregos.Models;
using TelaEmpregos.Models.data;

namespace TelaEmpregos.Controllers
{
    [Authorize]
    public class CurriculumController : Controller
    {

        private CadastroCurriculumModel cadastroCurriculumModel;

        private CidadeModel cidadeModel;
        public CurriculumController()
        {
            cadastroCurriculumModel = new CadastroCurriculumModel();
            cidadeModel = new CidadeModel();
        }

        public ActionResult CurriculumPublic()
        {
            string idParam = Request["id"];

            int id = 0;

            int.TryParse(idParam, out id);

            CadastroCurriculum cadastroCurriculum = cadastroCurriculumModel.ObterId(id);
            Cidade cidade = cidadeModel.Obter(cadastroCurriculum.cidadeid);

            ViewBag.Cidade = cidade;

            return View(cadastroCurriculum);
        }
        public ActionResult Curriculum()
        {
            string idParam = Session["id"].ToString();

            int id = 0;

            int.TryParse(idParam, out id);

            Cidade cidade = null;
            CadastroCurriculum cadastroCurriculum = null;
            cadastroCurriculum = cadastroCurriculumModel.Obter(id);
            if (cadastroCurriculum != null)
            {
                cidade = cidadeModel.Obter(cadastroCurriculum.cidadeid);
            }
            else
            {
                cadastroCurriculum = new CadastroCurriculum();
                cadastroCurriculum.id = 0;
                cadastroCurriculum.nome = "";
                cadastroCurriculum.profissao = "";
                cadastroCurriculum.sobre = "";
                cadastroCurriculum.educacao = "";
                cadastroCurriculum.experiencia = "";
                cadastroCurriculum.habilidade1 = "";
                cadastroCurriculum.nivelConhecimento1 = "";
                cadastroCurriculum.habilidade2 = "";
                cadastroCurriculum.nivelConhecimento2 = "";
                cadastroCurriculum.habilidade3 = "";
                cadastroCurriculum.nivelConhecimento3 = "";
                cadastroCurriculum.habilidade4 = "";
                cadastroCurriculum.nivelConhecimento4 = "";
                cadastroCurriculum.habilidade5 = "";
                cadastroCurriculum.nivelConhecimento5 = "";
                cadastroCurriculum.imagemPerfil = "";
                cadastroCurriculum.facebook = "";
                cadastroCurriculum.linkedIn = "";
                cadastroCurriculum.whatsApp = "";
                cidade = cidadeModel.Obter("CARAZINHO");
                string caminhoArquivo = Server.MapPath("~/files/" + cadastroCurriculum.imagemPerfil);

                if (!System.IO.File.Exists(caminhoArquivo))
                    cadastroCurriculum.imagemPerfil = "";
            }

            List<string> estados = cidadeModel.PesquisarEstados();

            ViewBag.Estados = estados;
            ViewBag.Cidade = cidade;

            return View(cadastroCurriculum);
        }
        public ActionResult ObterCidades()
        {
            ResultAjax result = new ResultAjax();

            try
            {
                string estado = Request["estado"];

                List<Cidade> cidades = cidadeModel.Pesquisar(estado);

                result.Dados = cidades;
                result.Ok = true;
                result.Message = "Cidades recuperadas com sucesso";
            }
            catch (Exception ex)
            {
                result.Ok = false;
                result.Message = ex.Message;
            }

            return new ContentResult()
            {
                Content = new JavaScriptSerializer().Serialize(result),
                ContentEncoding = Encoding.UTF8,
                ContentType = "application/json"
            };
        }



        [HttpPost]
        public ActionResult Edit(FormCollection formCollection)
        {
            ResultAjax result = new ResultAjax();

            try
            {
                string nome = formCollection["txtNomePessoa"];
                string profissao = formCollection["txtAreaAtuacao"];
                string sobre = formCollection["txtSobre"];
                string educacao = formCollection["txtFormacaoAcademica"];
                string experiencia = formCollection["txtExperienciaProfissional"];
                string habilidade1 = formCollection["txtHabilidadeUm"];
                string nivelConhecimento1 = formCollection["slcNivelUm"];
                string habilidade2 = formCollection["txtHabilidadeDois"];
                string nivelConhecimento2 = formCollection["slcNivelDois"];
                string habilidade3 = formCollection["txtHabilidadeTres"];
                string nivelConhecimento3 = formCollection["slcNivelTres"];
                string habilidade4 = formCollection["txtHabilidadeQuatro"];
                string nivelConhecimento4 = formCollection["slcNivelQuatro"];
                string habilidade5 = formCollection["txtHabilidadeCinco"];
                string nivelConhecimento5 = formCollection["slcNivelCinco"];
                string imagemPerfil = formCollection["txtImagemPerfil"];
                string facebook = formCollection["txtFacebook"];
                string linkedIn = formCollection["txtLinkedIn"];
                string whatsApp = formCollection["txtWhatsApp"];
                string cidadeid = formCollection["slcCidade"];
                string idParam = formCollection["txtIdCurriculum"];

                int id = 0;

                int.TryParse(idParam, out id);


                CadastroCurriculum cadastroCurriculum = null;

                if (id > 0)
                    cadastroCurriculum = cadastroCurriculumModel.ObterId(id);

                else
                    cadastroCurriculum = new CadastroCurriculum();

                cadastroCurriculum.nome = nome;
                cadastroCurriculum.profissao = profissao;
                cadastroCurriculum.cidadeid = int.Parse(cidadeid);
                cadastroCurriculum.imagemPerfil = imagemPerfil;
                cadastroCurriculum.sobre = sobre;
                cadastroCurriculum.educacao = educacao;
                cadastroCurriculum.experiencia = experiencia;
                cadastroCurriculum.habilidade1 = habilidade1;
                cadastroCurriculum.nivelConhecimento1 = nivelConhecimento1;
                cadastroCurriculum.habilidade2 = habilidade2;
                cadastroCurriculum.nivelConhecimento2 = nivelConhecimento2;
                cadastroCurriculum.habilidade3 = habilidade3;
                cadastroCurriculum.nivelConhecimento3 = nivelConhecimento3;
                cadastroCurriculum.habilidade4 = habilidade4;
                cadastroCurriculum.nivelConhecimento4 = nivelConhecimento4;
                cadastroCurriculum.habilidade5 = habilidade5;
                cadastroCurriculum.nivelConhecimento5 = nivelConhecimento5;
                cadastroCurriculum.facebook = facebook;
                cadastroCurriculum.whatsApp = whatsApp;
                cadastroCurriculum.linkedIn = linkedIn;
                cadastroCurriculum.cadastroid = int.Parse(Session["id"].ToString());

                if (cadastroCurriculum.id == 0)
                    cadastroCurriculumModel.Incluir(cadastroCurriculum);
                else
                    cadastroCurriculumModel.Alterar(cadastroCurriculum);

                string caminhoArquivoTemp = Server.MapPath("~/files/tmp/" + imagemPerfil);
                string caminhoArquivo = Server.MapPath("~/files/" + imagemPerfil);

                if (System.IO.File.Exists(caminhoArquivoTemp))
                    System.IO.File.Move(caminhoArquivoTemp, caminhoArquivo);

                ClearDirectoryTemp();

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

        private void ClearDirectoryTemp()
        {
            string diretorioArquivoTemp = Server.MapPath("~/files/tmp/");

            string[] arquivos = Directory.GetFiles(diretorioArquivoTemp);

            foreach (string arquivo in arquivos)
                try
                {
                    System.IO.File.Delete(arquivo);
                }
                catch (Exception)
                {
                }
        }

        [HttpPost]
        public ActionResult Upload()
        {
            ResultAjax result = new ResultAjax();

            try
            {
                string caminhoPasta = Server.MapPath("~/files/tmp");

                if (!Directory.Exists(caminhoPasta))
                    Directory.CreateDirectory(caminhoPasta);

                HttpPostedFileBase arquivoRecebido = Request.Files[0];

                string nomeArquivo = Guid.NewGuid().ToString() + ".png";

                string fullName = Path.Combine(caminhoPasta, nomeArquivo);

                Image imgOriginal = Image.FromStream(arquivoRecebido.InputStream);

                int largura = 0;
                int altura = 0;

                if (imgOriginal.Width >= imgOriginal.Height)
                {
                    largura = 300;
                    altura = imgOriginal.Height * largura / imgOriginal.Width;
                }
                else
                {
                    altura = 300;
                    largura = imgOriginal.Width * altura / imgOriginal.Height;
                }


                Image imageThumb = imgOriginal.GetThumbnailImage(largura, altura, () => false, IntPtr.Zero);

                imageThumb.Save(fullName);

                result.Ok = true;
                result.Message = "Arquivo salvo com sucesso";
                result.Dados = nomeArquivo;
            }
            catch (Exception ex)
            {
                result.Ok = false;
                result.Message = ex.Message;
            }

            return new ContentResult()
            {
                Content = new JavaScriptSerializer().Serialize(result),
                ContentEncoding = Encoding.UTF8,
                ContentType = "application/json"
            };
        }
    }
}
