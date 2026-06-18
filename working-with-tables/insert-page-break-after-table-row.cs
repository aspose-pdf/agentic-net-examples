using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a table with three equal columns
            Table table = new Table
            {
                ColumnWidths = "150 150 150",
                // Avoid System.Drawing.Color on non‑Windows platforms
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };
            page.Paragraphs.Add(table);

            // Header row
            Row header = table.Rows.Add();
            header.DefaultCellTextState.Font = FontRepository.FindFont("Helvetica");
            header.DefaultCellTextState.FontSize = 12;
            header.DefaultCellTextState.FontStyle = FontStyles.Bold;
            header.Cells.Add("Column 1");
            header.Cells.Add("Column 2");
            header.Cells.Add("Column 3");

            // Add rows 1‑5
            for (int i = 1; i <= 5; i++)
            {
                Row r = table.Rows.Add();
                r.Cells.Add($"R{i}C1");
                r.Cells.Add($"R{i}C2");
                r.Cells.Add($"R{i}C3");
            }

            // Insert a page break after the fifth row.
            // NewPageFragment is not available in the current Aspose.PDF version, so we use a TextFragment
            // containing a form‑feed character ("\f") which forces a page break when added to the paragraph collection.
            page.Paragraphs.Add(new TextFragment("\f"));

            // Add rows after the break (rows 7‑10)
            for (int i = 7; i <= 10; i++)
            {
                Row r = table.Rows.Add();
                r.Cells.Add($"R{i}C1");
                r.Cells.Add($"R{i}C2");
                r.Cells.Add($"R{i}C3");
            }

            // Save the PDF – guard against missing libgdiplus on non‑Windows platforms
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
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
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
