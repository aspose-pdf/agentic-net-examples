using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "CellVerticalAlignment.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (page indexing is 1‑based)
            Page page = doc.Pages.Add();

            // Create a table with a single column
            Table table = new Table { ColumnWidths = "200" }; // width of the column in points

            // Add a row to the table
            Row row = table.Rows.Add();

            // Create a cell, add some text, and set vertical alignment to middle (center)
            Cell cell = new Cell();
            cell.Paragraphs.Add(new TextFragment("Centered Text"));
            cell.VerticalAlignment = VerticalAlignment.Center; // Middle alignment

            // Add the cell to the row
            row.Cells.Add(cell);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            SaveDocument(doc, outputPath);
        }
    }

    private static void SaveDocument(Document doc, string path)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            doc.Save(path);
            Console.WriteLine($"PDF saved to '{path}'.");
        }
        else
        {
            try
            {
                doc.Save(path);
                Console.WriteLine($"PDF saved to '{path}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF not saved.");
            }
        }
    }

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