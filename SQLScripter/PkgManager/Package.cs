using System;

namespace SQLScripter.PkgManager
{
    /// <summary>
    /// Clase abstracta que provee las bases para crear archivos y directorios
    /// </summary>
    public abstract class Package
    {
        public String pkgName;
        public String path;

        /// <summary>
        /// Constructor general para los paquetes, inicia el nombre y la ubicación
        /// </summary>
        /// <param name="name">Nombre de carpeta o archivo</param>
        /// <param name="location">Ubicación de carpeta o archivo</param>
        public Package(String name, String location)
        {
            pkgName = name;
            path = location + "\\" + pkgName;

        }

        /// <summary>
        /// Constructor general para los paquetes, indica solo el nombre
        /// </summary>
        /// <param name="name">Nombre de carpeta</param>
        public Package(String name)
        {
            pkgName = name;
            path = null;
        }

        /// <summary>
        /// Cambia la ubicacion en donde se generará el archivo/carpeta
        /// </summary>
        /// <param name="location">Nueva ubicación</param>
        public void setPath(String location)
        {
            path = location + "\\" + pkgName;
        }

        /// <summary>
        /// Crea el archivo/carpeta en la ubicación dada
        /// </summary>
        public abstract void createPackage();


    }
}
