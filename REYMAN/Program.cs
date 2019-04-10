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
//TODO: Es necesario hacer una nueva migracion ya que el campo Precio de AccionC_Material es ahora Decimal?.
//TODO: Ver como arreglar si se altera una UO que no se afecten todas las cosas ancladas a esa UO.
