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
        public List<dynamic> GetConsultasPorUsuario(string pacienteId)
        {
            using (SqlConnection conexao = new SqlConnection(_Conexao.StringDeConexao))
            {
                string sql = @"SELECT 
                                C.ConsultaId,
                                C.DataHora,
                                C.ConsultaConfirmada,
                                M.MedicoId,
                                P.PacienteId,
                                PS.Nome AS NomePaciente,
                                PM.Nome AS NomeMedico,
                                PS.Email AS EmailPaciente
                            FROM 
                                Consulta C
                            INNER JOIN 
                                Medico M ON C.MedicoId = M.MedicoId
                            INNER JOIN 
                                Paciente P ON C.PacienteId = P.PacienteId
                            INNER JOIN 
                                Pessoa PM ON PM.PessoaId = M.PessoaId 
                            INNER JOIN 
                                Pessoa PS ON PS.PessoaId = P.PessoaId;";

                var parameters = new DynamicParameters();
                parameters.Add("@PacienteId", pacienteId);

                try
                {
                    var result = conexao.Query<dynamic>(sql, parameters).ToList();
                    return result;
                }
                catch (Exception ex)
                {
                    throw; 
                }
            } 
        }
        public Consulta GetConsultaById(int ConsultaId)
        {
            SqlConnection conexao = new SqlConnection(_Conexao.StringDeConexao);

            string sql = @"SELECT 
                            C.ConsultaId,
                            C.DataHora,
                            C.ConsultaConfirmada,
                            M.MedicoId,
                            P.PacienteId,
                            PS.Nome ,
                            PM.Nome ,
                            PS.Email
                        FROM 
                            Consulta C
                        INNER JOIN 
                            Medico M ON C.MedicoId = M.MedicoId
                        INNER JOIN 
                            Paciente P ON C.PacienteId = P.PacienteId
                        INNER JOIN 
                            Pessoa PM ON PM.PessoaId = M.PessoaId  
                        INNER JOIN 
                            Pessoa PS ON PS.PessoaId = P.PessoaId
                        WHERE C.ConsultaId = @ConsultaId";

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

        public List<Consulta> GetConsultasPorMedicoEData(int medicoId, DateTime dataHora)
        {
            var conexao = new SqlConnection(_Conexao.StringDeConexao);
            
                string sql = @"SELECT * 
                       FROM Consulta 
                       WHERE MedicoId = @MedicoId 
                       AND CAST(DataHora AS DATE) = @DataHora"; 

                var parameters = new DynamicParameters();
                parameters.Add("@MedicoId", medicoId);
                parameters.Add("@DataHora", dataHora.Date); 

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

        public bool AddConsulta(Consulta consulta)
        {
            SqlConnection conexao = new SqlConnection(_Conexao.StringDeConexao);

            string sql = @"INSERT INTO Consulta (MedicoId, PacienteId, DataHora, ConsultaConfirmada) 
                               VALUES (@MedicoId, @PacienteId, @DataHora, @ConsultaConfirmada)";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MedicoId", consulta.Medico.MedicoId);
            parameters.Add("@PacienteId", consulta.Paciente.PacienteId);
            parameters.Add("@DataHora", consulta.DataHora);
            parameters.Add("@ConsultaConfirmada", consulta.ConsultaConfirmada);

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
                           SET MedicoId = @MedicoId, PacienteId = @PacienteId, DataHora = @DataHora, ConsultaConfirmada = @ConsultaConfirmada 
                           WHERE ConsultaId = @ConsultaId;";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MedicoId", consulta.MedicoId);
            parameters.Add("@PacienteId", consulta.PacienteId);
            parameters.Add("@DataHora", consulta.DataHora);
            parameters.Add("@ConsultaConfirmada", consulta.ConsultaConfirmada);

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

            string sql = "DELETE FROM Consulta WHERE ConsultaId = @ConsultaId;";

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
