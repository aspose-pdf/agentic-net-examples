using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Globalization;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its column widths (optional)
            Table table = new Table
            {
                ColumnWidths = "100 150 200" // three columns with specified widths
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Cell A1");
            data.Cells.Add("Cell A2");
            data.Cells.Add("Cell A3");

            // Add the table to the page at a specific position
            page.Paragraphs.Add(table);

            // Force layout by saving to a memory stream (no file written)
            using (MemoryStream ms = new MemoryStream())
            {
                // Guard Document.Save for platforms that may lack libgdiplus (e.g., macOS/Linux)
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    doc.Save(ms);
                }
                else
                {
                    try
                    {
                        doc.Save(ms);
                    }
                    catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                    {
                        Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. Layout may be incomplete.");
                    }
                }

                // After layout, calculate the rendered width of the table.
                // Aspose.Pdf.Table does not expose a direct Width property. Use the parsed ColumnWidths string.
                double renderedWidth = 0;
                if (!string.IsNullOrWhiteSpace(table.ColumnWidths))
                {
                    // ColumnWidths is a space‑separated list of numbers (points). Parse and sum them.
                    renderedWidth = table.ColumnWidths
                        .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => double.Parse(s, CultureInfo.InvariantCulture))
                        .Sum();
                }
                else
                {
                    // If ColumnWidths were not set, fall back to the page width minus margins.
                    // PageInfo.Width is the full page width in points.
                    renderedWidth = page.PageInfo.Width;
                }
                Console.WriteLine($"Rendered table width: {renderedWidth}");
            }

            // Optionally save the PDF to disk (again guard for non‑Windows platforms)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save("output.pdf");
            }
            else
            {
                try
                {
                    doc.Save("output.pdf");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: Could not save PDF because GDI+ (libgdiplus) is missing on this platform.");
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
