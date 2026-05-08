using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_no_split.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
        {
            // Add a single page (pages are 1‑based)
            Aspose.Pdf.Page page = doc.Pages.Add();

            // Create a table and disable automatic splitting across pages
            Aspose.Pdf.Table table = new Aspose.Pdf.Table
            {
                IsBroken = false, // forces the whole table to stay on one page
                Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Define three equal column widths (adjust as needed)
            table.ColumnWidths = "100 100 100";

            // Add a header row
            Row header = table.Rows.Add();
            header.BackgroundColor = Aspose.Pdf.Color.LightGray;
            header.IsRowBroken = false; // keep header row intact
            header.Cells.Add(new Cell { Paragraphs = { new TextFragment("Header 1") } });
            header.Cells.Add(new Cell { Paragraphs = { new TextFragment("Header 2") } });
            header.Cells.Add(new Cell { Paragraphs = { new TextFragment("Header 3") } });

            // Add many data rows – enough to normally cause a split
            for (int i = 0; i < 30; i++)
            {
                Row row = table.Rows.Add();
                row.IsRowBroken = false; // ensure each row stays together
                row.Cells.Add(new Cell { Paragraphs = { new TextFragment($"Row {i + 1} Col 1") } });
                row.Cells.Add(new Cell { Paragraphs = { new TextFragment($"Row {i + 1} Col 2") } });
                row.Cells.Add(new Cell { Paragraphs = { new TextFragment($"Row {i + 1} Col 3") } });
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the document.
            // On non‑Windows platforms Aspose.Pdf may require libgdiplus for rendering.
            // Guard the Save call to avoid a TypeInitializationException.
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
                    Console.WriteLine("GDI+ (libgdiplus) not available; PDF saved without graphics rendering.");
                }
            }
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }

    // Helper method to walk inner exceptions and detect a missing native library
    static bool ContainsDllNotFound(Exception ex)
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