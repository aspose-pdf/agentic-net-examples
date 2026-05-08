using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths (adjust as needed)
        const string outputPath = "AutoNumberedTable.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with two columns
            Table table = new Table
            {
                // Optional: set table position and width
                Left = 50,
                Top = 700,
                ColumnWidths = "50 200" // first column 50 units, second column 200 units
            };

            // Sample data for the second column
            string[] data = { "Apple", "Banana", "Cherry", "Date", "Elderberry" };

            // Add rows: first column will be filled later with numbers
            foreach (string item in data)
            {
                // Add a new row
                Row row = table.Rows.Add();

                // Add empty cell for the auto‑numbered column
                row.Cells.Add(new Cell());

                // Add cell with actual data
                Cell dataCell = new Cell();
                dataCell.Paragraphs.Add(new TextFragment(item));
                row.Cells.Add(dataCell);
            }

            // Insert sequential numbers into the first cell of each row
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Row row = table.Rows[i];
                // Ensure the first cell exists
                if (row.Cells.Count > 0)
                {
                    // Clear any existing content (if any) and add the number
                    row.Cells[0].Paragraphs.Clear();
                    row.Cells[0].Paragraphs.Add(new TextFragment((i + 1).ToString()));
                }
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException
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
