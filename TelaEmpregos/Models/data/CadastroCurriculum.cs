using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelaEmpregos.Models.data
{
    public class CadastroCurriculum
    {
        public int id { get; set; }

        public string nome { get; set; }

        public string profissao { get; set; }

        public string habilidade1 { get; set; }

        public string nivelConhecimento1 { get; set; }

        public string habilidade2 { get; set; }

        public string nivelConhecimento2 { get; set; }

        public string habilidade3 { get; set; }

        public string nivelConhecimento3 { get; set; }

        public string habilidade4 { get; set; }

        public string nivelConhecimento4 { get; set; }

        public string habilidade5 { get; set; }

        public string nivelConhecimento5 { get; set; }

        public string sobre { get; set; }

        public string experiencia { get; set; }

        public string educacao { get; set; }

        public string imagemPerfil { get; set; }

        public string facebook { get; set; }

        public string linkedIn { get; set; }

        public string whatsApp { get; set; }

        public int cidadeid { get; set; }

        public int cadastroid { get; set; }
    }
}