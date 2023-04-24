using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace WpfAppTFG.Model
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }
        [BsonElement("nombre")]
        public string Name { get; set; }
        [BsonElement("pombre")]
        private string psswd;
        public string Psswd { get => psswd; set => HashSHA256Base64(value, Sal); }
        [BsonElement("sal")]
        public string Sal { get; private set; }
        [BsonElement("rol")]
        public string Rol { get; set; }
        [BsonElement("favoritos")]
        public List<Post> Favoritos { get; set; }
        [BsonElement("pendientes")]
        public List<Post> Pendientes { get; set; }

        public User(string name, string psswd, string sal, string rol)
        {
            Name = name;
            Psswd = HashSHA256Base64(psswd, sal);
            Sal = sal;
            Rol = rol;
            Favoritos = new List<Post>();
            Pendientes = new List<Post>();
        }

        public User(string name, string psswd, string rol)
        {
            Name = name;
            Sal = GenerarSal();
            Psswd = HashSHA256Base64(psswd, Sal);
            Rol = rol;
        }

        /// <summary>
        /// Compara una contraseña en texto plano con su sal, con la contraseña real encriptada
        /// </summary>
        /// <param name="input">contraseña en texto plano</param>
        /// <param name="sal">sal del usuario</param>
        /// <param name="realPsswd">contraseña real</param>
        /// <returns></returns>
        private bool CompararContraseñas(string input, string sal, string realPsswd)
        {
            string hashedInput = HashSHA256Base64(input, sal);
            return realPsswd.Equals(hashedInput);
        }

        /// <summary>
        /// Crea una sal aleatoria
        /// </summary>
        /// <returns>sal</returns>
        private string GenerarSal()
        {
            using (RandomNumberGenerator rand = RandomNumberGenerator.Create())
            {
                byte[] sal = new byte[16];
                rand.GetBytes(sal);
                return Convert.ToBase64String(sal);
            }
        }

        /// <summary>
        /// Hashea una contraseña con su sal
        /// </summary>
        /// <param name="psswd">contraseña en texto plano</param>
        /// <param name="sal">sal del usuario</param>
        /// <returns>contraseña encriptada en SHA256 en base64</returns>
        private string HashSHA256Base64(string psswd, string sal)
        {
            using (SHA256 sha256Hasher = SHA256.Create())
            {
                byte[] preHashedPsswd = Encoding.UTF8.GetBytes(string.Concat(sal, psswd));
                byte[] hashedPsswd = sha256Hasher.ComputeHash(preHashedPsswd);
                return Convert.ToBase64String(hashedPsswd);
            }
        }
    }
}
