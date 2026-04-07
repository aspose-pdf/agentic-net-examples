using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class SetCellVerticalAlignment
{
    public static void Main()
    {
        // Create a new PDF document
        using (Document document = new Document())
        {
            // Add a page to the document
            Page page = document.Pages.Add();

            // Create a table with two columns of equal width
            Table table = new Table { ColumnWidths = "200 200" };

            // Add a row to the table
            Row row = table.Rows.Add();

            // First cell – top aligned (for contrast)
            Cell cell1 = row.Cells.Add();
            cell1.Paragraphs.Add(new TextFragment("Top aligned"));
            cell1.VerticalAlignment = Aspose.Pdf.VerticalAlignment.Top;

            // Second cell – middle (center) aligned
            Cell cell2 = row.Cells.Add();
            cell2.Paragraphs.Add(new TextFragment("Middle aligned"));
            cell2.VerticalAlignment = Aspose.Pdf.VerticalAlignment.Center;

            // Add the table to the page
            page.Paragraphs.Add(table);

            string outputPath = "output.pdf";

            // Guard Document.Save against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was not saved using GDI+ dependent features.");
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