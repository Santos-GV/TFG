using System.Collections.Generic;
using System.Threading.Tasks;
using WpfAppTFG.Model.Interfaces;

namespace WpfAppTFG.Model.Respository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDAO<User> userDAO;

        public UserRepository(IDAO<User> userDAO)
        {
            this.userDAO = userDAO;
        }

        public async Task Create(User user)
        {
            await userDAO.Create(user);
        }

        public async Task Delete(User user)
        {
            await userDAO.Delete(user);
        }

        public async Task<User?> Read(int id)
        {
            return await userDAO.Read(id);
        }

        public async Task<IEnumerable<User>> ReadAll()
        {
            return await userDAO.ReadAll();
        }

        public async Task Update(User user)
        {
            await userDAO.Update(user);
        }
    }
}
