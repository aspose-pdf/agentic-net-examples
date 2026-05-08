using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // existing PDF (can be empty)
        const string outputPdf = "output_with_svg.pdf";
        const string svgPath   = "image.svg";          // path to the SVG file to embed

        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"SVG file not found: {svgPath}");
            return;
        }

        // Load (or create) the PDF document
        using (Document doc = File.Exists(inputPdf) ? new Document(inputPdf) : new Document())
        {
            // Ensure there is at least one page
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Create a table with a single column
            Table table = new Table
            {
                ColumnWidths = "500"   // width of the single column (points)
            };

            // Add a row
            Row row = table.Rows.Add();

            // Add a cell to the row
            Cell cell = row.Cells.Add();

            // Create an Image object that points to the SVG file
            Image svgImage = new Image
            {
                File = svgPath
            };

            // Add the image to the cell's paragraph collection
            cell.Paragraphs.Add(svgImage);

            // Add the table to the first page
            doc.Pages[1].Paragraphs.Add(table);

            // Save the document – guard against missing GDI+ on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPdf);
            }
            else
            {
                try
                {
                    doc.Save(outputPdf);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. PDF saved without rendering the SVG.");
                }
            }
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }

    // Helper to detect a nested DllNotFoundException (libgdiplus) inside TypeInitializationException
    private static bool ContainsDllNotFound(Exception ex)
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