using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace TableExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string outputPath = "table.pdf";

            using (Aspose.Pdf.Document document = new Aspose.Pdf.Document())
            {
                Aspose.Pdf.Page page = document.Pages.Add();

                Aspose.Pdf.Table table = new Aspose.Pdf.Table();
                table.ColumnWidths = "100 150 200"; // three columns
                table.Border = new BorderInfo(BorderSide.All, 1f);
                table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f);
                table.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);

                // Header row
                Aspose.Pdf.Row headerRow = table.Rows.Add();
                AddCell(headerRow, "ID", true);
                AddCell(headerRow, "Name", true);
                AddCell(headerRow, "Country", true);

                // Data rows
                for (int i = 1; i <= 5; i++)
                {
                    Aspose.Pdf.Row dataRow = table.Rows.Add();
                    AddCell(dataRow, i.ToString(), false);
                    AddCell(dataRow, "Item " + i, false);
                    AddCell(dataRow, "Country " + i, false);
                }

                page.Paragraphs.Add(table);

                // Save with OS guard for libgdiplus on non‑Windows platforms
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    document.Save(outputPath);
                }
                else
                {
                    try
                    {
                        document.Save(outputPath);
                    }
                    catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                    {
                        Console.WriteLine("GDI+ (libgdiplus) is not available – PDF cannot be saved on this platform.");
                    }
                }
            }
        }

        static void AddCell(Aspose.Pdf.Row row, string text, bool isHeader)
        {
            Aspose.Pdf.Cell cell = new Aspose.Pdf.Cell();
            Aspose.Pdf.Text.TextFragment fragment = new Aspose.Pdf.Text.TextFragment(text);

            if (isHeader)
            {
                // Modify the existing TextState instance instead of assigning a new one
                fragment.TextState.FontSize = 12;
                fragment.TextState.FontStyle = FontStyles.Bold;
                fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                cell.BackgroundColor = Aspose.Pdf.Color.LightGray;
            }
            else
            {
                // Modify the existing TextState instance instead of assigning a new one
                fragment.TextState.FontSize = 10;
                fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            }

            cell.Paragraphs.Add(fragment);
            row.Cells.Add(cell);
        }

        static bool ContainsDllNotFound(Exception ex)
        {
            Exception current = ex;
            while (current != null)
            {
                if (current is DllNotFoundException)
                    return true;
                current = current.InnerException;
            }
            return false;
        }
    }
}
