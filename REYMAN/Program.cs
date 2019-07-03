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
//TODO: [KARL] arreglar los botones edit a veces son botones otras links, no se quieren links. tambien a veces estan con mayuscula y otras no
//TODO: [ALL] en la vista de Objetos de Obra si toco annadir se explota
//TODO: Poner el layout de side-nav a todas las paginas q no lo tenga. Esta fula sin eso.
//TODO: Editar AC, y en ver materiales en las AC poder annadir materiales.
//TODO: Los materiales salgan organizados por orden alfabetico en EditMateriales
//TODO: No salen los errores en el view del log in
//TODO: Cuando insertas un inmueble, si eres admin, tiene q dar a escoger la unidad organizativa del inmueble
//TODO: (karle) Cambiar todos los editar que no salgan como boton para que salgan como boton
//TODO: (karle) En las tablas de mostrar Especialidades y Unidades de Medida, cuando eres admin sale una columna de botones:'Eliminar' pero cuando no eres admin sale el espacio correspondiente a esa columna vacio, quitar ese espacio en este caso.
//TODO: (karle) Ajustarle el tamaño del input, y ponerle el layout bien.
//TODO: los mensajes de salen cuando se valida algo, i.e cuando no se rellena un campo required, salen en ingles
//TODO: Hacer la pagina de contacto, con nuestros nombres y emails, ponerle el layout que te dice hola <nombre de usuario>

//Back
//TODO: Chequear q pasa cuando se borra un objeto de obra que q esta siendo usado en una AC
//TODO: Chequear q pasa cuando se borra un inmueble que q esta siendo usado en una AC
