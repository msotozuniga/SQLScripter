using System;
using System.Collections.Generic;
using System.IO;

namespace SQLScripter.PkgManager
{
    /// <summary>
    /// Clase destinada a la creación de carpetas
    /// </summary>
    class Folder : Package
    {
        private List<Package> paquetes;

        /// <summary>
        /// Constructor para creación de carpetas, dando nombre y ubicación
        /// </summary>
        /// <param name="name">Nombre de carpeta</param>
        /// <param name="location">Ubicación de carpeta</param>
        public Folder(String name, String location) : base(name, location)
        {
            this.paquetes = new List<Package>();
        }

        /// <summary>
        /// Constructor para creación de carpetas, iniciando solo el nombre
        /// </summary>
        /// <param name="name">Nombre de carpeta</param>
        public Folder(String name) : base(name)
        {
            this.paquetes = new List<Package>();
        }

        /// <summary>
        /// Añade un archivo o carpeta a la actual
        /// </summary>
        /// <param name="pkg">Archivo/carpeta a añadir</param>
        public void addPackage(Package pkg)
        {
            paquetes.Add(pkg);
            pkg.setPath(path);
        }


        /// <summary>
        /// Crea la carpeta en la ubicación dada y sus carpetas o archivos asociados
        /// </summary>
        public override void createPackage()
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            foreach (Package p in paquetes)
            {
                p.createPackage();
            }
        }
    }
}
