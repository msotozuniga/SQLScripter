using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Smo;
using SQLScripter.PkgManager;
using SQLScripter.Structures;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SQLScripter.Scripter
{
    /// <summary>
    /// Clase destinada a la creación
    /// </summary>
    public class Guionista
    {
        Server server;
        public bool alter;

        /// <summary>
        /// Contructor de clase. Inicializa el server entregado
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
                ScriptLocation sl = new ScriptLocation(library.dbName, library.type, library.fileName, file.script(library.newStatus,alter));
                PkgCreator.createFile(sl, place);
                library = library.getNextInLine();
            }
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