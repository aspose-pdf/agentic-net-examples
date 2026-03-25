using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "styled_table.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            Table table = new Table();
            table.ColumnWidths = "100 100"; // two equal columns

            // First row (header)
            Row headerRow = table.Rows.Add();
            Cell headerCell1 = headerRow.Cells.Add();
            StyleCell(headerCell1, "Header 1", Aspose.Pdf.Color.LightBlue);
            Cell headerCell2 = headerRow.Cells.Add();
            StyleCell(headerCell2, "Header 2", Aspose.Pdf.Color.LightBlue);

            // Second row (data)
            Row dataRow = table.Rows.Add();
            Cell dataCell1 = dataRow.Cells.Add();
            StyleCell(dataCell1, "Data A", Aspose.Pdf.Color.LightGray);
            Cell dataCell2 = dataRow.Cells.Add();
            StyleCell(dataCell2, "Data B", Aspose.Pdf.Color.LightGray);

            page.Paragraphs.Add(table);

            // Guard Document.Save against missing GDI+ (libgdiplus) on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }

        Console.WriteLine($"PDF processing completed. Output path: {outputPath}");
    }

    static void StyleCell(Cell cell, string text, Aspose.Pdf.Color background)
    {
        // Background color
        cell.BackgroundColor = background;

        // Border thickness, side and color using BorderInfo constructor (no Color property to set later)
        cell.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black);

        // Padding (margin) inside the cell using MarginInfo
        cell.Margin = new MarginInfo(5, 5, 5, 5);

        // Add text content
        TextFragment tf = new TextFragment(text);
        tf.TextState.Font = FontRepository.FindFont("Helvetica");
        tf.TextState.FontSize = 12;
        cell.Paragraphs.Add(tf);
    }

    // Helper to detect a missing native GDI+ library deep in the exception chain
    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
