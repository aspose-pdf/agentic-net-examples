using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // for Color

class Program
{
    static void Main()
    {
        using (Document document = new Document())
        {
            Page page = document.Pages.Add();
            Table table = new Table();
            table.ColumnWidths = "100 100 100 100";
            Row row = table.Rows.Add();

            // Merge three adjacent columns into a single cell
            Cell spanningCell = row.Cells.Add("Spanning Cell");
            spanningCell.ColSpan = 3;
            spanningCell.BackgroundColor = Color.Yellow;

            Cell normalCell = row.Cells.Add("Normal Cell");
            normalCell.BackgroundColor = Color.LightGray;

            page.Paragraphs.Add(table);

            // Guard Document.Save against missing GDI+ (libgdiplus) on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                document.Save("output.pdf");
                Console.WriteLine("PDF saved to 'output.pdf'.");
            }
            else
            {
                try
                {
                    document.Save("output.pdf");
                    Console.WriteLine("PDF saved to 'output.pdf' (non‑Windows platform).");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "Saving the PDF without GDI+ support is not possible, so the operation is skipped.");
                }
            }
        }
    }

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
