using ICSharpCode.SharpZipLib.Zip;
using SQLScripter.Structures;
using System;
using System.Collections.Generic;
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
            Folder root = new Folder("Base de datos", place);
            Folder db = new Folder(script.dbName);
            Folder type = new Folder(script.type);
            SQLFile file = new SQLFile(script.fileName, script.script);

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

        public static void createSQLCompiler(List<Node> list, string place)
        {
            string batFile = place + @"\Compilacion_SQL.bat";

            TextWriter tw = new StreamWriter(batFile, true);
            tw.WriteLine("cls");
            tw.WriteLine("echo instalador SQL");
            tw.WriteLine("echo.");
            tw.WriteLine("set/p Servidor= Ingrese el servidor:");
            tw.WriteLine("set/p usuario= usuario :");
            tw.WriteLine("set/p clave= password :");
            tw.WriteLine("\r\n");
            foreach(Node n in list)
            {
                var insertion = n.dbName + @"\" + n.type + @"\" + n.fileName;
                tw.WriteLine("osql - S % Servidor % -U % usuario % -P % clave % -n - i\"Base de datos\\"+insertion+ "\" >> \"ResultadoProcedimientos.Txt\"");
            }

            tw.Close();

        }
    }
}
