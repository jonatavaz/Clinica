﻿using Microsoft.Data.SqlClient;
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
        public List<Consulta> GetConsultasPorUsuario(string ConsultaId)
        {
            SqlConnection conexao = new SqlConnection(_Conexao.StringDeConexao);

            string sql = @"SELECT * FROM Consulta C 
                           INNER JOIN Medico M 
                           ON C.MedicoId = M.MedicoId
                           INNER JOIN Paciente P 
                           ON C.PacienteId = P.PacienteId
                           INNER JOIN Pessoa PS 
                           ON PS.PessoaId = C.PacienteId;";
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

            string sql = @"INSERT INTO Consulta (MedicoId, PacienteId, DataHora, ConsultaConfirmada) 
                               VALUES (@MedicoId, @PacienteId, @DataHora, @ConsultaConfirmada)";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MedicoId", consulta.MedicoId);
            parameters.Add("@PacienteId", consulta.PacienteId);
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
                           WHERE ConsultaId = @ConsultaId";

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
