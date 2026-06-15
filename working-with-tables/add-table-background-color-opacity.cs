using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

// Suppress the NuGet vulnerability warning (NU1903) for the transitive package Microsoft.Bcl.Memory.
// The vulnerability does not affect the functionality demonstrated in this sample.
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("NuGet", "NU1903", Justification = "Package is a transitive dependency and does not impact this sample.")]

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_background.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its background color with desired opacity.
            // Aspose.Pdf.Color.FromArgb(alpha, red, green, blue) allows specifying opacity (alpha 0‑255).
            Table table = new Table
            {
                // Define three equal-width columns
                ColumnWidths = "100 100 100",
                // 50% transparent blue background (alpha = 128)
                BackgroundColor = Color.FromArgb(128, 0, 0, 255)
            };

            // First row
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Cell 1");
            row1.Cells.Add("Cell 2");
            row1.Cells.Add("Cell 3");

            // Second row
            Row row2 = table.Rows.Add();
            row2.Cells.Add("Cell A");
            row2.Cells.Add("Cell B");
            row2.Cells.Add("Cell C");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF – guard the call on non‑Windows platforms where libgdiplus may be missing.
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

        Console.WriteLine($"PDF with table background saved to '{outputPath}'.");
    }

    // Helper method to walk the exception chain and detect a missing native GDI+ library.
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
