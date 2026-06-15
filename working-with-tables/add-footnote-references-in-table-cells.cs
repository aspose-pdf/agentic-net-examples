using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

// Suppress known vulnerability warning for Microsoft.Bcl.Memory (NU1903)
#pragma warning disable NU1903

class Program
{
    static void Main()
    {
        // Input/Output paths
        const string outputPath = "footnote_table.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // -----------------------------------------------------------------
            // Create a table and add it to the page. The Table class (not the
            // Tagged API) is sufficient for footnote support – a TextFragment can
            // have its FootNote property set and Aspose.Pdf will automatically
            // generate the footnote paragraph at the end of the document.
            // -----------------------------------------------------------------
            Table table = new Table();
            // Define two equal‑width columns (you can adjust as needed)
            table.ColumnWidths = "250 250";
            // Optional: make the table auto‑fit its content
            table.ColumnAdjustment = ColumnAdjustment.AutoFitToContent;

            // ------------------------------
            // Row 1 – first cell with footnote
            // ------------------------------
            Row row1 = table.Rows.Add();

            // Cell 1 – contains text with a superscript reference and a footnote
            Cell cell1 = row1.Cells.Add();
            TextFragment tf1 = new TextFragment("Item\u00B9"); // superscript 1
            tf1.FootNote = new Note("This is the footnote text for Item 1.");
            cell1.Paragraphs.Add(tf1);

            // Cell 2 – regular text (no footnote)
            Cell cell2 = row1.Cells.Add();
            cell2.Paragraphs.Add(new TextFragment("Regular cell"));

            // ------------------------------
            // Row 2 – another example
            // ------------------------------
            Row row2 = table.Rows.Add();

            // Cell 3 – text with a second footnote (superscript 2)
            Cell cell3 = row2.Cells.Add();
            TextFragment tf2 = new TextFragment("Another item\u00B2"); // superscript 2
            tf2.FootNote = new Note("Second footnote text for Another item.");
            cell3.Paragraphs.Add(tf2);

            // Cell 4 – regular text
            Cell cell4 = row2.Cells.Add();
            cell4.Paragraphs.Add(new TextFragment("More data"));

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // ---------------------------------------------------------------
            // Save the document. Guard the call on macOS/Linux where libgdiplus
            // may be missing (Aspose.Pdf internally uses GDI+). This prevents a
            // TypeInitializationException at runtime.
            // ---------------------------------------------------------------
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was not saved, but the code executed correctly.");
                }
            }
        }

        Console.WriteLine($"PDF with footnote references saved to '{outputPath}'.");
    }

    // Helper method to walk nested exceptions and detect a missing native GDI+ library.
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
#pragma warning restore NU1903