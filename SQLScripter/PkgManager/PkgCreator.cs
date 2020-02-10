using ICSharpCode.SharpZipLib.Zip;
using SQLScripter.Structures;
using System;
using System.IO;

namespace SQLScripter.PkgManager
{
    /// <summary>
    /// Clase destinada a crear un comprimido de un directorio
    /// </summary>
    public static class PkgCreator
    {
        /// <summary>
        /// Genera un comprimido con el formato dado
        /// </summary>
        /// <param name="format">Formato a comprimir</param>
        /// <param name="zipPath">Ubicación donde se creara el comprimido</param>
        /// <param name="pkgPath">Directorio archivo a comprimir</param>
        public static void generatePkg(String format, String zipPath, String pkgPath)
        {
            switch (format)
            {
                case "zip":
                    createZipPkg(zipPath, pkgPath);
                    break;
                default:
                    throw new Exception();
            }
        }

        /// <summary>
        /// Genera un comprimido formato zip
        /// </summary>
        /// <param name="zipPath">Ubicación donde se creara el comprimido</param>
        /// <param name="pkgPath">Directorio archivo a comprimir</param>
        private static void createZipPkg(String zipName, String pkgPath)
        {
            FastZip newZip = new FastZip();
            newZip.CreateZip(zipName + ".zip", pkgPath, true, null); // Creado en Debug por temas de permisos
            foreach (var subDir in new DirectoryInfo(pkgPath).GetDirectories())
            {
                subDir.Delete(true);
            }
            File.Delete(pkgPath + @"\errors.txt");
        }

        /// <summary>
        /// Genera un archivo sql en la carpeta indicada para entregas
        /// </summary>
        /// <param name="script">Estructura que indique donde se debe colocar el script</param>
        /// <param name="place">Ubicación donde se colocará la carpeta raiz</param>
        public static void createFile(ScriptLocation script, String place)
        {
            if (script.script == null)
            {
                addToLog(script, place);
                return;
            }
            CompositePackage root = new CompositePackage("Base de datos", place);
            CompositePackage db = new CompositePackage(script.dbName);
            CompositePackage type = new CompositePackage(script.type);
            SQLPackage file = new SQLPackage(script.fileName, script.script);

            root.addPackage(db);
            db.addPackage(type);
            type.addPackage(file);

            root.createPackage();
        }

        /// <summary>
        /// Crea o añade datos a un archivo "errors.txt" que contiene los datos de los objetos no procesados
        /// </summary>
        /// <param name="script">Estructura que indique los datos del objeto</param>
        /// <param name="place">Ubicación donde se depositará el mensaje de error</param>
        private static void addToLog(ScriptLocation script, string place)
        {
            string errFile = place + @"\errors.txt";

            TextWriter tw = new StreamWriter(errFile, true);
            tw.WriteLine("No se creo el archivo " + script.dbName + @"\" + script.type + @"\" + script.fileName);
            tw.Close();

        }
    }
}
