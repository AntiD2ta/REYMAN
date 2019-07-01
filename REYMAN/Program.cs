﻿using System;
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

//TODO: side-nav-bar si lo abres todo sale un scroll al lado pero se pierden cosas al menos en mi pc
//TODO: [KARL] arreglar los botones edit a veces son botones otras links, no se quieren links. tambien a veces estan con mayuscula y otras no
//TODO: [ALL] en la vista de Objetos de Obra si toco annadir se explota
//TODO: Poner el layout de side-nav a todas las paginas q no lo tenga. Esta fula sin eso.