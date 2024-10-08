using Microsoft.Data.SqlClient;
using POJO;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DAL
{
    public class LoginDAL
    {
        public Login GetLogin(string email, string senha)
        {
            var conexao = new SqlConnection(_Conexao.StringDeConexao);
            string sql = @"
                SELECT L.LoginId, L.DataHora, L.PacienteId, L.MedicoId,
                ISNULL(P1.Nome, P2.Nome) AS Nome,
                ISNULL(P1.Email, P2.Email) AS Email,
                ISNULL(P1.DataNascimento, P2.DataNascimento) AS DataNascimento,
                ISNULL(P1.Senha, P2.Senha) AS SenhaHash
                FROM Login L
                LEFT JOIN Paciente P ON L.PacienteId = P.PacienteId
                LEFT JOIN Pessoa P1 ON P.PessoaId = P1.PessoaId
                LEFT JOIN Medico M ON L.MedicoId = M.MedicoId
                LEFT JOIN Pessoa P2 ON M.PessoaId = P2.PessoaId
                WHERE (P1.Email = @Email OR P2.Email = @Email);";

            var parameters = new DynamicParameters();
            parameters.Add("@Email", email);

            try
            {
                var result = conexao.QueryFirstOrDefault<Login>(sql, parameters);
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
        public List<Login> GetAllLogins()
        {
            var conexao = new SqlConnection(_Conexao.StringDeConexao);
            string sql = "SELECT * FROM Login";

            try
            {
                var result = conexao.Query<Login>(sql).ToList();
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
        public Login GetLoginById(int loginId)
        {
            var conexao = new SqlConnection(_Conexao.StringDeConexao);
            string sql = "SELECT * FROM Login WHERE LoginId = @LoginId";

            var parameters = new DynamicParameters();
            parameters.Add("@LoginId", loginId);

            try
            {
                var result = conexao.QueryFirstOrDefault<Login>(sql, parameters);
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
        public bool AddLogin(Login login, bool isPaciente)
        {
            int pessoaId = 0, medicoId = 0, pacienteId = 0;

            var conexao = new SqlConnection(_Conexao.StringDeConexao);
            string sql = @"INSERT INTO Pessoa (Nome, Email, Senha, DataNascimento)
                   OUTPUT INSERTED.PessoaId
                   VALUES (@Nome, @Email, @Senha, @DataNascimento);";

            var parametersPessoa = new DynamicParameters();
            parametersPessoa.Add("@Nome", login.Nome);
            parametersPessoa.Add("@Email", login.Email);
            parametersPessoa.Add("@Senha", HashPassword(login.Senha));
            parametersPessoa.Add("@DataNascimento", login.DataNascimento);

            try
            {
                pessoaId = conexao.QuerySingle<int>(sql, parametersPessoa);

                if (isPaciente)
                {
                    string sqlPaciente = @"INSERT INTO Paciente (PessoaId)
                                            OUTPUT INSERTED.PacienteId
                                           VALUES (@PessoaId);";
                    var parametersPaciente = new DynamicParameters();
                    parametersPaciente.Add("@PessoaId", pessoaId);
                    pacienteId = conexao.QuerySingle<int>(sqlPaciente, parametersPaciente);
                }
                else
                {
                    string sqlMedico = @"INSERT INTO Medico (PessoaId)
                                           OUTPUT INSERTED.MedicoId
                                          VALUES (@PessoaId);";
                    var parametersMedico = new DynamicParameters();
                    parametersMedico.Add("@PessoaId", pessoaId);
                    medicoId = conexao.QuerySingle<int>(sqlMedico, parametersMedico);
                }

                string sqlLogin = @"INSERT INTO Login (DataHora, PacienteId, MedicoId)
                                    VALUES (GETDATE(), @PacienteId, @MedicoId);";

                var parametersLogin = new DynamicParameters();
                parametersLogin.Add("@PacienteId", isPaciente ? pacienteId : null);
                parametersLogin.Add("@MedicoId", isPaciente ? null : medicoId);

                conexao.Execute(sqlLogin, parametersLogin);

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


        public bool UpdateLogin(Login login)
        {
            var conexao = new SqlConnection(_Conexao.StringDeConexao);
            string sql = @"
                UPDATE Login
                SET DataHora = @DataHora,
                    Nome = @Nome,
                    Email = @Email,
                    DataNascimento = @DataNascimento
                WHERE LoginId = @LoginId";

            var parameters = new DynamicParameters();
            parameters.Add("@DataHora", login.DataHora);
            parameters.Add("@Nome", login.Nome);
            parameters.Add("@Email", login.Email);
            parameters.Add("@DataNascimento", login.DataNascimento);
            parameters.Add("@LoginId", login.LoginId);

            try
            {
                var rowsAffected = conexao.Execute(sql, parameters);
                return rowsAffected > 0;
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
        public bool DeleteLogin(int loginId)
        {
            var conexao = new SqlConnection(_Conexao.StringDeConexao);
            string sql = "DELETE FROM Login WHERE LoginId = @LoginId";

            var parameters = new DynamicParameters();
            parameters.Add("@LoginId", loginId);

            try
            {
                var rowsAffected = conexao.Execute(sql, parameters);
                return rowsAffected > 0;
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
        private bool VerifyPassword(string password, string storedHash)
        {
            var hashedInput = HashPassword(password);
            return hashedInput == storedHash;
        }
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
