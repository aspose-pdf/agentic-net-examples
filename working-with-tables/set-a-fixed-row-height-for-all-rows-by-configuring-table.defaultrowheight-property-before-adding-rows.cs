using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;   // Facades namespace is included as requested

class Program
{
    static void Main()
    {
        const string outputPath = "table_fixed_row_height.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a table and define three column widths
            Table table = new Table
            {
                ColumnWidths = "100 150 200"   // space‑separated widths in points
            };

            // Define the fixed height that will be applied to every row
            double fixedRowHeight = 30.0;   // points

            // ----- Header row -----
            Row headerRow = table.Rows.Add();
            headerRow.FixedRowHeight = fixedRowHeight;
            // Optional: style the header cells
            headerRow.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                ForegroundColor = Color.Black
            };
            headerRow.Cells.Add("Header 1");
            headerRow.Cells.Add("Header 2");
            headerRow.Cells.Add("Header 3");

            // ----- Data rows -----
            for (int i = 0; i < 5; i++)
            {
                Row dataRow = table.Rows.Add();
                dataRow.FixedRowHeight = fixedRowHeight;   // enforce the same height
                dataRow.Cells.Add($"Row {i + 1} – Col 1");
                dataRow.Cells.Add($"Row {i + 1} – Col 2");
                dataRow.Cells.Add($"Row {i + 1} – Col 3");
            }

            // Add the fully built table to the page
            page.Paragraphs.Add(table);

            // ----- Save the document -----
            // On non‑Windows platforms Aspose.Pdf may require libgdiplus for rendering.
            // Wrap the Save call in an OS check and handle the possible TypeInitializationException.
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
                    Console.WriteLine("libgdiplus is not available on this platform; PDF saving was skipped.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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