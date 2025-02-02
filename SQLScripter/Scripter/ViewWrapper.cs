﻿using Microsoft.SqlServer.Management.Smo;

namespace SQLScripter.Scripter
{
    /// <summary>
    /// Encapsulador para vistas
    /// </summary>
    public class ViewWrapper : Wrapper
    {
        /// <summary>
        /// Constructor de clase para objetos tipo "View", inicializa la urn nula
        /// </summary>
        /// <param name="obj">Objeto a envolver</param>
        public ViewWrapper(View obj) : base(obj)
        {
            urna = obj.Urn;

        }
    }
}
