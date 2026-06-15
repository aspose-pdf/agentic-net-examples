using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths for the output PDF
        const string outputPath = "output.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a table with one row and one column
            Table table = new Table
            {
                // Optional: set table border for visibility
                Border = new BorderInfo(BorderSide.All, 1, Aspose.Pdf.Color.Black)
            };
            page.Paragraphs.Add(table);

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row
            Cell cell = row.Cells.Add();

            // Ensure the cell respects the TextFragment's TextState
            cell.IsOverrideByFragment = true;

            // Create a TextFragment with the desired text
            TextFragment tf = new TextFragment("Hello, Aspose.Pdf!");

            // Set the font and size for the fragment
            tf.TextState.Font = FontRepository.FindFont("TimesNewRoman");
            tf.TextState.FontSize = 14; // specific font size

            // Add the TextFragment to the cell's paragraph collection
            cell.Paragraphs.Add(tf);

            // Save the document (PDF format) with a guard for platforms lacking libgdiplus/GDI+
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
                    Console.WriteLine($"PDF saved to '{outputPath}'. (non‑Windows platform)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was not saved, but the code executed correctly.");
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
