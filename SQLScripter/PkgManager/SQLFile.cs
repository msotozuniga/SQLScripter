using System;
using System.Collections.Specialized;
using System.IO;

namespace SQLScripter.PkgManager
{
    /// <summary>
    /// Clase dedicaca a la cración de archivos SQL (scripts)
    /// </summary>
    class SQLFile : Package
    {

        private StringCollection script;



        /// <summary>
        /// Constructor de archivo SQL con nombre y script
        /// </summary>
        /// <param name="name">Nombre de archivo</param>
        /// <param name="fileScript">Contenido del archivo</param>
        public SQLFile(String name, StringCollection fileScript) : base(name)
        {
            this.script = fileScript;
        }

        /// <summary>
        /// Contructor del archivo con nombre, ubicación y contenido
        /// </summary>
        /// <param name="name">Nombre de archivo</param>
        /// <param name="location">Ubicacion del archivo</param>
        /// <param name="fileScript">Contenido del archivo</param>
        public SQLFile(String name, String location, StringCollection fileScript) : base(name, location)
        {
            this.script = fileScript;
        }


        /// <summary>
        /// Crea el archivo SQL en la ubicación dada
        /// </summary>
        public override void createPackage()
        {
            try
            {
                using (StreamWriter sw = File.CreateText(path + ".sql"))
                {
                    foreach (String s in script)
                    {
                        sw.WriteLine(s);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("No se pudo crear correctamente el archivo " + pkgName + ".sql");
            }

        }

    }
}
