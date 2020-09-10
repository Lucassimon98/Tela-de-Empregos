using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TelaEmpregos.Models.data;

namespace TelaEmpregos.Models
{
    public class CidadeModel
    {
        public List<Cidade> Pesquisar(string estado)
        {
            var lista = new List<Cidade>();

            using (MySqlConnection connection = new MySqlConnection(TelaEmpregos.Properties.Settings.Default.StringConexao))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    string query = @"
                                    select
                                        *
                                    from
                                        cidade
                                    where
                                        1=1
                                    ";

                    string where = "";


                    if (estado != "")
                    {
                        where += " and estado = ? ";
                        command.Parameters.Add("@estado", MySqlDbType.VarChar).Value = estado;
                    }


                    query += where;

                    command.CommandText = query;

                    using (DataTable table = new DataTable())
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(table);
                        }

                        foreach (DataRow row in table.Rows)
                        {
                            Cidade cidade = new Cidade();

                            cidade.id = Convert.ToInt32(row["id"]);
                            cidade.nome = Convert.ToString(row["nome"]);
                            cidade.estado = Convert.ToString(row["estado"]);

                            lista.Add(cidade);
                        }

                    }
                }
            }

            return lista;
        }
        public List<string> PesquisarEstados()
        {
            var lista = new List<string>();

            using (MySqlConnection connection = new MySqlConnection(Properties.Settings.Default.StringConexao))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    string query = @"
                                    select
                                        distinct estado
                                    from
                                        cidade
                                    order by estado";

                    command.CommandText = query;

                    using (DataTable table = new DataTable())
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(table);
                        }

                        foreach (DataRow row in table.Rows)
                        {
                            string estado = Convert.ToString(row["estado"]);

                            lista.Add(estado);
                        }

                    }
                }
            }

            return lista;
        }
        public Cidade Obter(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Properties.Settings.Default.StringConexao))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    string query = @"
                                    select
                                        *
                                    from
                                        cidade
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

                        if (table.Rows.Count > 0)
                        {
                            DataRow row = table.Rows[0];

                            Cidade cidade = new Cidade();

                            cidade.id = Convert.ToInt32(row["id"]);
                            cidade.nome = Convert.ToString(row["nome"]);
                            cidade.estado = Convert.ToString(row["estado"]);

                            return cidade;

                        }

                    }
                }
            }

            return null;
        }


        public Cidade Obter(string nome)
        {
            using (MySqlConnection connection = new MySqlConnection(Properties.Settings.Default.StringConexao))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    string query = @"
                                    select
                                        *
                                    from
                                        cidade
                                    where
                                        nome = ?
                                    ";

                    command.Parameters.Add("@nome", MySqlDbType.VarChar).Value = nome;

                    command.CommandText = query;

                    using (DataTable table = new DataTable())
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(table);
                        }

                        if (table.Rows.Count > 0)
                        {
                            DataRow row = table.Rows[0];

                            Cidade cidade = new Cidade();

                            cidade.id = Convert.ToInt32(row["id"]);
                            cidade.nome = Convert.ToString(row["nome"]);
                            cidade.estado = Convert.ToString(row["estado"]);

                            return cidade;

                        }

                    }
                }
            }

            return null;
        }
    }
}