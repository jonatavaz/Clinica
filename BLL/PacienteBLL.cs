using DAL;
using POJO;
using Dapper;
using System.Collections.Generic;

namespace BLL
{
    public class PacienteBLL
    {
        private  PacienteDAL dao;

        public PacienteBLL()
        {
            dao = new PacienteDAL();
        }

        public List<Paciente> GetPacientes()
        {
            return dao.GetPacientes();
        }

        public void AddPaciente(Paciente paciente)
        {

            dao.AddPaciente(paciente);
        }

        public void UpdatePaciente(Paciente paciente)
        {

            dao.UpdatePaciente(paciente);
        }

        public void DeletePaciente(int id)
        {

            dao.DeletePaciente(id);
        }
    }
}
