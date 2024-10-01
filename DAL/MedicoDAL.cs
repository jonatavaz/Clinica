using Microsoft.Data.SqlClient;
using POJO;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MedicoDAL
    {
        public List<Medico> GetMedicos()
        {
            SqlConnection conexao = new SqlConnection(_Conexao.StringDeConexao);
            string sql = @$"select * from Medico where nome like @Nome";

            try
            {
                //insert/update/delete --- execute

                //listagem - query

                var result = conexao.Query<Medico>(sql).ToList();
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
        public Medico GetMedicoById(int MedicoId)
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

        public bool AddMedico(Medico medico)
        {
            SqlConnection conexao = new SqlConnection(_Conexao.StringDeConexao);

            string sql = "INSERT INTO Paciente (Nome, Nome) VALUES (@Nome, @Email, @Senha)";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Nome", medico.Nome);
            p

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

        public bool UpdateMedico(Medico medico)
        {
            SqlConnection conexao = new SqlConnection(_Conexao.StringDeConexao);

            string sql = "UPDATE Paciente SET Nome = @Nome, Email = @Email, Senha = @Senha WHERE PacienteId = @PacienteId";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Nome", medico.Nome);
            parameters.Add("@Email", medico.Email);
            parameters.Add("@Senha", medico.Senha);
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

        public bool DeleteMedico(int PacienteId)
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
