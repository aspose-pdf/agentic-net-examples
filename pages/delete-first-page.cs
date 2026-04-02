using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class DeleteFirstPage
{
    public static void Main()
    {
        // Create a sample PDF with three pages
        using (Document doc = new Document())
        {
            // Add three pages to the document
            doc.Pages.Add();
            doc.Pages.Add();
            doc.Pages.Add();

            // Add simple text to each page for illustration
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                TextFragment fragment = new TextFragment("Page " + i);
                fragment.Position = new Position(100, 700);
                doc.Pages[i].Paragraphs.Add(fragment);
            }

            // Delete the first page (page numbers are 1‑based)
            doc.Pages.Delete(1);

            // Save the modified PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            string outputPath = "output.pdf";
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
                    Console.WriteLine($"PDF saved to '{outputPath}'. (non‑Windows platform, libgdiplus may be required)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
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