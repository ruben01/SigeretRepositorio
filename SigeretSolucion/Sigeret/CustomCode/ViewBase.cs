using System;
using System.Resources;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace SIGERET.CustomCode
{
    /// <summary>
    /// Clase base utilizada para encapsular genéricamente métodos y propiedades
    /// que serán utilizados en todos los Views de la apliación.
    /// </summary>
    /// <typeparam name="TModel">Clase modelo abstracta del View en cuestión en tiempo de ejecución</typeparam>
    public abstract class ViewBase<TModel> : System.Web.Mvc.WebViewPage<TModel> where TModel : class
    {
        
    }
}