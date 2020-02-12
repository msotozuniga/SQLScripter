using SQLScripter.Scripter;
using SQLScripter.Structures;
using System;
using System.IO;
using SQLScripter.PkgManager;

namespace SQLScripter
{
    public static class StandardProcess
    {

        /// <summary>
        /// Función que sigue la lógica estándar para trabajar con el proyecto. Genera un zip con los scripts entregados
        /// </summary>
        /// <param name="pathToExcel">Ubicación del archivo a procesar</param>
        /// <param name="zipPath">Ubicación donde se depositará el comprimido</param>
        /// <param name="server">Nombre de servidor e instancia</param>
        /// <param name="user">Nombre de usuario</param>
        /// <param name="psw">Contraseña de usuario</param>
        public static void Process(String pathToExcel, String zipPath, String server, String user, String psw, bool altOption)
        {
            
            if (!Directory.Exists(zipPath))
            {
                Directory.CreateDirectory(zipPath);
            }

            String placeToCreateFiles = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\Data";

            Directory.CreateDirectory(placeToCreateFiles);

            SQLScripter.Parser.Parser p = new SQLScripter.Parser.Parser(pathToExcel);
            if (!p.docCheck())
            {
                return;
            }
            String pkgPath = zipPath + "//" + p.getDocName();

            FileLibrary f = p.createLibrary();

            try
            {
                Guionista g = new Guionista(server, user, psw);
                g.alter = altOption;
                g.generateScripts(f, placeToCreateFiles);
            }
            catch (Microsoft.SqlServer.Management.Common.ConnectionFailureException)
            {
                foreach (var subDir in new DirectoryInfo(placeToCreateFiles).GetDirectories())
                {
                    subDir.Delete(true);
                }
                File.Delete(placeToCreateFiles + @"\errors.txt");

            }

            PkgCreator.generatePkg("zip", pkgPath, placeToCreateFiles);

           
        }

       
    }
}
