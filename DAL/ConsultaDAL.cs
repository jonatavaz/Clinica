using Microsoft.Data.SqlClient;
using Dapper;
using POJO;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class ConsultaDAL
    {

        public List<Consulta> GetConsultas()
        {
            SqlConnection conexao = new SqlConnection(_Conexao.StringDeConexao);

            string sql = "SELECT * FROM Consulta";

            try
            {
                var result = conexao.Query<Consulta>(sql).ToList();
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

        public List<Consulta> GetConsultasPorUsuario(string usuarioId)
        {
            SqlConnection conexao = new SqlConnection(_Conexao.StringDeConexao);

            string sql = "SELECT * FROM Consulta WHERE UsuarioId = @UsuarioId";

            var parameters = new DynamicParameters();
            parameters.Add("@UsuarioId", usuarioId);

            try
            {
                var result = conexao.Query<Consulta>(sql, parameters).ToList();
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

        public Consulta GetConsultaById(int ConsultaId)
        {
            SqlConnection conexao = new SqlConnection(_Conexao.StringDeConexao);

            string sql = "SELECT * FROM Consulta WHERE ConsultaId = @ConsultaId";

            var parameters = new DynamicParameters();
            parameters.Add("@ConsultaId", ConsultaId);

            try
            {
                var result = conexao.QueryFirstOrDefault<Consulta>(sql, parameters);
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

        public bool AddConsulta(Consulta consulta)
        {
            SqlConnection conexao = new SqlConnection(_Conexao.StringDeConexao);

            string sql = @"INSERT INTO Consulta (MedicoId, PacienteId, DataHora, EmailPaciente) 
                               VALUES (@MedicoId, @PacienteId, @DataHora, @EmailPaciente)";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MedicoId", consulta.MedicoId);
            parameters.Add("@PacienteId", consulta.PacienteId);
            parameters.Add("@DataHora", consulta.DataHora);
            parameters.Add("@Email", consulta.EmailPaciente);

            try
            {
                var result = conexao.Execute(sql, parameters);
                return result > 0;
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


        public bool UpdateConsulta(Consulta consulta)
        {
            SqlConnection conexao = new SqlConnection(_Conexao.StringDeConexao);

            string sql = @"UPDATE Consulta 
                               SET MedicoId = @MedicoId, PacienteId = @PacienteId, DataHora = @DataHora, 
                                   EmailPaciente = @Email 
                               WHERE ConsultaId = @Id";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MedicoId", consulta.MedicoId);
            parameters.Add("@PacienteId", consulta.PacienteId);
            parameters.Add("@DataHora", consulta.DataHora);
            parameters.Add("@Email", consulta.EmailPaciente);

            try
            {
                var result = conexao.Execute(sql, consulta);
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

        
        public bool DeleteConsulta(int ConsultaId)
        {
            SqlConnection conexao = new SqlConnection(_Conexao.StringDeConexao);

            string sql = "DELETE FROM Consulta WHERE ConsultaId = @ConsultaId";

            var parameters = new DynamicParameters();
            parameters.Add("@ConsultaId", ConsultaId);

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
