using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

public class AlignTableCenter
{
    public static void Main()
    {
        const string outputPath = "output.pdf";
        using (Document document = new Document())
        {
            Page page = document.Pages.Add();

            Table table = new Table
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                ColumnWidths = "100 100 100"
            };

            Row row1 = table.Rows.Add();
            row1.Cells.Add("Cell 1");
            row1.Cells.Add("Cell 2");
            row1.Cells.Add("Cell 3");

            Row row2 = table.Rows.Add();
            row2.Cells.Add("Cell 4");
            row2.Cells.Add("Cell 5");
            row2.Cells.Add("Cell 6");

            page.Paragraphs.Add(table);

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
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
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