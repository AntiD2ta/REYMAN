using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace BizLogic.Reports
{
    public class ExportReport2
    {
        public byte[] Generate(dynamic report)
        {
            byte[] fileContents;
            int fila = 6;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Reporte 2");

                //crear el "encabezado" de la tabla
                worksheet.Cells[1, 1, 1, 6].Merge = true;
                worksheet.Cells[1, 1].Value = "Planes de Reparación y Mantenimiento Constructivo";

                worksheet.Cells[2, 1, 2, 6].Merge = true;
                worksheet.Cells[2, 1].Value = "Presupuesto resumen por inmuebles de la U.O.";

                worksheet.Cells[2, 8, 2, 9].Merge = true;
                worksheet.Cells[2, 8].Value = $"Año: {report.año}";

                worksheet.Cells[4, 1, 5, 2].Merge = true;
                worksheet.Cells[4, 1].Value = "Unidad Organizativa";

                worksheet.Cells[4, 3, 5, 4].Merge = true;
                worksheet.Cells[4, 3].Value = "Inmueble";

                worksheet.Cells[4, 5, 4, 7].Merge = true;
                worksheet.Cells[4, 5].Value = "Total";
                worksheet.Cells[5, 5].Value = "MT";
                worksheet.Cells[5, 6].Value = "CUC";
                worksheet.Cells[5, 7].Value = "CUP";

                worksheet.Cells[4, 8, 4, 10].Merge = true;
                worksheet.Cells[4, 8].Value = "Reparaciones";
                worksheet.Cells[5, 8].Value = "MT";
                worksheet.Cells[5, 9].Value = "CUC";
                worksheet.Cells[5, 10].Value = "CUP";

                worksheet.Cells[4, 11, 4, 13].Merge = true;
                worksheet.Cells[4, 11].Value = "Mantenimiento";
                worksheet.Cells[5, 11].Value = "MT";
                worksheet.Cells[5, 12].Value = "CUC";
                worksheet.Cells[5, 13].Value = "CUP";

                //llenando la tabla
                foreach (var unidad in report.unidades)
                {
                    worksheet.Cells[fila, 1, fila, 2].Merge = true;
                    worksheet.Cells[fila, 1, fila, 2].Value = unidad.nombre;

                    foreach (var inmueble in unidad.inmuebles)
                    {
                        worksheet.Cells[fila, 3, fila, 4].Merge = true;
                        worksheet.Cells[fila, 3, fila, 4].Value = inmueble.nombre;

                        worksheet.Cells[fila, 5].Value = inmueble.reparacionesCUC + inmueble.reparacionesCUP + inmueble.mantenimientoCUC + inmueble.mantenimiento.mantenimientoCUP;
                        worksheet.Cells[fila, 6].Value = inmueble.reparacionesCUC + inmueble.mantenimientoCUC;
                        worksheet.Cells[fila, 7].Value = inmueble.reparacionesCUP + inmueble.mantenimientoCUP;
                        worksheet.Cells[fila, 8].Value = inmueble.reparacionesCUC + inmueble.reparacionesCUP;
                        worksheet.Cells[fila, 9].Value = inmueble.reparacionesCUC;
                        worksheet.Cells[fila, 10].Value = inmueble.reparacionesCUP;
                        worksheet.Cells[fila, 11].Value = inmueble.mantenimientoCUC + inmueble.mantenimientoCUP;
                        worksheet.Cells[fila, 12].Value = inmueble.mantenimientoCUC;
                        worksheet.Cells[fila, 13].Value = inmueble.mantenimientoCUP;

                        ++fila;
                    }

                    worksheet.Cells[fila, 3, fila, 4].Merge = true;
                    worksheet.Cells[fila, 3, fila, 4].Value = "Total de la Unidad Organizativa"; 

                    worksheet.Cells[fila, 5].Value = unidad.reparacionesCUC + unidad.reparacionesCUP + unidad.mantenimientoCUC + unidad.mantenimiento.mantenimientoCUP;
                    worksheet.Cells[fila, 6].Value = unidad.reparacionesCUC + unidad.mantenimientoCUC;
                    worksheet.Cells[fila, 7].Value = unidad.reparacionesCUP + unidad.mantenimientoCUP;
                    worksheet.Cells[fila, 8].Value = unidad.reparacionesCUC + unidad.reparacionesCUP;
                    worksheet.Cells[fila, 9].Value = unidad.reparacionesCUC;
                    worksheet.Cells[fila, 10].Value = unidad.reparacionesCUP;
                    worksheet.Cells[fila, 11].Value = unidad.mantenimientoCUC + unidad.mantenimientoCUP;
                    worksheet.Cells[fila, 12].Value = unidad.mantenimientoCUC;
                    worksheet.Cells[fila, 13].Value = unidad.mantenimientoCUP;

                    ++fila;
                }

                worksheet.Cells[1, 1, fila, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[1, 1, fila, 13].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                fileContents = package.GetAsByteArray();
            }

            return fileContents;
        }
    }
}
