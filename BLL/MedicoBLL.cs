using DAL;
using POJO;

namespace BLL
{
    public class MedicoBLL
    {
        private MedicoDAL dao;

        public MedicoBLL() 
        {
            dao = new MedicoDAL();
        }

        public List<Medico> GetMedicos()
        {
            return dao.GetMedicos();
        }

    }
}
