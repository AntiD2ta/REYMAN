using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Reports
{
    public class ExportReport5
    {
        public byte[] Generate(dynamic report)
        {
            byte[] fileContents;
            int fila = 10;
            int col = 9;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Reporte 5");

                //crear el "encabezado" de la tabla
                worksheet.Cells[1, 1, 1, 6].Merge = true;
                worksheet.Cells[1, 1].Value = "Plan de Reparaciones y Mantenimiento Constructivo";

                worksheet.Cells[2, 1, 2, 6].Merge = true;
                worksheet.Cells[2, 1].Value = "Listado de materiales por U.O.";

                worksheet.Cells[2, 8, 2, 9].Merge = true;
                worksheet.Cells[2, 8].Value = $"Año: {report.Result.año}";

                worksheet.Cells[4, 1, 9, 6].Merge = true;
                worksheet.Cells[4, 1].Value = "Material";

                worksheet.Cells[4, 7, 9, 7].Merge = true;
                worksheet.Cells[4, 7].Value = "u/m";

                worksheet.Cells[5, 8, 9, 8].Merge = true;
                worksheet.Cells[5, 8].Value = "Total";

                //lenando la tabla
                foreach (var material in report.Result.materiales)
                {
                    worksheet.Cells[fila, 1, fila, 6].Merge = true;
                    worksheet.Cells[fila, 1].Value = material.nombre;
                    worksheet.Cells[fila, 7].Value = material.unidadMedida;

                    ++fila;
                }

                fila = 10;
                foreach (var total in report.Result.totales)
                {
                    worksheet.Cells[fila, 8].Value = total;
                    ++fila;
                }

                foreach (var unidad in report.Result.unidades)
                {
                    fila = 10;
                    worksheet.Cells[5, col, 9, col].Merge = true;
                    worksheet.Cells[5, col].Value = unidad.nombre;

                    foreach (var mat in unidad.materiales)
                    {
                        worksheet.Cells[fila, col].Value = mat;
                        ++fila;
                    }

                    ++col;
                }

                worksheet.Cells[4, 8, 4, col - 1].Merge = true;
                worksheet.Cells[4, 8].Value = "Cantidad";

                worksheet.Cells[1, 1, fila, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[1, 1, fila, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                fileContents = package.GetAsByteArray();
            }

            return fileContents;
        }
    }
}
