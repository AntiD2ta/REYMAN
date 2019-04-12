//using OfficeOpenXml;
//using OfficeOpenXml.Style;

//namespace BizLogic.Reports
//{
//    public class ExportReport1
//    {
//        public byte[] Generate(dynamic report)
//        {
//            byte[] fileContents;
//            int fila = 6;
            
//            using (var package = new ExcelPackage())
//            {
//                var worksheet = package.Workbook.Worksheets.Add("Reporte 1");

//                //crear el "encabezado" de la tabla
//                worksheet.Cells[1, 1, 1, 5].Merge = true;
//                worksheet.Cells[1, 1].Value = $"Plan de {report.tipo}";

//                worksheet.Cells[2, 1, 2, 7].Merge = true;
//                worksheet.Cells[2, 1].Value = "Alcance de acciones de constructivas por Inmuebles/Objetos de Obra";

//                worksheet.Cells[2, 11,2,12].Merge = true;
//                worksheet.Cells[2, 12].Value = $"Año: {report.año}";

//                worksheet.Cells[4, 1, 5, 2].Merge = true;
//                worksheet.Cells[4, 1].Style.WrapText = true;
//                worksheet.Cells[4, 1].Value = "Unidad Organizativa";

//                worksheet.Cells[4, 3, 5, 4].Merge = true;
//                worksheet.Cells[4, 3].Value = "Inmueble";

//                worksheet.Cells[4, 5, 5, 6].Merge = true;
//                worksheet.Cells[4, 5].Value = "Objeto de Obra";

//                worksheet.Cells[4, 7, 5, 11].Merge = true;
//                worksheet.Cells[4, 7].Value = "Especialidad/Actividad";

//                worksheet.Cells[4, 12, 5, 12].Merge = true;
//                worksheet.Cells[4, 12].Value = "UM";

//                worksheet.Cells[4, 13, 5, 13].Merge = true;
//                worksheet.Cells[4, 13].Value = "Cant.";

//                worksheet.Cells[4, 14, 4, 16].Merge = true;
//                worksheet.Cells[4, 14].Value = "Importe Total";
//                worksheet.Cells[5, 14].Value = "MT";
//                worksheet.Cells[5, 15].Value = "CUC";
//                worksheet.Cells[5, 16].Value = "CUP";

//                worksheet.Cells[4, 17, 4, 19].Merge = true;
//                worksheet.Cells[4, 17].Value = "Importe Mano de Obra";
//                worksheet.Cells[5, 17].Value = "MT";
//                worksheet.Cells[5, 18].Value = "CUC";
//                worksheet.Cells[5, 19].Value = "CUP";

//                worksheet.Cells[4, 20, 4, 22].Merge = true;
//                worksheet.Cells[4, 20].Value = "Importe Materiales";
//                worksheet.Cells[5, 20].Value = "MT";
//                worksheet.Cells[5, 21].Value = "CUC";
//                worksheet.Cells[5, 22].Value = "CUP";

//                //llenando el contenido
//                foreach (var unidad in report.unidades)
//                {
//                    worksheet.Cells[fila, 1, fila, 2].Merge = true;
//                    worksheet.Cells[fila, 1].Value = unidad.nombre;

//                    foreach (var inmueble in unidad.inmuebles)
//                    {
//                        worksheet.Cells[fila, 3, fila, 4].Merge = true;
//                        worksheet.Cells[fila, 3].Value = inmueble.nombre;

//                        foreach (var obj in inmueble.objetos)
//                        {
//                            worksheet.Cells[fila, 5, fila, 6].Merge = true;
//                            worksheet.Cells[fila, 5].Value = obj.nombre;
                       
//                            foreach (var especialidad in obj.especialidades)
//                            {
//                                worksheet.Cells[fila, 7, fila, 11].Merge = true;
//                                worksheet.Cells[fila, 7].Value = especialidad.nombre;
//                                worksheet.Cells[fila, 7, fila, 22].Style.Font.Bold = true;

//                                worksheet.Cells[fila, 12].Value = "";
//                                worksheet.Cells[fila, 13].Value = "";
//                                worksheet.Cells[fila, 14].Value = especialidad.manoObraTotalCUC + especialidad.manoObraTotalCUP + especialidad.materialesTotalCUC + especialidad.materialesTotalCUP;
//                                worksheet.Cells[fila, 15].Value = especialidad.manoObraTotalCUC + especialidad.materialesTotalCUC;
//                                worksheet.Cells[fila, 16].Value = especialidad.manoObraTotalCUP + especialidad.materialesTotalCUP;
//                                worksheet.Cells[fila, 17].Value = especialidad.manoObraTotalCUC + especialidad.manoObraTotalCUP;
//                                worksheet.Cells[fila, 18].Value = especialidad.manoObraTotalCUC;
//                                worksheet.Cells[fila, 19].Value = especialidad.manoObraTotalCUP;
//                                worksheet.Cells[fila, 20].Value = especialidad.materialesTotalCUC + especialidad.materialesTotalCUP;
//                                worksheet.Cells[fila, 21].Value = especialidad.materialesTotalCUC;
//                                worksheet.Cells[fila, 22].Value = especialidad.materialesTotalCUP;
                                
//                                foreach (var accion in especialidad.acciones)
//                                {
//                                    ++fila;

//                                    worksheet.Cells[fila, 7, fila, 11].Merge = true;
//                                    worksheet.Cells[fila, 7].Value = accion.nombre;

//                                    worksheet.Cells[fila, 12].Value = accion.unidadMedida;
//                                    worksheet.Cells[fila, 13].Value = accion.cantidad;
//                                    worksheet.Cells[fila, 14].Value = accion.manoObraCUC + accion.manoObraCUP + accion.materialesCUC + accion.materialesCUP;
//                                    worksheet.Cells[fila, 15].Value = accion.manoObraCUC + accion.materialesCUC;
//                                    worksheet.Cells[fila, 16].Value = accion.manoObraCUP + accion.materialesCUP;
//                                    worksheet.Cells[fila, 17].Value = accion.manoObraCUC + accion.manoObraCUP;
//                                    worksheet.Cells[fila, 18].Value = accion.manoObraCUC;
//                                    worksheet.Cells[fila, 19].Value = accion.manoObraCUP;
//                                    worksheet.Cells[fila, 20].Value = accion.materialesCUC + accion.materialesCUP;
//                                    worksheet.Cells[fila, 21].Value = accion.materialesCUC;
//                                    worksheet.Cells[fila, 22].Value = accion.materialesCUP;
//                                 }
//                            }

//                            ++fila;

//                            worksheet.Cells[fila, 5, fila, 13].Merge = true;
//                            worksheet.Cells[fila, 7, fila, 22].Style.Border.Top.Style = ExcelBorderStyle.Thin;
//                            worksheet.Cells[fila, 7, fila, 22].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
//                            worksheet.Cells[fila, 5, fila, 22].Style.Font.Bold = true;
//                            worksheet.Cells[fila, 5].Value = "Total del objeto de obra";

//                            worksheet.Cells[fila, 14].Value = obj.manoObraTotalCUC + obj.manoObraTotalCUP + obj.materialesTotalCUC + obj.materialesTotalCUP;
//                            worksheet.Cells[fila, 15].Value = obj.manoObraTotalCUC + obj.materialesTotalCUC;
//                            worksheet.Cells[fila, 16].Value = obj.manoObraTotalCUP + obj.materialesTotalCUP;
//                            worksheet.Cells[fila, 17].Value = obj.manoObraTotalCUC + obj.manoObraTotalCUP;
//                            worksheet.Cells[fila, 18].Value = obj.manoObraTotalCUC;
//                            worksheet.Cells[fila, 19].Value = obj.manoObraTotalCUP;
//                            worksheet.Cells[fila, 20].Value = obj.materialesTotalCUC + obj.materialesTotalCUP;
//                            worksheet.Cells[fila, 21].Value = obj.materialesTotalCUC;
//                            worksheet.Cells[fila, 22].Value = obj.materialesTotalCUP;

//                            ++fila;
//                        }

//                        worksheet.Cells[fila, 3, fila, 13].Merge = true;
//                        worksheet.Cells[fila, 5, fila, 22].Style.Border.Top.Style = ExcelBorderStyle.Thin;
//                        worksheet.Cells[fila, 5, fila, 22].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
//                        worksheet.Cells[fila, 3, fila, 22].Style.Font.Bold = true;
//                        worksheet.Cells[fila, 3].Value = "Total del inmueble";

//                        worksheet.Cells[fila, 14].Value = inmueble.manoObraTotalCUC + inmueble.manoObraTotalCUP + inmueble.materialesTotalCUC + inmueble.materialesTotalCUP;
//                        worksheet.Cells[fila, 15].Value = inmueble.manoObraTotalCUC + inmueble.materialesTotalCUC;
//                        worksheet.Cells[fila, 16].Value = inmueble.manoObraTotalCUP + inmueble.materialesTotalCUP;
//                        worksheet.Cells[fila, 17].Value = inmueble.manoObraTotalCUC + inmueble.manoObraTotalCUP;
//                        worksheet.Cells[fila, 18].Value = inmueble.manoObraTotalCUC;
//                        worksheet.Cells[fila, 19].Value = inmueble.manoObraTotalCUP;
//                        worksheet.Cells[fila, 20].Value = inmueble.materialesTotalCUC + inmueble.materialesTotalCUP;
//                        worksheet.Cells[fila, 21].Value = inmueble.materialesTotalCUC;
//                        worksheet.Cells[fila, 22].Value = inmueble.materialesTotalCUP;

//                        ++fila;
//                    }

//                    worksheet.Cells[fila, 1, fila, 13].Merge = true;
//                    worksheet.Cells[fila, 1, fila, 22].Style.Border.Top.Style = ExcelBorderStyle.Thin;
//                    worksheet.Cells[fila, 1, fila, 22].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
//                    worksheet.Cells[fila, 1, fila, 22].Style.Font.Bold = true;
//                    worksheet.Cells[fila, 1].Value = "Total de la unidad organizativa";

//                    worksheet.Cells[fila, 14].Value = unidad.manoObraTotalCUC + unidad.manoObraTotalCUP + unidad.materialesTotalCUC + unidad.materialesTotalCUP;
//                    worksheet.Cells[fila, 15].Value = unidad.manoObraTotalCUC + unidad.materialesTotalCUC;
//                    worksheet.Cells[fila, 16].Value = unidad.manoObraTotalCUP + unidad.materialesTotalCUP;
//                    worksheet.Cells[fila, 17].Value = unidad.manoObraTotalCUC + unidad.manoObraTotalCUP;
//                    worksheet.Cells[fila, 18].Value = unidad.manoObraTotalCUC;
//                    worksheet.Cells[fila, 19].Value = unidad.manoObraTotalCUP;
//                    worksheet.Cells[fila, 20].Value = unidad.materialesTotalCUC + unidad.materialesTotalCUP;
//                    worksheet.Cells[fila, 21].Value = unidad.materialesTotalCUC;
//                    worksheet.Cells[fila, 22].Value = unidad.materialesTotalCUP;

//                    ++fila;
//                }

//                worksheet.Cells[1, 1, 5, 22].Style.Font.Bold = true;
//                worksheet.Cells[5, 1, 5, 22].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
//                worksheet.Cells[1, 1, fila, 22].Style.Border.Right.Style = ExcelBorderStyle.None;
//                worksheet.Cells[1, 1, fila, 22].Style.Border.Left.Style = ExcelBorderStyle.None;
//                worksheet.Cells[1, 1, fila, 22].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//                worksheet.Cells[1, 1, fila, 22].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
//                fileContents = package.GetAsByteArray();
//            }

//            return fileContents;
//        }
//    }
//}
