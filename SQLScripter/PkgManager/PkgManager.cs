using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

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

    /// <summary>
    /// Clase destinada a la creación de carpetas
    /// </summary>
    class CompositePackage : Package
    {
        private List<Package> paquetes;

        /// <summary>
        /// Constructor para creación de carpetas, dando nombre y ubicación
        /// </summary>
        /// <param name="name">Nombre de carpeta</param>
        /// <param name="location">Ubicación de carpeta</param>
        public CompositePackage(String name, String location) : base(name, location)
        {
            this.paquetes = new List<Package>();
        }

        /// <summary>
        /// Constructor para creación de carpetas, iniciando solo el nombre
        /// </summary>
        /// <param name="name">Nombre de carpeta</param>
        public CompositePackage(String name) : base(name)
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


    /// <summary>
    /// Clase dedicaca a la cración de archivos SQL (scripts)
    /// </summary>
    class SQLPackage : Package
    {

        private StringCollection script;



        /// <summary>
        /// Constructor de archivo SQL con nombre y script
        /// </summary>
        /// <param name="name">Nombre de archivo</param>
        /// <param name="fileScript">Contenido del archivo</param>
        public SQLPackage(String name, StringCollection fileScript) : base(name)
        {
            this.script = fileScript;
        }

        /// <summary>
        /// Contructor del archivo con nombre, ubicación y contenido
        /// </summary>
        /// <param name="name">Nombre de archivo</param>
        /// <param name="location">Ubicacion del archivo</param>
        /// <param name="fileScript">Contenido del archivo</param>
        public SQLPackage(String name, String location, StringCollection fileScript) : base(name, location)
        {
            this.script = fileScript;
        }


        /// <summary>
        /// Crea la carpeta en la ubicación dada y sus carpetas o archivos asociados
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
