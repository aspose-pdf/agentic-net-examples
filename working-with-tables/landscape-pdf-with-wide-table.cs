using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the output PDF
        const string outputPath = "landscape_table.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Set the page orientation to landscape.
            page.PageInfo.IsLandscape = true;
            // Adjust the page size to A4 landscape dimensions (width > height).
            page.SetPageSize(842, 595);

            // Create a wide table with many columns to demonstrate landscape fitting
            Table table = new Table
            {
                Top = 50,
                Left = 20,
                BackgroundColor = Color.LightGray,
                ColumnWidths = "100 100 100 100 100 100 100 100"
            };

            // Add a header row
            Row header = table.Rows.Add();
            for (int i = 1; i <= 8; i++)
            {
                Cell cell = header.Cells.Add($"Header {i}");
                cell.DefaultCellTextState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica-Bold"),
                    FontSize = 12,
                    ForegroundColor = Color.Black,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
            }

            // Add a few data rows
            for (int r = 1; r <= 5; r++)
            {
                Row dataRow = table.Rows.Add();
                for (int c = 1; c <= 8; c++)
                {
                    Cell cell = dataRow.Cells.Add($"R{r}C{c}");
                    cell.DefaultCellTextState = new TextState
                    {
                        Font = FontRepository.FindFont("Helvetica"),
                        FontSize = 10,
                        ForegroundColor = Color.Black
                    };
                }
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the document – guard against missing GDI+ on non‑Windows platforms
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
                                      "The PDF was generated in memory but could not be saved to disk.");
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
