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

//MEMO: Cuando una accion constructiva sea creada, solo es requerido que se ponga su nombre y el nombre de los materiales.
//MEMO: Una Accion constructiva solo va a tener una mano de obra asociada.
//MEMO: Las monedas no se van a cambiar, solo se va a mostrar los precios en la moneda que fue insertado.
//MEMO: Vamos a dejar que se ponga un precio en las dos monedas, pero se para los reportes se debe especificar al menos un precio en alguna moneda.
//TODO: En los reportes solo salen los O.O o los Inmuebles que tengan AC's.

//Front
//TODO: side-nav-bar si lo abres todo sale un scroll al lado pero se pierden cosas al menos en mi pc
//TODO: Poner el layout de side-nav a todas las paginas q no lo tenga. Esta fula sin eso.
//TODO: [Raul] Hacer la pagina de contacto, con nuestros nombres y emails, ponerle el layout que te dice hola <nombre de usuario>
//TODO: [TENORIO] Arreglar EditAC, rellenar los campos con los datos actuales de esa AC. Arreglar la ortografia del header y comenzarlo todo en mayusculas.
//TODO: [Raul] Los materiales salgan organizados por orden alfabetico en EditMateriales
//TODO: [Karle] Cambiaste algo para el AddObjObra, el admin ahora tiene q tener una UO obligado seteada para poder acceder a la pagina, haz un chequeo q si es admin no pida la UO.
//TODO: [karle] (LLevas 3 pr sin hacer esto) En las tablas de mostrar Especialidades y Unidades de Medida, cuando eres admin sale una columna de botones:'Eliminar' pero cuando no eres admin sale el espacio correspondiente a esa columna vacio, quitar ese espacio en este caso.
//TODO: [karle] Implementar borrar AC y borrar plan(Ya estan puestos los metodos en el backend)
//TODO: [karle] Cuando se crea una AC, que los nombres de los materiales se autocompleten.

//TODO: Probar modificar AC