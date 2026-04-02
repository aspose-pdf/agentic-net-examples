using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document document = new Document())
        {
            // Add a page to the document
            Page page = document.Pages.Add();

            // Create a table and define column widths
            Table table = new Table();
            table.ColumnWidths = "100 100 100";
            // Set the number of header rows to repeat on each page
            table.RepeatingRowsCount = 1;

            // Add header row
            Row headerRow = table.Rows.Add();
            headerRow.Cells.Add("Header 1");
            headerRow.Cells.Add("Header 2");
            headerRow.Cells.Add("Header 3");
            // Style header cells
            foreach (Cell cell in headerRow.Cells)
            {
                // Use the TextState constructor that accepts font name and size.
                // Set the color via the ForegroundColor property.
                cell.DefaultCellTextState = new TextState("Arial", 12);
                cell.DefaultCellTextState.ForegroundColor = Aspose.Pdf.Color.Black;
                cell.BackgroundColor = Aspose.Pdf.Color.LightGray;
            }

            // Add many data rows to force the table to span multiple pages
            for (int i = 0; i < 100; i++)
            {
                Row dataRow = table.Rows.Add();
                dataRow.Cells.Add($"Row {i} Col 1");
                dataRow.Cells.Add($"Row {i} Col 2");
                dataRow.Cells.Add($"Row {i} Col 3");
            }

            // Position the table on the page
            table.Left = 50;
            table.Top = 750;

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            const string outputPath = "output.pdf";
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
                    Console.WriteLine($"PDF saved to '{outputPath}'. (non‑Windows platform, libgdiplus present)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF could not be saved using Aspose.Pdf's default renderer.");
                    // Optionally, you could fall back to a different rendering approach or inform the user.
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException (e.g., libgdiplus)
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
