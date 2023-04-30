﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using WpfAppTFG.Model.Interfaces;

namespace WpfAppTFG.Model
{
    /// <summary>
    /// Representa una cuenta de usuario
    /// </summary>
    public class User : IIdentifiable
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("nombre")]
        public string Name { get; set; }
        [BsonElement("psswd")]
        private string psswd;
        [BsonIgnore]
        public string Psswd { get => psswd; set => HashSHA256Base64(value, Sal); }
        [BsonElement("sal")]
        public string Sal { get; private set; }
        [BsonElement("rol")]
        public Rol Rol { get; set; }
        [BsonElement("favoritos")]
        public List<Post> Favoritos { get; set; }
        [BsonElement("pendientes")]
        public List<Post> Pendientes { get; set; }

        public User(string name, string psswd, Rol rol)
        {
            Name = name;
            Sal = GenerarSal();
            this.psswd = HashSHA256Base64(psswd, Sal);
            Rol = rol;
            Favoritos = new List<Post>();
            Pendientes = new List<Post>();
        }

        /// <summary>
        /// Obtiene el Id del <see cref="User"/>
        /// </summary>
        /// <returns></returns>
        public string GetId()
        {
            return this.Id;
        }

        /// <summary>
        /// Compara una contraseña en texto plano con su sal,
        /// con la contraseña real encriptada
        /// </summary>
        /// <param name="input">contraseña en texto plano</param>
        /// <returns></returns>
        public bool CheckPsswd(string input)
        {
            string hashedInput = HashSHA256Base64(input, Sal);
            return Psswd.Equals(hashedInput);
        }

        /// <summary>
        /// Crea una sal aleatoria
        /// Los valores devuletos por este método son valores aleatorios
        /// criptográficamente seguros y aleatorios.
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
