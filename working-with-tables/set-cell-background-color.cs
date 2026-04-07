using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        string outputPath = "output.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            Table table = new Table();
            page.Paragraphs.Add(table);
            // Define two columns of equal width
            table.ColumnWidths = "100 100";

            // Add a row to the table
            Row row = table.Rows.Add();

            // First cell – set background color (values normalized to 0..1)
            Cell cell1 = row.Cells.Add("Cell 1");
            cell1.BackgroundColor = Aspose.Pdf.Color.FromRgb(
                255.0 / 255.0,   // Red   = 1.0
                200.0 / 255.0,   // Green = 0.7843
                200.0 / 255.0);  // Blue  = 0.7843

            // Second cell – default appearance
            Cell cell2 = row.Cells.Add("Cell 2");

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
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform; PDF could not be saved.");
                }
            }
        }
    }

    private static bool ContainsDllNotFound(Exception ex)
    {
        Exception current = ex; // removed nullable annotation to silence CS8632 warning
        while (current != null)
        {
            if (current is DllNotFoundException)
                return true;
            current = current.InnerException;
        }
        return false;
    }
}
