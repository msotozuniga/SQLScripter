
using Microsoft.SqlServer.Management.Smo;
using SQLScripter.PkgManager;
using SQLScripter.Structures;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SQLScripter.Scripter
{
    /// <summary>
    /// Clase destinada a la creación en conjuto de scripts
    /// </summary>
    public class Guionista
    {
        Server server;
        public bool alter;

        /// <summary>
        /// Contructor de clase. Inicializa el servidor a usar
        /// </summary>
        /// <param name="serverName">Nombre de servidor e instancia</param>
        /// <param name="user">Nombre de usuario</param>
        /// <param name="password">Contraseña del usuario</param>
        public Guionista(String serverName, String user, String password)
        {

            SqlConnection sqlConnection = new SqlConnection(@"Data Source=" + serverName + ";Initial Catalog=REALAIS_DESKMANAGER;User ID=" + user + ";Password=" + password + ";Max Pool Size=250");

            Microsoft.SqlServer.Management.Common.ServerConnection serverConnection = new Microsoft.SqlServer.Management.Common.ServerConnection(sqlConnection);

            server = new Server(serverConnection);

            alter = false;
        }

        /// <summary>
        /// Genera los scripts de los archivos mencionados en el documento
        /// </summary>
        /// <param name="library">Lista que contiene los datos de archivos mencionados en el documento</param>
        /// <param name="place">Ubicación en donde se creará los archivos</param>
        internal void generateScripts(FileLibrary library, String place)
        {
            while (library != null)
            {
                Wrapper file = this.serverExplorer(library.dbName, library.type, library.fileName);
                ScriptLocation sl = new ScriptLocation(library.dbName, library.type, library.fileName, file.script(library.newStatus, alter));
                PkgCreator.createFile(sl, place);
                library = library.getNextInLine();
            }
        }

        internal object[] createDependencyGraph(FileLibrary library)
        {
            var nodes = new Dictionary<string, Node>();
            var edges = new List<Tuple<string, string>>();
            while (library != null)
            {
                var node = new Node(library.dbName, library.type, library.fileName);
                nodes.Add(library.dbName + library.fileName, node);
                var file = this.serverExplorer(library.dbName, library.type, library.fileName);
                var nodeDeps = file.getDependencies(server);
                foreach (Tuple<string, string> t in nodeDeps)
                {
                    edges.Add(t);
                }
                library = library.getNextInLine();
            }

            return new object[] { nodes, edges };
        }

        internal List<Node> createDependencyList(object[] objs)
        {
            var nodes = (Dictionary<string, Node>)objs[0];
            var edges = (List<Tuple<string, string>>)objs[1];
            var list = new List<Node>();
            var noEdges = extractBaseNodes(edges, nodes);
            while (noEdges.Count > 0)
            {
                var n = noEdges.First();
                noEdges.Remove(n.Key);
                list.Add(n.Value);
                foreach (var e in edges.Where(e => e.Item1.Equals(n.Key)).ToList())
                {
                    if (!nodes.ContainsKey(e.Item2))
                    {
                        edges.Remove(e);
                        continue;
                    }
                    var m = nodes[e.Item2];
                    edges.Remove(e);
                    if (edges.All(me => me.Item2.Equals(e.Item2) == false))
                    {
                        noEdges.Add(e.Item2, m);
                    }
                }
            }
            list.Reverse();
            return list;
        }

        public static Dictionary<string, Node> extractBaseNodes(List<Tuple<string,string>> edges, Dictionary<string, Node> nodes)
        {
            var dict = new Dictionary<string, Node>();
            foreach (KeyValuePair<string, Node> node  in nodes)
            {
                if(edges.Where(e => e.Item2.Equals(node.Key)).Count() == 0)
                {
                    dict.Add(node.Key, node.Value);
                }
            }
            return dict;
        }

        /// <summary>
        /// Explora el servidor en busca del objeto dado la base de datos, tipo y nombre del objeto, y lo encapsula
        /// </summary>
        /// <param name="dbName">Nombre de la base de datos</param>
        /// <param name="type">Tipo del archivo</param>
        /// <param name="fileName">Nombre del archivo</param>
        /// <returns>El objeto indicado encapsulado</returns>
        private Wrapper serverExplorer(String dbName, String type, String fileName)
        {
            Database db = server.Databases[dbName];
            if (db == null)
            {
                return new NullWrapper(null);
            }
            Wrapper file;
            try
            {
                switch (type)
                {

                    case "Tablas":
                        file = new TableWrapper(db.Tables[fileName]);
                        break;

                    case "Funciones":
                        file = new UserDefinedFunctionWrapper(db.UserDefinedFunctions[fileName]);
                        break;

                    case "Vistas":
                        file = new ViewWrapper(db.Views[fileName]);
                        break;

                    case "Procedimientos Almacenados":
                        file = new StoredProcedureWrapper(db.StoredProcedures[fileName]);
                        break;

                    default:
                        throw new NullReferenceException();
                }
            }
            catch (System.NullReferenceException)
            {
                file = new NullWrapper(null);
            }
            return file;
        }


    }
}