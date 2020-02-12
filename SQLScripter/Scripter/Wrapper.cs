using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

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

        internal List<Tuple<string, string>> getDependencies(Server server)
        {
            var list = new List<Tuple<string, string>>();
            var dc = getUrnOrderList(urna, server);
            var nodeId = dc[dc.Count - 1].Urn.Parent.GetAttribute("Name") + dc[dc.Count - 1].Urn.GetAttribute("Name");
            for (int i = 0; i < dc.Count - 1; i++)
            {
                var id = dc[i].Urn.Parent.GetAttribute("Name") + dc[i].Urn.GetAttribute("Name");
                list.Add(new Tuple<string, string>(nodeId,id));
            }

            return list.Distinct().ToList();

        }

        private DependencyCollection getUrnOrderList(Urn urn, Server server)
        {
            Urn[] urns = new Urn[] { urn };
            DependencyWalker walker = new DependencyWalker(server);
            DependencyTree tree = walker.DiscoverDependencies(urns, DependencyType.Parents);
            DependencyCollection nodes = walker.WalkDependencies(tree);
            DependencyCollection list = new DependencyCollection();
            foreach (DependencyCollectionNode d in nodes)
            {
                if (!d.Urn.Parent.GetAttribute("Name").Equals(urn.Parent.GetAttribute("Name")))
                {
                    var tmp = getUrnOrderList(d.Urn, server);
                    list.AddRange(tmp);
                }
            }
            list.AddRange(nodes);
            return list;
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
