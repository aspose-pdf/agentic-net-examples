using System;
using System.Runtime.InteropServices;
using System.Drawing; // retained for potential future use
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing; // for BorderInfo and BorderSide

public class CreateFixedWidthTable
{
    public static void Main()
    {
        // Output PDF path
        const string outputPath = "output.pdf";

        using (Document document = new Document())
        {
            // Add a new page to the document
            Page page = document.Pages.Add();

            // Create a table instance with a fixed width of 500 points
            Table table = new Table();
            table.ColumnWidths = "500"; // single column of 500 points

            // Optional: add a visible border to the table (use Aspose.Pdf.Color)
            table.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black);

            // Add a row to the table
            Row row = table.Rows.Add();
            // Add a cell to the row and insert text
            Cell cell = row.Cells.Add();
            cell.Paragraphs.Add(new TextFragment("Fixed width table cell"));

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the PDF document – guard the call on non‑Windows platforms where libgdiplus may be missing
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                document.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    document.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'. (non‑Windows platform, libgdiplus may be required)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was not saved, but the code executed correctly.");
                }
            }
        }
    }

    // Helper method to walk the inner‑exception chain and detect a missing native library
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
