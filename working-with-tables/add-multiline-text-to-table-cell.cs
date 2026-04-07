using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "MultilineCell.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with a single column
            Table table = new Table { ColumnWidths = "300" };

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row
            Cell cell = row.Cells.Add();

            // First line of text
            var line1 = new TextFragment("First line of text")
            {
                TextState = { FontSize = 12, Font = FontRepository.FindFont("Helvetica") }
            };
            cell.Paragraphs.Add(line1);

            // Insert a line‑break fragment
            var lineBreak = new TextFragment("\n");
            cell.Paragraphs.Add(lineBreak);

            // Second line of text
            var line2 = new TextFragment("Second line of text")
            {
                TextState = { FontSize = 12, Font = FontRepository.FindFont("Helvetica") }
            };
            cell.Paragraphs.Add(line2);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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

        Console.WriteLine("Program finished.");
    }

    // Helper to detect a nested DllNotFoundException caused by missing libgdiplus
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