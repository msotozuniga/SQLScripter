
using System.Collections.Specialized;

namespace SQLScripter.Structures
{
    /// <summary>
    /// Estructura usada para crear la estructura adecuada de directorios y archivos correspondientes a scripts
    /// </summary>
    public class ScriptLocation
    {
        public string dbName { get; }
        public string type { get; }
        public string fileName { get; }
        public StringCollection script { get; }

        /// <summary>
        /// Genera la estructura indicando la base de datos, el tipo, el nombre y el contenido del archivo que se debe crear
        /// </summary>
        /// <param name="dbName">Nombre de la base de datos</param>
        /// <param name="type">Tipo del archivo</param>
        /// <param name="fileName">Nombre del archivo</param>
        /// <param name="stringCollection">Contenido del archivo</param>
        public ScriptLocation(string dbName, string type, string fileName, StringCollection stringCollection)
        {
            this.dbName = dbName;
            this.type = type;
            this.fileName = fileName;
            this.script = stringCollection;
        }
    }
}
