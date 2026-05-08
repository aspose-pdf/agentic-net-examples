using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            Table table = new Table
            {
                Left = 50,
                Top = 700,
                // Adjust column widths based on content
                ColumnAdjustment = ColumnAdjustment.AutoFitToContent
                // No DefaultCellAutoFitBehavior property – ColumnAdjustment handles auto‑fit for cells
            };

            // First row
            Row row1 = table.Rows.Add();
            Cell cell11 = row1.Cells.Add();
            cell11.Paragraphs.Add(new TextFragment("Short"));
            Cell cell12 = row1.Cells.Add();
            cell12.Paragraphs.Add(new TextFragment("A much longer piece of text that should cause the column to expand automatically."));

            // Second row
            Row row2 = table.Rows.Add();
            Cell cell21 = row2.Cells.Add();
            cell21.Paragraphs.Add(new TextFragment("Another"));
            Cell cell22 = row2.Cells.Add();
            cell22.Paragraphs.Add(new TextFragment("Text"));

            page.Paragraphs.Add(table);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
