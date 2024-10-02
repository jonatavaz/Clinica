using DAL;
using POJO;


namespace BLL
{
    public class ConsultaBLL
    {
        private ConsultaDAL dao;

        public ConsultaBLL()
        {
            dao = new ConsultaDAL();
        }

        public List<Consulta> GetAllConsultas()
        {
            return dao.GetConsultas();
        }

        public List<Consulta> GetConsultasPorUsuario(string pacienteId)
        {
            return dao.GetConsultasPorUsuario(pacienteId);
        }

        public Consulta GetConsultaById(int ConsultaId)
        {
            return dao.GetConsultaById(ConsultaId);
        }

        public bool CreateConsulta(Consulta consulta)
        {
            return dao.AddConsulta(consulta);
        }

        public bool UpdateConsulta(Consulta consulta)
        {
           return dao.UpdateConsulta(consulta);
        }

        public bool DeleteConsulta(int ConsultaId)
        {
            return dao.DeleteConsulta(ConsultaId);
        }
    }
}
