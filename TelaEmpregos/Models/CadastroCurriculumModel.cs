using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TelaEmpregos.Models.data;

namespace TelaEmpregos.Models
{
    public class CadastroCurriculumModel
    {
        public void Incluir(CadastroCurriculum cadastroCurriculum)
        {
            using (MySqlConnection connection = new MySqlConnection(TelaEmpregos.Properties.Settings.Default.StringConexao))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                                insert into cadastrocurriculum 
                                    (nome, profissao, sobre, educacao, experiencia,
                                     habilidade1, nivelConhecimento1, habilidade2, nivelConhecimento2, habilidade3, nivelConhecimento3,
                                     habilidade4, nivelConhecimento4, habilidade5, nivelConhecimento5, imagemPerfil, 
                                     facebook, linkedIn, whatsApp, cidadeid, cadastroid) 
                                values 
                                    (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?);

                                select LAST_INSERT_ID();";


                    command.Parameters.Add("@nome", MySqlDbType.VarChar).Value = cadastroCurriculum.nome;
                    command.Parameters.Add("@profissao", MySqlDbType.VarChar).Value = cadastroCurriculum.profissao;
                    command.Parameters.Add("@sobre", MySqlDbType.VarChar).Value = cadastroCurriculum.sobre;
                    command.Parameters.Add("@educacao", MySqlDbType.VarChar).Value = cadastroCurriculum.educacao;
                    command.Parameters.Add("@experiencia", MySqlDbType.VarChar).Value = cadastroCurriculum.experiencia;
                    command.Parameters.Add("@habilidadae1", MySqlDbType.VarChar).Value = cadastroCurriculum.habilidade1;
                    command.Parameters.Add("@nivelConhecimento1", MySqlDbType.VarChar).Value = cadastroCurriculum.nivelConhecimento1;
                    command.Parameters.Add("@habilidade2", MySqlDbType.VarChar).Value = cadastroCurriculum.habilidade2;
                    command.Parameters.Add("@nivelConhecimento2", MySqlDbType.VarChar).Value = cadastroCurriculum.nivelConhecimento2;
                    command.Parameters.Add("@habilidade3", MySqlDbType.VarChar).Value = cadastroCurriculum.habilidade3;
                    command.Parameters.Add("@nivelConhecimento3", MySqlDbType.VarChar).Value = cadastroCurriculum.nivelConhecimento3;
                    command.Parameters.Add("@habilidade4", MySqlDbType.VarChar).Value = cadastroCurriculum.habilidade4;
                    command.Parameters.Add("@nivelConhecimento4", MySqlDbType.VarChar).Value = cadastroCurriculum.nivelConhecimento4;
                    command.Parameters.Add("@habilidade5", MySqlDbType.VarChar).Value = cadastroCurriculum.habilidade5;
                    command.Parameters.Add("@nivelConhecimento5", MySqlDbType.VarChar).Value = cadastroCurriculum.nivelConhecimento5;
                    command.Parameters.Add("@imagemPerfil", MySqlDbType.VarChar).Value = cadastroCurriculum.imagemPerfil;
                    command.Parameters.Add("@facebook", MySqlDbType.VarChar).Value = cadastroCurriculum.facebook;
                    command.Parameters.Add("@linkedIn", MySqlDbType.VarChar).Value = cadastroCurriculum.linkedIn;
                    command.Parameters.Add("@whatsApp", MySqlDbType.VarChar).Value = cadastroCurriculum.whatsApp;
                    command.Parameters.Add("@cidadeid", MySqlDbType.Int32).Value = cadastroCurriculum.cidadeid;
                    command.Parameters.Add("@cadastroid", MySqlDbType.Int32).Value = cadastroCurriculum.cadastroid;

                    connection.Open();

                    object idGerado = command.ExecuteScalar();

                    cadastroCurriculum.id = Convert.ToInt32(idGerado);
                }
            }
        }

        public void Alterar(Cadastro cadastro)
        {
            using (MySqlConnection connection = new MySqlConnection(TelaEmpregos.Properties.Settings.Default.StringConexao))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                                update cadastro
                                set
                                    email = ?
                                    ,senha = ?
                                where
                                    id = ?";


                    command.Parameters.Add("@email", MySqlDbType.VarChar).Value = cadastro.email;
                    command.Parameters.Add("@senha", MySqlDbType.VarChar).Value = cadastro.senha;


                    command.Parameters.Add("@id", MySqlDbType.Int32).Value = cadastro.id;

                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
        }


        public void Excluir(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(TelaEmpregos.Properties.Settings.Default.StringConexao))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                                delete from cadastro
                                where
                                    id = ?";

                    command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<CadastroCurriculum> Pesquisar()
        {
            var lista = new List<CadastroCurriculum>();

            using (MySqlConnection connection = new MySqlConnection(TelaEmpregos.Properties.Settings.Default.StringConexao))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    string query = @"
                                    select
                                        *
                                    from
                                        cadastrocurriculum
                                    limit 15
                                    ";

                    command.CommandText = query;

                    using (DataTable table = new DataTable())
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(table);
                        }

                        foreach (DataRow row in table.Rows)
                        {
                            CadastroCurriculum cadastro = new CadastroCurriculum();

                            cadastro.cadastroid = Convert.ToInt32(row["cadastroid"]);
                            cadastro.cidadeid = Convert.ToInt32(row["cidadeid"]);
                            cadastro.nome = Convert.ToString(row["nome"]);
                            cadastro.profissao = Convert.ToString(row["profissao"]);
                            cadastro.sobre = Convert.ToString(row["sobre"]);
                            cadastro.educacao = Convert.ToString(row["educacao"]);
                            cadastro.experiencia = Convert.ToString(row["experiencia"]);
                            cadastro.habilidade1 = Convert.ToString(row["habilidade1"]);
                            cadastro.nivelConhecimento1 = Convert.ToString(row["nivelConhecimento1"]);
                            cadastro.habilidade2 = Convert.ToString(row["habilidade2"]);
                            cadastro.nivelConhecimento2 = Convert.ToString(row["nivelConhecimento2"]);
                            cadastro.habilidade3 = Convert.ToString(row["habilidade3"]);
                            cadastro.nivelConhecimento3 = Convert.ToString(row["nivelConhecimento3"]);
                            cadastro.habilidade4 = Convert.ToString(row["habilidade4"]);
                            cadastro.nivelConhecimento4 = Convert.ToString(row["nivelConhecimento4"]);
                            cadastro.habilidade5 = Convert.ToString(row["habilidade5"]);
                            cadastro.nivelConhecimento5 = Convert.ToString(row["nivelConhecimento5"]);
                            cadastro.imagemPerfil = Convert.ToString(row["imagemPerfil"]);
                            cadastro.facebook = Convert.ToString(row["facebook"]);
                            cadastro.linkedIn = Convert.ToString(row["linkedIn"]);
                            cadastro.whatsApp = Convert.ToString(row["whatsApp"]);
                            cadastro.id = Convert.ToInt32(row["id"]);

                            lista.Add(cadastro);
                        }

                    }
                }
            }

            return lista;
        }



        public CadastroCurriculum Obter(int cadastroid)
        {
            using (MySqlConnection connection = new MySqlConnection(TelaEmpregos.Properties.Settings.Default.StringConexao))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    string query = @"
                                    select
                                        *
                                    from
                                        cadastrocurriculum
                                    where
                                        cadastroid = ?
                                    ";

                    command.Parameters.Add("@cadastroid", MySqlDbType.Int32).Value = cadastroid;

                    command.CommandText = query;

                    using (DataTable table = new DataTable())
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(table);
                        }

                        foreach (DataRow row in table.Rows)
                        {
                            CadastroCurriculum cadastro = new CadastroCurriculum();

                            cadastro.cadastroid = Convert.ToInt32(row["cadastroid"]);
                            cadastro.cidadeid = Convert.ToInt32(row["cidadeid"]);
                            cadastro.nome = Convert.ToString(row["nome"]);
                            cadastro.profissao = Convert.ToString(row["profissao"]);
                            cadastro.sobre = Convert.ToString(row["sobre"]);
                            cadastro.educacao = Convert.ToString(row["educacao"]);
                            cadastro.experiencia = Convert.ToString(row["experiencia"]);
                            cadastro.habilidade1 = Convert.ToString(row["habilidade1"]);
                            cadastro.nivelConhecimento1 = Convert.ToString(row["nivelConhecimento1"]);
                            cadastro.habilidade2 = Convert.ToString(row["habilidade2"]);
                            cadastro.nivelConhecimento2 = Convert.ToString(row["nivelConhecimento2"]);
                            cadastro.habilidade3 = Convert.ToString(row["habilidade3"]);
                            cadastro.nivelConhecimento3 = Convert.ToString(row["nivelConhecimento3"]);
                            cadastro.habilidade4 = Convert.ToString(row["habilidade4"]);
                            cadastro.nivelConhecimento4 = Convert.ToString(row["nivelConhecimento4"]);
                            cadastro.habilidade5 = Convert.ToString(row["habilidade5"]);
                            cadastro.nivelConhecimento5 = Convert.ToString(row["nivelConhecimento5"]);
                            cadastro.imagemPerfil = Convert.ToString(row["imagemPerfil"]);
                            cadastro.facebook = Convert.ToString(row["facebook"]);
                            cadastro.linkedIn = Convert.ToString(row["linkedIn"]);
                            cadastro.whatsApp = Convert.ToString(row["whatsApp"]);
                            cadastro.id = Convert.ToInt32(row["id"]);

                            return cadastro;
                        }

                    }
                }
            }

            return null;
        }

        public CadastroCurriculum ObterId(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(TelaEmpregos.Properties.Settings.Default.StringConexao))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    string query = @"
                                    select
                                        *
                                    from
                                        cadastrocurriculum
                                    where
                                        id = ?
                                    ";

                    command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

                    command.CommandText = query;

                    using (DataTable table = new DataTable())
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(table);
                        }

                        foreach (DataRow row in table.Rows)
                        {
                            CadastroCurriculum cadastro = new CadastroCurriculum();

                            cadastro.cadastroid = Convert.ToInt32(row["cadastroid"]);
                            cadastro.cidadeid = Convert.ToInt32(row["cidadeid"]);
                            cadastro.nome = Convert.ToString(row["nome"]);
                            cadastro.profissao = Convert.ToString(row["profissao"]);
                            cadastro.sobre = Convert.ToString(row["sobre"]);
                            cadastro.educacao = Convert.ToString(row["educacao"]);
                            cadastro.experiencia = Convert.ToString(row["experiencia"]);
                            cadastro.habilidade1 = Convert.ToString(row["habilidade1"]);
                            cadastro.nivelConhecimento1 = Convert.ToString(row["nivelConhecimento1"]);
                            cadastro.habilidade2 = Convert.ToString(row["habilidade2"]);
                            cadastro.nivelConhecimento2 = Convert.ToString(row["nivelConhecimento2"]);
                            cadastro.habilidade3 = Convert.ToString(row["habilidade3"]);
                            cadastro.nivelConhecimento3 = Convert.ToString(row["nivelConhecimento3"]);
                            cadastro.habilidade4 = Convert.ToString(row["habilidade4"]);
                            cadastro.nivelConhecimento4 = Convert.ToString(row["nivelConhecimento4"]);
                            cadastro.habilidade5 = Convert.ToString(row["habilidade5"]);
                            cadastro.nivelConhecimento5 = Convert.ToString(row["nivelConhecimento5"]);
                            cadastro.imagemPerfil = Convert.ToString(row["imagemPerfil"]);
                            cadastro.facebook = Convert.ToString(row["facebook"]);
                            cadastro.linkedIn = Convert.ToString(row["linkedIn"]);
                            cadastro.whatsApp = Convert.ToString(row["whatsApp"]);
                            cadastro.id = Convert.ToInt32(row["id"]);

                            return cadastro;
                        }

                    }
                }
            }

            return null;
        }

        public void Alterar(CadastroCurriculum cadastroCurriculum)
        {
            using (MySqlConnection connection = new MySqlConnection(Properties.Settings.Default.StringConexao))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                                update cadastrocurriculum
                                set
                                    nome = ?, profissao = ?, sobre = ?, educacao = ?, experiencia = ?,
                                    habilidade1 = ?, nivelConhecimento1 = ?, habilidade2 = ?, nivelConhecimento2 = ?, 
                                    habilidade3 = ?, nivelConhecimento3 = ?,
                                    habilidade4 = ?, nivelConhecimento4 = ?, habilidade5 = ?, nivelConhecimento5 = ?, 
                                    imagemPerfil = ?, 
                                    facebook = ?, linkedIn = ?, whatsApp = ?, cidadeid = ?, cadastroid = ?
                                where
                                    id = ?";

                    command.Parameters.Add("@nome", MySqlDbType.VarChar).Value = cadastroCurriculum.nome;
                    command.Parameters.Add("@profissao", MySqlDbType.VarChar).Value = cadastroCurriculum.profissao;
                    command.Parameters.Add("@sobre", MySqlDbType.VarChar).Value = cadastroCurriculum.sobre;
                    command.Parameters.Add("@educacao", MySqlDbType.VarChar).Value = cadastroCurriculum.educacao;
                    command.Parameters.Add("@experiencia", MySqlDbType.VarChar).Value = cadastroCurriculum.experiencia;
                    command.Parameters.Add("@habilidadae1", MySqlDbType.VarChar).Value = cadastroCurriculum.habilidade1;
                    command.Parameters.Add("@nivelConhecimento1", MySqlDbType.VarChar).Value = cadastroCurriculum.nivelConhecimento1;
                    command.Parameters.Add("@habilidade2", MySqlDbType.VarChar).Value = cadastroCurriculum.habilidade2;
                    command.Parameters.Add("@nivelConhecimento2", MySqlDbType.VarChar).Value = cadastroCurriculum.nivelConhecimento2;
                    command.Parameters.Add("@habilidade3", MySqlDbType.VarChar).Value = cadastroCurriculum.habilidade3;
                    command.Parameters.Add("@nivelConhecimento3", MySqlDbType.VarChar).Value = cadastroCurriculum.nivelConhecimento3;
                    command.Parameters.Add("@habilidade4", MySqlDbType.VarChar).Value = cadastroCurriculum.habilidade4;
                    command.Parameters.Add("@nivelConhecimento4", MySqlDbType.VarChar).Value = cadastroCurriculum.nivelConhecimento4;
                    command.Parameters.Add("@habilidade5", MySqlDbType.VarChar).Value = cadastroCurriculum.habilidade5;
                    command.Parameters.Add("@nivelConhecimento5", MySqlDbType.VarChar).Value = cadastroCurriculum.nivelConhecimento5;
                    command.Parameters.Add("@imagemPerfil", MySqlDbType.VarChar).Value = cadastroCurriculum.imagemPerfil;
                    command.Parameters.Add("@facebook", MySqlDbType.VarChar).Value = cadastroCurriculum.facebook;
                    command.Parameters.Add("@linkedIn", MySqlDbType.VarChar).Value = cadastroCurriculum.linkedIn;
                    command.Parameters.Add("@whatsApp", MySqlDbType.VarChar).Value = cadastroCurriculum.whatsApp;
                    command.Parameters.Add("@cidadeid", MySqlDbType.Int32).Value = cadastroCurriculum.cidadeid;
                    command.Parameters.Add("@cadastroid", MySqlDbType.Int32).Value = cadastroCurriculum.cadastroid;

                    command.Parameters.Add("@id", MySqlDbType.Int32).Value = cadastroCurriculum.id;

                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}