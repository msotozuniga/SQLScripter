using Microsoft.SqlServer.Management.Smo;

namespace SQLScripter.Scripter
{
    /// <summary>
    /// Encapsulador para tablas
    /// </summary>
    public class TableWrapper : Wrapper
    {
        /// <summary>
        /// Constructor de clase para objetos tipo "Table", inicializa la urn nula
        /// </summary>
        /// <param name="obj">Objeto a envolver</param>
        public TableWrapper(Table obj) : base(obj)
        {
            urna = obj.Urn;

        }

    }
}
