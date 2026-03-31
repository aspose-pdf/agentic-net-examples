using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "center_aligned_page2.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add first (blank) page
            Page firstPage = doc.Pages.Add();

            // Add second page where the text will be centered
            Page secondPage = doc.Pages.Add();

            // Create a text fragment with the desired content
            TextFragment textFragment = new TextFragment("Centered Text on Page 2");

            // Set horizontal alignment to center
            textFragment.TextState.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center;

            // Add the text fragment to the second page
            secondPage.Paragraphs.Add(textFragment);

            // Save the PDF document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                    Console.WriteLine($"PDF saved to '{outputPath}'. (saved on non‑Windows platform)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF could not be saved using Aspose.Pdf's default renderer.");
                    // Optionally, you could implement an alternative saving strategy here.
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain to detect a missing native GDI+ library.
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
