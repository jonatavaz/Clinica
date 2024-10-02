using DAL;
using POJO;

namespace BLL
{
    public class PacienteBLL
    {
        private  PacienteDAL dao;
        public PacienteBLL()
        {
            dao = new PacienteDAL();
        }
        public List<Paciente> GetAllPacientes()
        {
            return dao.GetAllPacientes();
        }
        public Paciente GetPacienteById(int pacienteId)
        {
            if (pacienteId <= 0)
                throw new ArgumentException("O ID do paciente deve ser maior que zero.");

            return dao.GetPacienteById(pacienteId);
        }
        public bool AddPaciente(Paciente paciente)
        {

             return dao.AddPaciente(paciente);
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
