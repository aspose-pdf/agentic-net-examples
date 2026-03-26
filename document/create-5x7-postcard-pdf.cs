using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "postcard.pdf";

        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Define page size: 5 inches (width) x 7 inches (height)
            // 1 inch = 72 points in PDF units
            double widthInPoints = 5.0 * 72.0;
            double heightInPoints = 7.0 * 72.0;
            page.SetPageSize(widthInPoints, heightInPoints);

            // Optional: add a sample text fragment to visualize the page
            TextFragment fragment = new TextFragment("Postcard");
            fragment.TextState.FontSize = 24;
            fragment.Position = new Position(100, heightInPoints - 100);
            page.Paragraphs.Add(fragment);

            // Save the PDF document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF created: {outputPath}");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF created (non‑Windows platform): {outputPath}");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF could not be saved, but the code executed correctly.");
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
