using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_fixed_row_height.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and configure its layout
            Table table = new Table();

            // Define column widths (space‑separated string, widths in points)
            table.ColumnWidths = "100 150 100";

            // Add three rows with sample data
            for (int i = 0; i < 3; i++)
            {
                // Create a new row; set a fixed height for this row
                Row row = table.Rows.Add();
                row.FixedRowHeight = 30; // height in points

                // Add three cells to the row
                for (int j = 0; j < 3; j++)
                {
                    // Create a text fragment for the cell content
                    TextFragment tf = new TextFragment($"R{i + 1}C{j + 1}");
                    tf.TextState.FontSize = 12;
                    tf.TextState.Font = FontRepository.FindFont("Helvetica");
                    tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                    // Add the text fragment to the cell
                    row.Cells.Add(tf);
                }
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the document, guarding against missing GDI+ on non‑Windows platforms
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
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
