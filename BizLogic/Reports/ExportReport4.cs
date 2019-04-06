using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizLogic.Reports
{
    public class ExportReport4
    {
        public byte[] Generate(dynamic report)
        {
            byte[] fileContents;
            int fila = 6;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Reporte 4");

                //crear el "encabezado" de la tabla
                worksheet.Cells[1, 1, 1, 6].Merge = true;
                worksheet.Cells[1, 1].Value = "Plan de Reparaciones y Mantenimiento Constructivo ";

                worksheet.Cells[2, 1, 2, 6].Merge = true;
                worksheet.Cells[2, 1].Value = "Listado de materiales por Inmuebles";

                worksheet.Cells[2, 8, 2, 9].Merge = true;
                worksheet.Cells[2, 8].Value = $"Año: {report.año}";

                worksheet.Cells[4, 1, 5, 2].Merge = true;
                worksheet.Cells[4, 1].Value = "Unidad Organizativa";

                worksheet.Cells[4, 3, 5, 4].Merge = true;
                worksheet.Cells[4, 3].Value = "Inmueble";

                worksheet.Cells[4, 5, 5, 6].Merge = true;
                worksheet.Cells[4, 5].Value = "Objeto de obra";

                worksheet.Cells[4, 7, 5, 8].Merge = true;
                worksheet.Cells[4, 7].Value = "Material";

                worksheet.Cells[4, 9, 5, 9].Merge = true;
                worksheet.Cells[4, 9].Value = "u/m";

                worksheet.Cells[4, 10, 4, 12].Merge = true;
                worksheet.Cells[4, 10].Value = "Cantidades";
                worksheet.Cells[5, 10].Value = "Rep";
                worksheet.Cells[5, 11].Value = "Mnto";
                worksheet.Cells[5, 12].Value = "Total";

                //llenando la tabla
                foreach (var unidad in report.unidades)
                {
                    worksheet.Cells[fila, 1, fila, 2].Merge = true;
                    worksheet.Cells[fila, 1].Value = unidad.nombre;

                    foreach (var inmueble in unidad.inmuebles)
                    {
                        worksheet.Cells[fila, 3, fila, 4].Merge = true;
                        worksheet.Cells[fila, 3].Value = inmueble.nombre;

                        foreach (var obj in inmueble.objetos)
                        {
                            worksheet.Cells[fila, 5, fila, 6].Merge = true;
                            worksheet.Cells[fila, 5].Value = obj.nombre;

                            foreach (var material in obj.materiales)
                            {
                                worksheet.Cells[fila, 7, fila, 8].Merge = true;
                                worksheet.Cells[fila, 7].Value = material.nombre;

                                worksheet.Cells[fila, 9].Value = material.unidadMedida;
                                worksheet.Cells[fila, 10].Value = material.reparaciones;
                                worksheet.Cells[fila, 11].Value = material.mantenimiento;
                                worksheet.Cells[fila, 12].Value = material.reparaciones + material.mantenimiento;
                                ++fila;
                            }

                            if (((IEnumerable<dynamic>)obj.materiales).Count() == 0)
                                ++fila;
                        }
                    }
                }

                worksheet.Cells[1, 1, fila, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[1, 1, fila, 12].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                fileContents = package.GetAsByteArray();
            }

            return fileContents;
        }
    }
}
