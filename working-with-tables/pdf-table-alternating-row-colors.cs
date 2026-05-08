using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "alternating_rows.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with 5 columns
            Table table = new Table
            {
                ColumnWidths = "100 100 100 100 100",
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Add a header row
            Row header = table.Rows.Add();
            for (int c = 0; c < 5; c++)
            {
                Cell cell = header.Cells.Add();
                cell.Paragraphs.Add(new TextFragment($"Header {c + 1}"));
                cell.BackgroundColor = Aspose.Pdf.Color.LightBlue;
                cell.DefaultCellTextState = new TextState { FontSize = 12, FontStyle = FontStyles.Bold };
            }

            // Add data rows
            for (int r = 0; r < 10; r++)
            {
                Row row = table.Rows.Add();
                for (int c = 0; c < 5; c++)
                {
                    Cell cell = row.Cells.Add();
                    cell.Paragraphs.Add(new TextFragment($"R{r + 1}C{c + 1}"));
                }
            }

            // Apply alternating background colors to each row's cells
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Row row = table.Rows[i];
                // Choose background based on row index parity
                Aspose.Pdf.Color bg = (i % 2 == 0) ? Aspose.Pdf.Color.LightGray : Aspose.Pdf.Color.White;
                foreach (Cell cell in row.Cells)
                {
                    cell.BackgroundColor = bg;
                }
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }

        Console.WriteLine($"PDF saved to {Path.GetFullPath(outputPath)}");
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
