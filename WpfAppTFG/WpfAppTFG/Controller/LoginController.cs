﻿using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using WpfAppTFG.Model.Respository;

namespace WpfAppTFG.Controller
{
    public class LoginController
    {
        private readonly UserRepository userRepository;

        public LoginController()
        {
            userRepository = new UserRepository();
        }

        public async Task<bool> Login(string userName, string userPsswd)
        {
            var user = await userRepository.ReadAll()
                .Where(user => user.Name.Equals(userName))
                .FirstOrDefaultAsync();
            // Si no existe el usuario (es null) el login no es correcto
            if (user == null) return false;
            return user.CheckPsswd(userPsswd);
        }
    }
}