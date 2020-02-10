using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Specialized;

namespace SQLScripter.Scripter
{

    /// <summary>
    /// Clase para agrupar encapsulaciones de objetos SMO
    /// </summary>
    /// 
    public abstract class Wrapper
    {
        protected IScriptable obj;
        protected ScriptingOptions soCreate;
        protected ScriptingOptions soDrop;
        protected Urn urna;

        /// <summary>
        /// Constructor genérico para la clase, inicializa el objeto a envolver
        /// </summary>
        /// <param name="objeto">Objeto al cual envolver</param>
        public Wrapper(IScriptable objeto)
        {
            obj = objeto;
        }

        /// <summary>
        /// Crea el script adecuado del objeto SMO
        /// </summary>
        /// <param name="alter">Indica si se creara un script tipo "ALTER" en vez de "CREATE & DROP"</param>
        /// <param name="status">Indica el tipo de script de acuerdo con si es nuevo o no</param>
        /// <returns>Lista de strings que representan el script</returns>
        public virtual StringCollection script(string status, bool alter)
        {
            StringCollection s;
            if (status.Equals("S", StringComparison.OrdinalIgnoreCase))
            {
                s = this.newScript();
            }
            else if (alter)
            {
                s = this.alterScript();
            }
            else
            {
                s = this.oldScript();
            }
            return s;
        }

        /// <summary>
        /// Genera un script tipo ALTER para el objeto envuelto. Disponible solo para procedimientos
        /// </summary>
        /// <returns>Stringcollection que representa al script</returns>
        protected virtual StringCollection alterScript()
        {
            return this.oldScript();
        }

        /// <summary>
        /// Genera un script tipo CREATE & DROP para el objeto envuelto
        /// </summary>
        /// <returns>Stringcollection que representa al script</returns>
        protected StringCollection oldScript()
        {
            soCreate = new ScriptingOptions();
            soDrop = new ScriptingOptions();
            soDrop.IncludeHeaders = true;
            soDrop.IncludeDatabaseContext = true;
            soDrop.ScriptDrops = true;
            soDrop.IncludeIfNotExists = true;
            soCreate.SchemaQualify = true;
            soCreate.IncludeHeaders = true;

            StringCollection createScript = obj.Script(soCreate);
            StringCollection dropScript = obj.Script(soDrop);

            StringCollection script = new StringCollection();


            foreach (String s in dropScript)
            {
                script.Add(s + "\r\nGO\r\n");
            }
            foreach (String s in createScript)
            {
                script.Add(s + "\r\nGO\r\n");
            }

            return script;
        }

        /// <summary>
        /// Genera un script tipo CREATE para el objeto envuelto
        /// </summary>
        /// <returns>Stringcollection que representa al script</returns>
        protected StringCollection newScript()
        {
            soCreate = new ScriptingOptions();
            soCreate.IncludeDatabaseContext = true;
            soCreate.SchemaQualify = true;

            soCreate.IncludeHeaders = true;
            StringCollection createScript = obj.Script(soCreate);

            StringCollection script = new StringCollection();
            foreach (String s in createScript)
            {
                script.Add(s + "\r\nGO\r\n");
            }

            return script;
        }

        /// <summary>
        /// Adquiere la urn del objeto
        /// </summary>
        /// <returns>Urn del objeto</returns>
        public Urn getUrn()
        {
            return urna;
        }
    }

}
