using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "RichTextTable.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a table and set its position on the page
            Table table = new Table
            {
                // Position the table (left, top) – coordinates are in points
                Left = 50,
                Top = 700,
                // Optional: set column widths (two columns, each 200 points wide)
                ColumnWidths = "200 200"
            };

            // ---------- First Row ----------
            // Add a new row to the table
            Aspose.Pdf.Row row1 = table.Rows.Add();

            // First cell with bold, blue text
            Aspose.Pdf.Cell cell11 = row1.Cells.Add();
            TextFragment tf11 = new TextFragment("Bold Blue");
            tf11.TextState.Font = FontRepository.FindFont("Helvetica-Bold");
            tf11.TextState.FontSize = 12;
            tf11.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            cell11.Paragraphs.Add(tf11);

            // Second cell with italic, red text
            Aspose.Pdf.Cell cell12 = row1.Cells.Add();
            TextFragment tf12 = new TextFragment("Italic Red");
            tf12.TextState.Font = FontRepository.FindFont("Helvetica-Oblique");
            tf12.TextState.FontSize = 12;
            tf12.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
            cell12.Paragraphs.Add(tf12);

            // ---------- Second Row ----------
            Aspose.Pdf.Row row2 = table.Rows.Add();

            // First cell with underlined, green text
            Aspose.Pdf.Cell cell21 = row2.Cells.Add();
            TextFragment tf21 = new TextFragment("Underlined Green");
            tf21.TextState.Font = FontRepository.FindFont("Helvetica");
            tf21.TextState.FontSize = 12;
            tf21.TextState.ForegroundColor = Aspose.Pdf.Color.Green;
            tf21.TextState.Underline = true;
            cell21.Paragraphs.Add(tf21);

            // Second cell with larger, orange text
            Aspose.Pdf.Cell cell22 = row2.Cells.Add();
            TextFragment tf22 = new TextFragment("Large Orange");
            tf22.TextState.Font = FontRepository.FindFont("Helvetica");
            tf22.TextState.FontSize = 16;
            tf22.TextState.ForegroundColor = Aspose.Pdf.Color.Orange;
            cell22.Paragraphs.Add(tf22);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF could not be saved using Aspose.Pdf's default renderer.");
                }
            }
        }
    }

    // Helper method to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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
