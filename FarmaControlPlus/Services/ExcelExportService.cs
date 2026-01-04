using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using TuProyecto.Models;

namespace TuProyecto.Services
{
    public class ExcelExportService
    {
        public static void ExportarDataGridView(DataGridView dataGridView, string filePath, Func<DataGridViewRow, string> obtenerEstado)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Inventario");

                    // Título
                    worksheet.Cell("A1").Value = "INVENTARIO DE MEDICAMENTOS - FarmaControlPlus";
                    worksheet.Range("A1:G1").Merge();
                    worksheet.Cell("A1").Style.Font.FontSize = 16;
                    worksheet.Cell("A1").Style.Font.Bold = true;
                    worksheet.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell("A1").Style.Fill.BackgroundColor = XLColor.SteelBlue;
                    worksheet.Cell("A1").Style.Font.FontColor = XLColor.White;

                    // Subtítulo
                    worksheet.Cell("A2").Value = $"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}";
                    worksheet.Range("A2:G2").Merge();
                    worksheet.Cell("A2").Style.Font.Italic = true;
                    worksheet.Cell("A2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    // Encabezados
                    int headerRow = 3;
                    for (int i = 0; i < dataGridView.Columns.Count; i++)
                    {
                        worksheet.Cell(headerRow, i + 1).Value = dataGridView.Columns[i].HeaderText;
                        worksheet.Cell(headerRow, i + 1).Style.Font.Bold = true;
                        worksheet.Cell(headerRow, i + 1).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                        worksheet.Cell(headerRow, i + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    }

                    // Columna adicional para Estado
                    int estadoCol = dataGridView.Columns.Count + 1;
                    worksheet.Cell(headerRow, estadoCol).Value = "Estado";
                    worksheet.Cell(headerRow, estadoCol).Style.Font.Bold = true;
                    worksheet.Cell(headerRow, estadoCol).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                    worksheet.Cell(headerRow, estadoCol).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    // Función para obtener estado (deberás implementarla según tu lógica)

                    // Datos
                    int dataRow = headerRow + 1;
                    foreach (DataGridViewRow gridRow in dataGridView.Rows)
                    {
                        if (gridRow.IsNewRow) continue;

                        for (int col = 0; col < dataGridView.Columns.Count; col++)
                        {
                            var cell = worksheet.Cell(dataRow, col + 1);
                            cell.Value = gridRow.Cells[col].Value?.ToString();
                            cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                            if (dataGridView.Columns[col].HeaderText == "Stock")
                                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                            if (dataGridView.Columns[col].HeaderText == "Precio" &&
                                decimal.TryParse(gridRow.Cells[col].Value?.ToString(), out decimal precio))
                            {
                                cell.Value = precio;
                                cell.Style.NumberFormat.Format = "#,##0.00";
                            }
                        }

                        // Estado
                        string estado = obtenerEstado(gridRow);
                        var estadoCell = worksheet.Cell(dataRow, estadoCol);
                        estadoCell.Value = estado;
                        estadoCell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        estadoCell.Style.Font.Bold = true;

                        // Colores según estado
                        AplicarColorEstado(estadoCell, estado);

                        dataRow++;
                    }

                    // Autoajustar columnas
                    worksheet.Columns().AdjustToContents();

                    // Guardar
                    workbook.SaveAs(filePath);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al exportar a Excel: {ex.Message}", ex);
            }
        }

        private static void AplicarColorEstado(IXLCell cell, string estado)
        {
            switch (estado)
            {
                case "VENCIDO":
                    cell.Style.Fill.BackgroundColor = XLColor.Orange;
                    cell.Style.Font.FontColor = XLColor.White;
                    break;
                case "SIN STOCK":
                    cell.Style.Fill.BackgroundColor = XLColor.Gray;
                    cell.Style.Font.FontColor = XLColor.White;
                    break;
                case "A PUNTO DE VENCER":
                    cell.Style.Fill.BackgroundColor = XLColor.Red;
                    cell.Style.Font.FontColor = XLColor.White;
                    break;
                case "POR VENCER":
                    cell.Style.Fill.BackgroundColor = XLColor.Yellow;
                    cell.Style.Font.FontColor = XLColor.Black;
                    break;
                case "BAJO STOCK":
                    cell.Style.Fill.BackgroundColor = XLColor.LightCoral;
                    cell.Style.Font.FontColor = XLColor.Black;
                    break;
                default:
                    cell.Style.Fill.BackgroundColor = XLColor.LightGreen;
                    cell.Style.Font.FontColor = XLColor.Black;
                    break;
            }
        }
    }
}