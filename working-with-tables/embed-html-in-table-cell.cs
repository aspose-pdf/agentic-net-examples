using System;
using System.IO;
using System.Runtime.InteropServices; // for OS check
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Text;          // For HtmlFragment (inherits from FormattedFragment)

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "TableWithHtmlCell.pdf";

        // Ensure the output directory exists
        string? outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a table with a single column (width can be adjusted as needed)
            Table table = new Table
            {
                ColumnWidths = "200"   // 200 points width for the single column
            };

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row
            Cell cell = row.Cells.Add();

            // Create an HtmlFragment containing the desired HTML markup
            HtmlFragment htmlFragment = new HtmlFragment("<b>Hello <i>World</i> from <u>HTML</u></b>");

            // Optional: customize HTML loading options (e.g., base path for external resources)
            // htmlFragment.HtmlLoadOptions = new HtmlLoadOptions { BasePath = "path/to/resources" };

            // Add the HtmlFragment to the cell's paragraph collection
            cell.Paragraphs.Add(htmlFragment);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document – guard against missing libgdiplus on non‑Windows platforms
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
                    Console.WriteLine($"PDF saved to '{outputPath}'. (Saved on non‑Windows platform – ensure libgdiplus is installed.)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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
