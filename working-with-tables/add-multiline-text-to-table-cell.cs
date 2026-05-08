using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "multiline_cell.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Create a table and add it to the page
            Table table = new Table
            {
                // Optional: set column widths (single column in this example)
                ColumnWidths = "200"
            };
            page.Paragraphs.Add(table);

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row
            Cell cell = row.Cells.Add();

            // First line of text
            TextFragment line1 = new TextFragment("First line of text");
            line1.TextState.FontSize = 12;
            line1.TextState.Font = FontRepository.FindFont("Helvetica");
            line1.TextState.ForegroundColor = Color.Black;

            // Line break fragment (empty text with a newline)
            TextFragment lineBreak = new TextFragment("\n");

            // Second line of text
            TextFragment line2 = new TextFragment("Second line of text");
            line2.TextState.FontSize = 12;
            line2.TextState.Font = FontRepository.FindFont("Helvetica");
            line2.TextState.ForegroundColor = Color.Black;

            // Third line of text
            TextFragment line3 = new TextFragment("Third line of text");
            line3.TextState.FontSize = 12;
            line3.TextState.Font = FontRepository.FindFont("Helvetica");
            line3.TextState.ForegroundColor = Color.Black;

            // Add the fragments to the cell, separating them with line‑break fragments
            cell.Paragraphs.Add(line1);
            cell.Paragraphs.Add(lineBreak);
            cell.Paragraphs.Add(line2);
            cell.Paragraphs.Add(lineBreak);
            cell.Paragraphs.Add(line3);

            // Save the document (guard against missing GDI+ on non‑Windows platforms)
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException
    static bool ContainsDllNotFound(Exception ex)
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