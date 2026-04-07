using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Add a paragraph that we will later locate
            TextFragment targetParagraph = new TextFragment("Target paragraph");
            page.Paragraphs.Add(targetParagraph);

            // Locate the paragraph by its text
            int targetIndex = -1;
            for (int i = 0; i < page.Paragraphs.Count; i++)
            {
                if (page.Paragraphs[i] is TextFragment tf && tf.Text == "Target paragraph")
                {
                    targetIndex = i;
                    break;
                }
            }

            if (targetIndex != -1)
            {
                // Create a simple table
                Table table = new Table { ColumnWidths = "100 100" };

                // First row (header)
                Row headerRow = table.Rows.Add();
                headerRow.Cells.Add("Header 1");
                headerRow.Cells.Add("Header 2");

                // Second row (data)
                Row dataRow = table.Rows.Add();
                dataRow.Cells.Add("Cell 1");
                dataRow.Cells.Add("Cell 2");

                // Insert the table right after the located paragraph
                page.Paragraphs.Insert(targetIndex + 1, table);
            }

            // Save the resulting PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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