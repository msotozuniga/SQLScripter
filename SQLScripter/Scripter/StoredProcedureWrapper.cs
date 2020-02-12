using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Specialized;

namespace SQLScripter.Scripter
{
    /// <summary>
    /// Encapsulador para procedimientos
    /// </summary>
    public class StoredProcedureWrapper : Wrapper
    {
        /// <summary>
        /// Constructor de clase para objetos tipo "StoredProcedure", inicializa la urn nula
        /// </summary>
        /// <param name="obj">Objeto a envolver</param>
        public StoredProcedureWrapper(StoredProcedure obj) : base(obj)
        {
            urna = obj.Urn;

        }

        /// <summary>
        /// Genera un script tipo ALTER para el objeto envuelto. Disponible solo para procedimientos
        /// </summary>
        /// <returns>Stringcollection que representa al script</returns>
        protected override StringCollection alterScript()
        {
            soCreate = new ScriptingOptions();
            soCreate.IncludeDatabaseContext = true;
            soCreate.SchemaQualify = true;
            soCreate.IncludeHeaders = true;

            StringCollection alterScript = obj.Script(soCreate);

            StringCollection script = new StringCollection();
            for (int i = 0; i < alterScript.Count; i++)
            {
                if (i == alterScript.Count - 1)
                {
                    var s = alterScript[i].Remove(0, alterScript[i].IndexOf("PROCEDURE", StringComparison.OrdinalIgnoreCase));
                    script.Add("ALTER " + s + "\r\nGO\r\n");
                }
                else
                {
                    script.Add(alterScript[i] + "\r\nGO\r\n");
                }
            }

            return script;

        }
    }
}
