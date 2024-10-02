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
        public List<Medico> GetAllMedicos()
        {
            SqlConnection conexao = new SqlConnection(_Conexao.StringDeConexao);
            string sql = @$"select * from Medico";

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

            string sql = @"SELECT * FROM Medico WHERE MedicoId = @MedicoId";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MedicoId", MedicoId);
            try
            {
                var result = conexao.QueryFirstOrDefault<Medico>(sql, parameters);
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

            string sql = @"INSERT INTO Medico (Nome, Email, Senha, DataNascimento, NomeEspecialidade) 
                VALUES (@Nome, @Email, @Senha, @DataNascimento, @NomeEspecialidade)";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Nome", medico.Nome);
            parameters.Add("@Email", medico.Email);
            parameters.Add("@Senha", medico.Senha);
            parameters.Add("@DataNascimento", medico.DataNascimento);
            parameters.Add("@Especialidade", medico.NomeEspecialidade);

            try
            {
                var result = conexao.Query<Medico>(sql, parameters).ToList();
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

            string sql = "UPDATE Medico SET Nome = @Nome, Email = @Email, Senha = @Senha, DataNascimento = @DataNascimento, NomeEspecialidade = @NomeEspecialidade WHERE MedicoId = @MedicoId";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Nome", medico.Nome);
            parameters.Add("@Email", medico.Email);
            parameters.Add("@Senha", medico.Senha);
            parameters.Add("@DataNascimento", medico.DataNascimento);
            parameters.Add("@NomeEspecialidade", medico.NomeEspecialidade);
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

        public bool DeleteMedico(int MedicoId)
        {
            SqlConnection conexao = new SqlConnection(_Conexao.StringDeConexao);
            string sql = "DELETE FROM Medico WHERE MedicoId = @MedicoId";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MedicoId", MedicoId);

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
