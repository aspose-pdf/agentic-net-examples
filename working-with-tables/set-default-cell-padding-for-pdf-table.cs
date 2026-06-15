using System;
using System.IO;
using System.Runtime.InteropServices; // for OS check
using Aspose.Pdf;
using Aspose.Pdf.Text; // for text handling if needed

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Suppress known NuGet vulnerability warnings (NU1903) that may be treated as errors
        #pragma warning disable NU1903
        // Load existing PDF or create a new one if the file does not exist
        using (Document doc = File.Exists(inputPath) ? new Document(inputPath) : new Document())
        {
            // Ensure there is at least one page to host the table
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Create a table with two columns
            Table table = new Table();

            // Define default cell padding (5 points on each side) using MarginInfo
            MarginInfo defaultPadding = new MarginInfo
            {
                Top    = 5,
                Bottom = 5,
                Left   = 5,
                Right  = 5
            };

            // Apply the padding to the entire table
            table.DefaultCellPadding = defaultPadding;

            // Add header row
            Row headerRow = table.Rows.Add();
            headerRow.Cells.Add("Header 1");
            headerRow.Cells.Add("Header 2");

            // Add a data row
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Data 1");
            dataRow.Cells.Add("Data 2");

            // Insert the table into the first page
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified document – guard against missing libgdiplus on non‑Windows platforms
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without GDI+ dependent features.");
                }
            }
        }
        #pragma warning restore NU1903

        Console.WriteLine($"Table with default cell padding saved to '{outputPath}'.");
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
