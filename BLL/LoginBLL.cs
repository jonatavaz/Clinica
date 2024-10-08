using POJO;
using DAL;

namespace BLL
{
    public class LoginBLL
    {
        private LoginDAL dao;

        public LoginBLL()
        {
            dao = new LoginDAL();
        }

        public List<Login> GetAllLogins()
        {
            return dao.GetAllLogins();
        }

        public Login GetLoginById(int loginId)
        {
            if (loginId <= 0)
                throw new ArgumentException("O ID do login deve ser maior que zero.");

            return dao.GetLoginById(loginId);
        }

        public bool AddLogin(Login login, bool isPaciente)
        {
            ValidateLogin(login);

            if (login.DataHora == default(DateTime))
            {
                login.DataHora = DateTime.Now;
            }

            return dao.AddLogin(login, isPaciente);
        }

        public bool UpdateLogin(Login login)
        {
            if (login.LoginId <= 0)
                throw new ArgumentException("O ID do login deve ser maior que zero.");

            ValidateLogin(login);
            return dao.UpdateLogin(login);
        }

        public bool DeleteLogin(int loginId)
        {
            if (loginId <= 0)
                throw new ArgumentException("O ID do login deve ser maior que zero.");

            return dao.DeleteLogin(loginId);
        }

        public Login Authenticate(Login login)
         {
            ValidateLogin(login);
            
            var usuarioAutenticado = dao.GetLogin(login.Email, login.Senha);
            return usuarioAutenticado; 
        }

        private void ValidateLogin(Login login)
        {
            if (login == null)
                throw new ArgumentNullException(nameof(login), "O objeto Login não pode ser nulo.");

            
            if (string.IsNullOrWhiteSpace(login.Email))
                throw new ArgumentException("O email não pode ser vazio.");
            if (string.IsNullOrWhiteSpace(login.Senha))
                throw new ArgumentException("A senha não pode ser vazia.");
        }
    }
}