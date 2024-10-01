using DAL;
using POJO;
using System.Collections.Generic;

namespace BLL
{
    public class ConsultaBLL
    {
        private ConsultaDAL dao;

        public ConsultaBLL()
        {
            dao = new ConsultaDAL();
        }

        public List<Consulta> GetConsultas()
        {
            return dao.GetConsultas();
        }

        public List<Consulta> GetConsultasPorUsuario(string usuarioId)
        {
            return dao.GetConsultasPorUsuario(usuarioId);
        }

        public Consulta GetConsultaById(int id)
        {
            return dao.GetConsultaById(id);
        }

        public bool CreateConsulta(Consulta consulta)
        {
            return dao.AddConsulta(consulta);
        }

        public void UpdateConsulta(Consulta consulta)
        {
            dao.UpdateConsulta(consulta);
        }

        public void DeleteConsulta(int id)
        {
            dao.DeleteConsulta(id);
        }
    }
}
