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
            string sql = "SELECT * FROM Paciente";

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

        public bool AddPaciente(Paciente paciente)
        {
            SqlConnection conexao = new SqlConnection(_Conexao.StringDeConexao);

            string sql = "INSERT INTO Paciente (Nome, Email, Senha, DataNascimento) VALUES (@Nome, @Email, @Senha, @DataNascimento)";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Nome", paciente.Nome);
            parameters.Add("@Email", paciente.Email);
            parameters.Add("@Senha", paciente.Senha);
            parameters.Add("@DataNascimento", paciente.DataNascimento);

            try
            {
                var result = conexao.Query<Paciente>(sql, parameters).ToList();
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
