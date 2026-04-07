using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "alternating_table.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with three equal columns and a thin black border for cells
            Table table = new Table
            {
                ColumnWidths = "100 100 100",
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black)
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.DefaultCellTextState = new TextState { FontSize = 12, FontStyle = FontStyles.Bold };
            foreach (string headerText in new[] { "Header 1", "Header 2", "Header 3" })
            {
                Cell hdrCell = header.Cells.Add();
                hdrCell.Paragraphs.Add(new TextFragment(headerText));
            }

            // Add sample data rows
            for (int r = 0; r < 10; r++)
            {
                Row row = table.Rows.Add();
                for (int c = 0; c < 3; c++)
                {
                    Cell cell = row.Cells.Add();
                    cell.Paragraphs.Add(new TextFragment($"Row {r + 1} Col {c + 1}"));
                }
            }

            // Apply alternating background colors to cells based on row index parity
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Row row = table.Rows[i];
                Aspose.Pdf.Color bgColor = (i % 2 == 0) ? Aspose.Pdf.Color.LightGray : Aspose.Pdf.Color.White;
                foreach (Cell cell in row.Cells)
                {
                    cell.BackgroundColor = bgColor;
                }
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
