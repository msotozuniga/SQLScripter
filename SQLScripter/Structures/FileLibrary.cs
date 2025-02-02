﻿namespace SQLScripter.Structures
{
    /// <summary>
    /// Estructura tipo lista enlazada destinada a traspasar la información de un documento a algo simple para el guionista.
    /// </summary>
    public class FileLibrary
    {
        public string dbName { get; }
        public string type { get; }
        public string fileName { get; }

        public string newStatus { get; }

        private FileLibrary next;

        /// <summary>
        /// Constructor de un elemento de la lista, recibe la base de datos, el tipo del archivo referido y el nombre del archivo
        /// </summary>
        /// <param name="db">Nombre de la base de datos</param>
        /// <param name="type">Tipo del archivo</param>
        /// <param name="file">Nombre del archivo</param>
        public FileLibrary(string db, string type, string file, string nuevo)
        {
            this.dbName = db;
            this.type = type;
            this.fileName = file;
            this.newStatus = nuevo;
        }

        /// <summary>
        /// Método accesor para el siguiente elemento de la lista
        /// </summary>
        /// <returns>Los siguientes elementos de la lista</returns>
        internal FileLibrary getNextInLine()
        {
            return next;
        }

        /// <summary>
        /// Une una lista al final de la que llame esta función
        /// </summary>
        /// <param name="rowLibrary"></param>
        internal void addLibrary(FileLibrary rowLibrary)
        {
            if (next == null)
            {
                next = rowLibrary;
                return;
            }
            next.addLibrary(rowLibrary);
        }
    }
}
