﻿using System.Collections.Generic;
using System.Threading.Tasks;
using WpfAppTFG.Model.Interfaces;

namespace WpfAppTFG.Model.Respository
{
    /// <summary>
    /// Implementación de un Repositorio de <see cref="Log"/>
    /// </summary>
    public class LogRepository : IRepository<Log>
    {
        private readonly IDAO<Log> LogDAO;

        public LogRepository(IDAO<Log> logDAO)
        {
            this.LogDAO = logDAO;
        }

        /// <summary>
        /// Crea un objeto un log
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public async Task Create(Log log)
        {
            await LogDAO.Create(log);
        }

        /// <summary>
        /// Elimina un log
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public async Task Delete(Log log)
        {
            await LogDAO.Delete(log);
        }

        /// <summary>
        /// Obtiene un log
        /// </summary>
        /// <remarks>Puede ser nulo, si no existe</remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Log?> Read(int id)
        {
            var log = await LogDAO.Read(id);
            return log;
        }

        /// <summary>
        /// Obtiene todos los logs
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Log>> ReadAll()
        {
            var logs = await LogDAO.ReadAll();
            return logs;
        }

        /// <summary>
        /// Actualiza un log
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task Update(Log log)
        {
            await LogDAO.Update(log);
        }
    }
}
