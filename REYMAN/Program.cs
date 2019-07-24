using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace REYMAN
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}

//TODO: En los reportes solo salen los O.O o los Inmuebles que tengan AC's.

//Front
//TODO: side-nav-bar si lo abres todo sale un scroll al lado pero se pierden cosas al menos en mi pc
//TODO: [Karle] Cambiaste algo para el AddObjObra, el admin ahora tiene q tener una UO obligado seteada para poder acceder a la pagina, haz un chequeo q si es admin no pida la UO.
//TODO: [karle] (LLevas 3 pr sin hacer esto) En las tablas de mostrar Especialidades y Unidades de Medida, cuando eres admin sale una columna de botones:'Eliminar' pero cuando no eres admin sale el espacio correspondiente a esa columna vacio, quitar ese espacio en este caso.
//TODO: [karle] Separame el AddAccionCons en metodos GET y POST, q eso hace q se parta la app si el modelo no es valido cuando tratas de añadir la AC, arregla lo q tengas q arreglar en los views.
//TODO: [RUL0] Poner un boton en el view Materiales_AccCons q te permitar dar atras y volver al PlanState correspondiente.

//TODO: [T!] Añadir validacion a AddAccionCons(Tengo q esperar por KL)

//TODO: [Dillo] En el reporte 1, no se calculna bien los importes
//TODO: [Dillo] En el reporte 5 si no eres admin, q no salga la columna TOTAL