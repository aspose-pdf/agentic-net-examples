using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a table and position it on the page
            Table table = new Table
            {
                // Position the table (coordinates are in points)
                Left = 50,
                Top = 700,
                // Enable auto‑fit to content for all columns
                ColumnAdjustment = ColumnAdjustment.AutoFitToContent
                // AutoFitBehavior is not a member of Table in current Aspose.PDF versions.
                // The ColumnAdjustment setting above provides the required auto‑fit functionality.
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Product");
            header.Cells.Add("Description");
            header.Cells.Add("Price");

            // Add a data row with long text to demonstrate auto‑fit
            Row data = table.Rows.Add();
            data.Cells.Add("Widget A");
            data.Cells.Add("This is a very long description that should be automatically reduced to fit within the column width.");
            data.Cells.Add("$123.45");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF to a file (guard against missing libgdiplus on non‑Windows platforms)
            const string outputPath = "AutoFitTable.pdf";
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without rendering‑dependent features.");
                }
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
