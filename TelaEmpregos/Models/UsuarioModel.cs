using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using TelaEmpregos.Models.data;

namespace TelaEmpregos.Models
{
    public class UsuarioModel
    {
        public void Incluir(Cadastro cadastro)
        {
            using (MySqlConnection connection = new MySqlConnection(TelaEmpregos.Properties.Settings.Default.StringConexao))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                                insert into cadastro 
                                    (email, senha) 
                                values 
                                    (?, ?);

                                select LAST_INSERT_ID();";


                    command.Parameters.Add("@email", MySqlDbType.VarChar).Value = cadastro.email;
                    command.Parameters.Add("@senha", MySqlDbType.VarChar).Value = cadastro.senha;


                    connection.Open();

                    object idGerado = command.ExecuteScalar();

                    cadastro.id = Convert.ToInt32(idGerado);
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

        public List<Cadastro> Pesquisar(string nome, string email)
        {
            var lista = new List<Cadastro>();

            using (MySqlConnection connection = new MySqlConnection(TelaEmpregos.Properties.Settings.Default.StringConexao))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    string query = @"
                                    select
                                        *
                                    from
                                        cadastro
                                    where
                                        1=1
                                    ";

                    string where = "";


                    if (nome != "")
                    {
                        where += " and email like ? ";
                        command.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;
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
                            Cadastro cadastro = new Cadastro();

                            cadastro.id = Convert.ToInt32(row["id"]);
                            cadastro.email = Convert.ToString(row["email"]);
                            cadastro.email = Convert.ToString(row["senha"]);

                            lista.Add(cadastro);
                        }

                    }
                }
            }

            return lista;
        }



        public Cadastro Obter(string txtEmailEntrar)
        {
            using (MySqlConnection connection = new MySqlConnection(TelaEmpregos.Properties.Settings.Default.StringConexao))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    string query = @"
                                    select
                                        *
                                    from
                                        cadastro
                                    where
                                        email = ?
                                    ";


                    command.Parameters.Add("@email", MySqlDbType.VarChar).Value = txtEmailEntrar;

                    command.CommandText = query;

                    using (DataTable table = new DataTable())
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(table);
                        }

                        foreach (DataRow row in table.Rows)
                        {
                            Cadastro cadastro = new Cadastro();

                            cadastro.id = Convert.ToInt32(row["id"]);
                            cadastro.email = Convert.ToString(row["email"]);
                            cadastro.senha = Convert.ToString(row["senha"]);

                            return cadastro;
                        }

                    }
                }
            }

            return null;
        }




        public Cadastro Obter(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(TelaEmpregos.Properties.Settings.Default.StringConexao))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    string query = @"
                                    select
                                        *
                                    from
                                        cadastro
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

                            Cadastro cadastro = new Cadastro();

                            cadastro.id = Convert.ToInt32(row["id"]);
                            cadastro.email = Convert.ToString(row["email"]);
                            cadastro.senha = Convert.ToString(row["senha"]);

                            return cadastro;
                        }

                    }
                }
            }

            return null;
        }



    }
}