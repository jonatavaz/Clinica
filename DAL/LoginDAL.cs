using Microsoft.Data.SqlClient;
using POJO;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class LoginDAL
    {
        public Login GetLogin(string email, string senha)
        {
            SqlConnection conexao = new SqlConnection(_Conexao.StringDeConexao);

            string sql = @"                
                SELECT L.LoginId, l.DataHora, l.PacienteId, l.MedicoId,   
                ISNULL(p1.Nome, p2.Nome) Nome,
                ISNULL(p1.Email, p2.Email) Email,
                ISNULL(p1.DataNascimento, p2.DataNascimento) DataNascimento

                FROM Login L

                LEFT JOIN Paciente P
                ON L.PacienteId = P.PacienteId
                LEFT JOIN Pessoa P1	
                ON P.PacienteId = P1.PessoaId

                LEFT JOIN Medico M
                ON L.MedicoId = M.MedicoId
                LEFT JOIN Pessoa P2
                ON M.MedicoId = P2.PessoaId

                WHERE (p1.Email = @Email and p1.Senha = @Senha) 
                OR (p2.Email = @Email and p2.Senha = @Senha)";

            var parameters = new DynamicParameters();
            parameters.Add("@Email", email);
            parameters.Add("@Senha", senha);

            try
            {
                var result = conexao.QueryFirstOrDefault<Login>(sql, parameters);
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public List<Login> GetAllLogins()
        {
            SqlConnection conexao = new SqlConnection(_Conexao.StringDeConexao);
            
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
        }

        

        public Login GetLoginById(int loginId)
        {
            SqlConnection conexao = new SqlConnection(_Conexao.StringDeConexao);
            
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
            
        }
        
        public bool AddLogin(Login login)
        {
            SqlConnection conexao = new SqlConnection(_Conexao.StringDeConexao);

            string sql = @"INSERT INTO Pessoa (Nome, Email, Senha, DataNascimento)
                           VALUES (@Nome, @Email, @Senha, @DataNascimento);";

                var parameters = new DynamicParameters();
                parameters.Add("@Nome", login.Nome);
                parameters.Add("@Email", login.Email);
                parameters.Add("@Senha", login.Senha);
                parameters.Add("@DataNascimento", login.DataNascimento);

                try
                {
                    var result = conexao.Execute(sql, parameters);
                    return true; 
                }
                catch (Exception ex)
                {
                    
                    throw;
                }
        }
        
        public bool UpdateLogin(Login login)
        {
            SqlConnection conexao = new SqlConnection(_Conexao.StringDeConexao);
            
                string sql = @"UPDATE Login
                                SET DataHora = @DataHora,
                                Nome = @Nome,
                                Email = @Email,
                                DataNascimento = @DataNascimeno
                                WHERE LoginId = @LoginId";

                var parameters = new DynamicParameters();
                parameters.Add("@DataHora", login.DataHora);
                parameters.Add("@Nome", login.Nome);
                parameters.Add("@Email", login.Email);
                parameters.Add("@DataNascimento", login.DataNascimento);

                try
                {
                    var rowsAffected = conexao.Execute(sql, parameters);
                    return rowsAffected > 0; 
                }
                catch (Exception ex)
                {
                    
                    throw;
                }
        }
        
        public bool DeleteLogin(int loginId)
        {
            SqlConnection conexao = new SqlConnection(_Conexao.StringDeConexao);
            
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
        }
    
    }
}