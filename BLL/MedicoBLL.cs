using System;
using System.Collections.Generic;
using POJO;
using DAL;

namespace BLL
{
    public class MedicoBLL
    {
        private MedicoDAL dao;
        public MedicoBLL()
        {
            dao = new MedicoDAL();
        }
        
        public List<Medico> GetAllMedicos()
        {
            return dao.GetAllMedicos();
        }
        

        public Medico GetMedicoPorNome(string nome)
        {
            return dao.GetMedicoPorNome(nome);
        }



        public Medico GetMedicoById(int medicoId)
        {
            if (medicoId <= 0)
                throw new ArgumentException("O ID do médico deve ser maior que zero.");

            return dao.GetMedicoById(medicoId);
        }

        
        public bool AddMedico(Medico medico)
        {
            ValidateMedico(medico); 

            return dao.AddMedico(medico);
        }

        
        public bool UpdateMedico(Medico medico)
        {
            if (medico.MedicoId <= 0)
                throw new ArgumentException("O ID do médico deve ser maior que zero.");

            ValidateMedico(medico); 

            return dao.UpdateMedico(medico);
        }

        
        public bool DeleteMedico(int medicoId)
        {
            if (medicoId <= 0)
                throw new ArgumentException("O ID do médico deve ser maior que zero.");

            return dao.DeleteMedico(medicoId);
        }

        
        private void ValidateMedico(Medico medico)
        {
            if (medico == null)
                throw new ArgumentNullException(nameof(medico), "O objeto Médico não pode ser nulo.");

            if (string.IsNullOrWhiteSpace(medico.Nome))
                throw new ArgumentException("O nome do médico não pode ser vazio.");

            
            if (medico.Especialidade == null || medico.Especialidade.EspecialidadeId <= 0)
                throw new ArgumentException("A especialidade do médico deve ser válida.");
        }
    }
}