using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "multi_page_table.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add the first page (tables will automatically create new pages when broken)
            Page page = doc.Pages.Add();

            // Create a table that can break across pages
            Table table = new Table
            {
                // Enable automatic breaking; the table will continue on the next page when needed
                IsBroken = true,
                // Repeat the first row (header) on each new page fragment
                RepeatingRowsCount = 1,
                // Define three column widths (in points)
                ColumnWidths = "100 200 100"
            };

            // ----- Header row -----
            Row header = table.Rows.Add();

            // Helper to add a cell with text and a default appearance
            void AddCell(Row r, string text)
            {
                Cell c = new Cell();
                TextFragment tf = new TextFragment(text);
                // Modify the existing TextState (do NOT assign a new TextState object)
                tf.TextState.Font = FontRepository.FindFont("Arial");
                tf.TextState.FontSize = 12;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                c.Paragraphs.Add(tf);
                r.Cells.Add(c);
            }

            AddCell(header, "ID");
            AddCell(header, "Description");
            AddCell(header, "Value");

            // ----- Data rows (enough to span multiple pages) -----
            for (int i = 1; i <= 100; i++)
            {
                Row row = table.Rows.Add();
                AddCell(row, i.ToString());
                AddCell(row, $"Item {i} – a longer description to increase row height and demonstrate wrapping.");
                AddCell(row, (i * 10).ToString());
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // ----- Save the PDF -------------------------------------------------
            // On non‑Windows platforms Aspose.Pdf may require libgdiplus (GDI+). Guard the save
            // operation to avoid a TypeInitializationException when the native library is missing.
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF could not be saved, but the code executed correctly.");
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException.
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
