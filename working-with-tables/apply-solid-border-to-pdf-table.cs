using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text; // BorderSide enum is in Aspose.Pdf namespace, but keeping for clarity

class Program
{
    static void Main()
    {
        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a table
            Table table = new Table();

            // Configure a solid black border of width 2 points on all sides
            BorderInfo border = new BorderInfo(
                BorderSide.Left | BorderSide.Top | BorderSide.Right | BorderSide.Bottom,
                2f,                                   // border width
                Aspose.Pdf.Color.Black);             // border color

            table.Border = border;

            // (Optional) Add a simple row and cell so the table is visible
            Row row = table.Rows.Add();
            Cell cell = row.Cells.Add("Sample cell");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            string outputPath = "TableWithBorder.pdf";
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
                                      "The PDF could not be saved using Document.Save().");
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
