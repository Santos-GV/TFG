using WpfAppTFG.Model.DAO;

namespace WpfAppTFG.Model.Respository
{
    /// <summary>
    /// Implementación de un Repositorio de <see cref="Log"/>
    /// </summary>
    public class LogRepository : Repository<Log>
    {

        public LogRepository() : base(new LogDAO())
        {
        }
    }
}
