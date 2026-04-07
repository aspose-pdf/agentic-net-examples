using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and define column widths
            Table table = new Table
            {
                ColumnWidths = "100 200 200",
                // Repeat the first row (header) on each new page
                RepeatingRowsCount = 1
            };

            // Add a header row (visual clarity)
            Row headerRow = table.Rows.Add();
            headerRow.Cells.Add("Header1");
            headerRow.Cells.Add("Header2");
            headerRow.Cells.Add("Header3");

            // Apply bold style to the header row
            TextState headerStyle = new TextState
            {
                FontSize = 12,
                FontStyle = FontStyles.Bold
            };
            headerRow.DefaultCellTextState = headerStyle;

            // Populate the table with many rows to force page breaks
            for (int i = 1; i <= 50; i++)
            {
                Row dataRow = table.Rows.Add();
                dataRow.Cells.Add($"Row {i}");
                dataRow.Cells.Add($"Data A {i}");
                dataRow.Cells.Add($"Data B {i}");
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF document – guard against missing GDI+ on non‑Windows platforms
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