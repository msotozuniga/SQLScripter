using Spire.Xls;
using SQLScripter.Structures;
using System;
using System.IO;

namespace SQLScripter.Parser
{
    /// <summary>
    /// Clase dedicada a la extracción de datos de un archivo
    /// </summary>
    public class Parser
    {
        public string documentPath { get; private set; }
        public string type { get; private set; }

        /// <summary>
        /// Inicializa un objeto representativo del archivo excel a procesar
        /// </summary>
        /// <param name="pathLocation">Ubicación del archivo</param>
        public Parser(String pathLocation)
        {
            documentPath = pathLocation;
            type = Path.GetExtension(documentPath);

        }

        /// <summary>
        /// Revisa si el documento entregado es soportado por el programa
        /// </summary>
        /// <returns>Booleano que entrega true si el formato es soportado</returns>
        public bool docCheck()
        {
            bool valid = false;
            string[] supportedTypes = new string[] { ".xls", ".xlsx" };
            foreach (string s in supportedTypes)
            {
                if (type.Equals(s))
                {
                    valid = true;
                }
            }
            return valid;
        }

        /// <summary>
        /// Obtiene el nombre del documento sin extension
        /// </summary>
        /// <returns>Nombre del documento</returns>
        public string getDocName()
        {
            return Path.GetFileNameWithoutExtension(documentPath);
        }

        /// <summary>
        /// Extrae el nombre, la base de datos asociada y el tipo de todos los archivos referenciados en el documento
        /// </summary>
        /// <returns>Una estructura que contiene los datos del documento en strings</returns>
        public FileLibrary createLibrary()
        {
            FileLibrary lib;
            switch (type)
            {
                case ".xlsx":
                case ".xls":
                    lib = this.createExcelLibrary();
                    break;

                default:
                    throw new NotImplementedException();

            }
            return lib;
        }

        /// <summary>
        /// Extrae el nombre, la base de datos asociada y el tipo de todos los archivos referenciados en el excel
        /// </summary>
        /// <returns>Una estructura que contiene los datos del documento en strings</returns>
        public FileLibrary createExcelLibrary()
        {
            Workbook document = new Workbook();
            document.LoadFromFile(documentPath);
            Worksheet hoja = document.Worksheets[0];

            FileLibrary fl = new FileLibrary(null, null, null, null);

            foreach (CellRange l in hoja.Rows)
            {
                String db = l.Cells[0].DisplayedText;

                if (db.Length == 0) { break; }

                String type = l.Cells[1].DisplayedText;
                String file = l.Cells[2].DisplayedText;
                String nuevo = l.Cells[3].DisplayedText;
 
                FileLibrary rowLibrary = new FileLibrary(db, type, file, nuevo);
                fl.addLibrary(rowLibrary);
            }

            document.Dispose();

            return fl.getNextInLine();
        }
    }
}
