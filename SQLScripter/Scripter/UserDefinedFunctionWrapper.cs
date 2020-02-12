using Microsoft.SqlServer.Management.Smo;

namespace SQLScripter.Scripter
{
    /// <summary>
    /// Encapsulador para funciones definidas por el usuario
    /// </summary>  
    public class UserDefinedFunctionWrapper : Wrapper
    {
        /// <summary>
        /// Constructor de clase para objetos tipo "UserDefinedFunction", inicializa la urn nula
        /// </summary>
        /// <param name="obj">Objeto a envolver</param>
        public UserDefinedFunctionWrapper(UserDefinedFunction obj) : base(obj)
        {
            urna = obj.Urn;
        }
    }
}
