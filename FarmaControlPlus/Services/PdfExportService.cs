using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using TuProyecto.Models;

namespace TuProyecto.Services
{
    public class PdfExportService
    {
        // Usar alias para evitar conflicto con System.Drawing.Color
        private static class PdfColors
        {
            public static string BlueDarken3 = "#1A237E";
            public static string GreyDarken1 = "#757575";
            public static string GreyMedium = "#9E9E9E";
            public static string LightSteelBlue = "#B0C4DE";
            public static string OrangeDarken2 = "#F57C00";
            public static string RedDarken1 = "#D32F2F";
            public static string YellowDarken2 = "#FBC02D";
            public static string RedAccent2 = "#FF5252";
            public static string GreenDarken1 = "#388E3C";
            public static string White = "#FFFFFF";
            public static string Black = "#000000";
            public static string GreyLighten4 = "#F5F5F5";
            public static string GreyLighten2 = "#EEEEEE";
        }

        public static void ExportarMedicamentos(List<MedicamentoPDF> medicamentos, string filePath)
        {
            try
            {
                QuestPDF.Settings.License = LicenseType.Community;

                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(50);

                        // Encabezado
                        page.Header()
                            .Column(col =>
                            {
                                col.Item()
                                    .AlignCenter()
                                    .Text("INVENTARIO DE MEDICAMENTOS")
                                    .SemiBold().FontSize(20).FontColor(PdfColors.BlueDarken3);

                                col.Item()
                                    .AlignCenter()
                                    .Text("FarmaControlPlus - Sistema de Gestión Farmacéutica")
                                    .FontSize(12).FontColor(PdfColors.GreyDarken1);

                                col.Item()
                                    .AlignCenter()
                                    .Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}")
                                    .FontSize(9).FontColor(PdfColors.GreyMedium);

                                col.Item().PaddingBottom(10).LineHorizontal(1);
                            });

                        // Contenido
                        page.Content()
                            .PaddingVertical(10)
                            .Column(column =>
                            {
                                // Tabla
                                column.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn(2.5f); // Nombre
                                        columns.RelativeColumn(1.2f); // Código
                                        columns.RelativeColumn(1.5f); // Categoría
                                        columns.RelativeColumn(1.0f); // Stock
                                        columns.RelativeColumn(1.2f); // Precio
                                        columns.RelativeColumn(1.3f); // Vencimiento
                                        columns.RelativeColumn(1.3f); // Estado
                                    });

                                    // Encabezados
                                    table.Header(header =>
                                    {
                                        header.Cell().Background(PdfColors.BlueDarken3).Padding(5)
                                            .Text("NOMBRE").FontColor(PdfColors.White).Bold().AlignCenter();
                                        header.Cell().Background(PdfColors.BlueDarken3).Padding(5)
                                            .Text("CÓDIGO").FontColor(PdfColors.White).Bold().AlignCenter();
                                        header.Cell().Background(PdfColors.BlueDarken3).Padding(5)
                                            .Text("CATEGORÍA").FontColor(PdfColors.White).Bold().AlignCenter();
                                        header.Cell().Background(PdfColors.BlueDarken3).Padding(5)
                                            .Text("STOCK").FontColor(PdfColors.White).Bold().AlignCenter();
                                        header.Cell().Background(PdfColors.BlueDarken3).Padding(5)
                                            .Text("PRECIO").FontColor(PdfColors.White).Bold().AlignCenter();
                                        header.Cell().Background(PdfColors.BlueDarken3).Padding(5)
                                            .Text("VENCIMIENTO").FontColor(PdfColors.White).Bold().AlignCenter();
                                        header.Cell().Background(PdfColors.BlueDarken3).Padding(5)
                                            .Text("ESTADO").FontColor(PdfColors.White).Bold().AlignCenter();
                                    });

                                    // Datos
                                    for (int i = 0; i < medicamentos.Count; i++)
                                    {
                                        var med = medicamentos[i];
                                        var rowColor = i % 2 == 0 ? PdfColors.GreyLighten4 : PdfColors.White;
                                        var estadoColor = ObtenerColorEstado(med.Estado);

                                        table.Cell().Background(rowColor).Padding(3).Border(1).BorderColor(PdfColors.GreyLighten2)
                                            .Text(med.Nombre).FontSize(9).AlignLeft();

                                        table.Cell().Background(rowColor).Padding(3).Border(1).BorderColor(PdfColors.GreyLighten2)
                                            .Text(med.Codigo).FontSize(9).AlignCenter();

                                        table.Cell().Background(rowColor).Padding(3).Border(1).BorderColor(PdfColors.GreyLighten2)
                                            .Text(med.Categoria).FontSize(9).AlignCenter();

                                        table.Cell().Background(rowColor).Padding(3).Border(1).BorderColor(PdfColors.GreyLighten2)
                                            .Text(med.Stock.ToString()).FontSize(9).AlignCenter();

                                        table.Cell().Background(rowColor).Padding(3).Border(1).BorderColor(PdfColors.GreyLighten2)
                                            .Text(med.Precio).FontSize(9).AlignRight();

                                        table.Cell().Background(rowColor).Padding(3).Border(1).BorderColor(PdfColors.GreyLighten2)
                                            .Text(med.Vencimiento).FontSize(9).AlignCenter();

                                        table.Cell().Background(estadoColor).Padding(3).Border(1).BorderColor(PdfColors.GreyLighten2)
                                            .Text(med.Estado).FontSize(9).Bold().FontColor(PdfColors.White).AlignCenter();
                                    }
                                });
                            });

                        // Pie de página
                        page.Footer()
                            .AlignCenter()
                            .Text(text =>
                            {
                                text.Span("Página ");
                                text.CurrentPageNumber();
                                text.Span(" de ");
                                text.TotalPages();
                                text.Span(" • FarmaControlPlus © 2024");
                            });
                    });
                });

                document.GeneratePdf(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al exportar a PDF: {ex.Message}", ex);
            }
        }

        public static void ExportarFacturaVenta(
    List<DetalleFacturaPDF> detalles,
    string total,
    string filePath)
        {
            try
            {
                QuestPDF.Settings.License = LicenseType.Community;

                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(50);

                        // ================= ENCABEZADO =================
                        page.Header()
                            .Column(col =>
                            {
                                col.Item()
                                    .AlignCenter()
                                    .Text("FACTURA DE VENTA")
                                    .SemiBold().FontSize(20).FontColor(PdfColors.BlueDarken3);

                                col.Item()
                                    .AlignCenter()
                                    .Text("FarmaControlPlus")
                                    .FontSize(12).FontColor(PdfColors.GreyDarken1);

                                col.Item()
                                    .AlignCenter()
                                    .Text($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm}")
                                    .FontSize(9).FontColor(PdfColors.GreyMedium);

                                col.Item().PaddingBottom(10).LineHorizontal(1);
                            });

                        // ================= CONTENIDO =================
                        page.Content()
                            .PaddingVertical(10)
                            .Column(column =>
                            {
                                column.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn(3); // Medicamento
                                        columns.RelativeColumn(1); // Cantidad
                                        columns.RelativeColumn(1.5f); // Precio
                                        columns.RelativeColumn(1.5f); // Subtotal
                                    });

                                    // Encabezados
                                    table.Header(header =>
                                    {
                                        header.Cell().Background(PdfColors.BlueDarken3).Padding(5)
                                            .Text("MEDICAMENTO").FontColor(PdfColors.White).Bold().AlignCenter();

                                        header.Cell().Background(PdfColors.BlueDarken3).Padding(5)
                                            .Text("CANT.").FontColor(PdfColors.White).Bold().AlignCenter();

                                        header.Cell().Background(PdfColors.BlueDarken3).Padding(5)
                                            .Text("PRECIO").FontColor(PdfColors.White).Bold().AlignCenter();

                                        header.Cell().Background(PdfColors.BlueDarken3).Padding(5)
                                            .Text("SUBTOTAL").FontColor(PdfColors.White).Bold().AlignCenter();
                                    });

                                    // Detalle
                                    for (int i = 0; i < detalles.Count; i++)
                                    {
                                        var item = detalles[i];
                                        var rowColor = i % 2 == 0 ? PdfColors.GreyLighten4 : PdfColors.White;

                                        table.Cell().Background(rowColor).Padding(4).Border(1)
                                            .Text(item.Medicamento).FontSize(9);

                                        table.Cell().Background(rowColor).Padding(4).Border(1)
                                            .Text(item.Cantidad.ToString()).FontSize(9).AlignCenter();

                                        table.Cell().Background(rowColor).Padding(4).Border(1)
                                            .Text(item.Precio).FontSize(9).AlignRight();

                                        table.Cell().Background(rowColor).Padding(4).Border(1)
                                            .Text(item.Subtotal).FontSize(9).AlignRight();
                                    }
                                });

                                // TOTAL
                                column.Item().PaddingTop(10)
                                    .AlignRight()
                                    .Text($"TOTAL: {total}")
                                    .Bold().FontSize(12).FontColor(PdfColors.Black);
                            });

                        // ================= PIE =================
                        page.Footer()
                            .AlignCenter()
                            .Text(text =>
                            {
                                text.Span("Página ");
                                text.CurrentPageNumber();
                                text.Span(" de ");
                                text.TotalPages();
                                text.Span(" • FarmaControlPlus © 2024");
                            });
                    });
                });

                document.GeneratePdf(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al generar factura PDF: {ex.Message}", ex);
            }
        }

        private static string ObtenerColorEstado(string estado)
        {
            switch (estado)
            {
                case "VENCIDO":
                    return PdfColors.OrangeDarken2;
                case "SIN STOCK":
                    return PdfColors.GreyMedium;
                case "A PUNTO DE VENCER":
                    return PdfColors.RedDarken1;
                case "POR VENCER":
                    return PdfColors.YellowDarken2;
                case "BAJO STOCK":
                    return PdfColors.RedAccent2;
                default:
                    return PdfColors.GreenDarken1; // NORMAL
            }
        }
    }
}