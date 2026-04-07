using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF to work with
        using (Document sampleDoc = new Document())
        {
            // Add a blank page
            sampleDoc.Pages.Add();
            sampleDoc.Save("input.pdf");
        }

        // Load the existing PDF
        using (Document doc = new Document("input.pdf"))
        {
            // Get the first page (1‑based index)
            Page page = doc.Pages[1];

            // Construct a table
            Table table = new Table();
            // Set position on the page
            table.Left = 100f;
            table.Top = 200f;
            // Define column widths (optional)
            table.ColumnWidths = "100 100";

            // First row (header)
            Row headerRow = table.Rows.Add();
            Cell headerCell1 = new Cell();
            headerCell1.Paragraphs.Add(new TextFragment("Header 1"));
            headerRow.Cells.Add(headerCell1);
            Cell headerCell2 = new Cell();
            headerCell2.Paragraphs.Add(new TextFragment("Header 2"));
            headerRow.Cells.Add(headerCell2);

            // Second row (data)
            Row dataRow = table.Rows.Add();
            Cell dataCell1 = new Cell();
            dataCell1.Paragraphs.Add(new TextFragment("Value A"));
            dataRow.Cells.Add(dataCell1);
            Cell dataCell2 = new Cell();
            dataCell2.Paragraphs.Add(new TextFragment("Value B"));
            dataRow.Cells.Add(dataCell2);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the modified PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            const string outputPath = "output.pdf";
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
                    Console.WriteLine($"PDF saved to '{outputPath}'. (non‑Windows platform, GDI+ may be missing but save succeeded)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF could not be saved.");
                    // Optionally, you could implement an alternative saving strategy here.
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException (libgdiplus)
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
