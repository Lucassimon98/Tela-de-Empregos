using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelaEmpregos.Models;
using TelaEmpregos.Models.data;

namespace TelaEmpregos.Controllers
{
    [Authorize]
    public class InicioController : Controller
    {
        // GET: Inicio

        private CadastroCurriculumModel cadastroCurriculum;

        private CidadeModel cidadeModel;
        public ActionResult Inicio()
        {
            List<CadastroCurriculum> cadastrosCurriculum = cadastroCurriculum.Pesquisar();

            return View(cadastrosCurriculum);
        }
        public InicioController()
        {
            cadastroCurriculum = new CadastroCurriculumModel();
            cidadeModel = new CidadeModel();
        }

    }
}