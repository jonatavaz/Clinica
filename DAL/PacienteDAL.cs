using Microsoft.Data.SqlClient;
using POJO;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class PacienteDAL
    {
        public List<Paciente> GetAllPacientes()
        {
            SqlConnection conexao = new SqlConnection(_Conexao.StringDeConexao);
            string sql = @"SELECT * FROM Paciente PC 
                           INNER JOIN Pessoa P ON PC.PessoaId = P.PessoaId;";

            try
            {
                var result = conexao.Query<Paciente>(sql).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conexao.Close();
            }

        }
        public List<Paciente> GetPacientesPorNome(string nome)
        {
            using (var conexao = new SqlConnection(_Conexao.StringDeConexao))
            {
                string sql = @"SELECT PC.* 
                       FROM Paciente PC 
                       INNER JOIN Pessoa P ON PC.PessoaId = P.PessoaId 
                       WHERE P.Nome = @Nome";

                var parameters = new DynamicParameters();
                parameters.Add("@Nome", nome);

                try
                {
                    var result = conexao.Query<Paciente>(sql, parameters).ToList(); // Retorna uma lista
                    return result;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }



        public Paciente GetPacienteById(int PacienteId)
        {
            SqlConnection conexao = new SqlConnection(_Conexao.StringDeConexao);

            string sql = @"SELECT * FROM Paciente WHERE PacienteId = @PacienteId";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PacienteId", PacienteId);
            try
            {
                var result = conexao.QueryFirstOrDefault<Paciente>(sql, parameters);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conexao.Close();
            }

        }

        public int AddPaciente(Paciente paciente)
        {
            SqlConnection conexao = new SqlConnection(_Conexao.StringDeConexao);

            // Primeiro, insira na tabela Pessoa
            string sqlPessoa = @"INSERT INTO Pessoa (Nome, Email, Senha, DataNascimento)
                         OUTPUT INSERTED.PessoaId
                         VALUES (@Nome, @Email, @Senha, @DataNascimento);";

            DynamicParameters parametersPessoa = new DynamicParameters();
            parametersPessoa.Add("@Nome", paciente.Nome);
            parametersPessoa.Add("@Email", paciente.Email);
            parametersPessoa.Add("@Senha", paciente.Senha); 
            parametersPessoa.Add("@DataNascimento", paciente.DataNascimento);

            try
            {
                var pessoaId = conexao.QuerySingle<int>(sqlPessoa, parametersPessoa);

                // Depois, insira na tabela Paciente
                string sqlPaciente = @"INSERT INTO Paciente (PessoaId)
                               OUTPUT INSERTED.PacienteId
                               VALUES (@PessoaId);";

                var parametersPaciente = new DynamicParameters();
                parametersPaciente.Add("@PessoaId", pessoaId);

                var pacienteId = conexao.QuerySingle<int>(sqlPaciente, parametersPaciente);
                return pacienteId; // Retorna o ID do paciente
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conexao.Close();
            }
        }


        public bool UpdatePaciente(Paciente paciente)
        {
            SqlConnection conexao = new SqlConnection(_Conexao.StringDeConexao);

            string sql = "UPDATE Paciente SET Nome = @Nome, Email = @Email, Senha = @Senha, DataNascimento = @DataNascimento WHERE PacienteId = @PacienteId";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Nome", paciente.Nome);
            parameters.Add("@Email", paciente.Email);
            parameters.Add("@Senha", paciente.Senha);
            parameters.Add("@DataNascimento", paciente.DataNascimento);

            try
            {
                var result = conexao.Query(sql, parameters).ToList();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conexao.Close();
            }

        }

        public bool DeletePaciente(int PacienteId)
        {
            SqlConnection conexao = new SqlConnection(_Conexao.StringDeConexao);
            string sql = "DELETE FROM Paciente WHERE PacienteId = @PacienteId";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PacienteId", PacienteId);

            try
            {

                var result = conexao.Execute(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conexao.Close();
            }

        }
    }
}
