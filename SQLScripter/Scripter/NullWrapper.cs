using Microsoft.SqlServer.Management.Smo;
using System.Collections.Specialized;

namespace SQLScripter.Scripter
{
    /// <summary>
    /// Encapsulador para objetos nulos
    /// </summary>
    public class NullWrapper : Wrapper
    {
        /// <summary>
        /// Constructor de clase nula para errores o objetos no soportados, inicializa la urn nula
        /// </summary>
        /// <param name="obj">Objeto a envolver</param>
        public NullWrapper(IScriptable obj) : base(obj)
        {
            urna = null;
        }

        /// <summary>
        /// Generación de un script nulo para caso de errores o objetos no soportados
        /// </summary>
        /// <param name="alter">Indica si se creara un script tipo "ALTER" en vez de "CREATE & DROP"</param>
        /// <param name="status">Indica el tipo de script de acuerdo con si es nuevo o no</param>
        /// <returns>Nulo</returns>
        public override StringCollection script(string status, bool alter)
        {
            return null;
        }
    }

}
