using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF with three pages
        using (Document document = new Document())
        {
            // Add three pages with sample text
            for (int i = 1; i <= 3; i++)
            {
                Page page = document.Pages.Add();
                // Add some sample text to the page
                TextFragment fragment = new TextFragment("Sample content on page " + i);
                page.Paragraphs.Add(fragment);
            }

            // Define HTML header with embedded CSS styling
            string headerHtml = "<div style=\"font-family:Arial; font-size:14pt; color:#FF0000; text-align:center;\">My HTML Header</div>";

            // Apply the HTML header to the first three pages
            for (int i = 1; i <= 3; i++)
            {
                Page page = document.Pages[i];
                HeaderFooter header = new HeaderFooter();
                // Add the HtmlFragment to the header's Paragraphs collection
                header.Paragraphs.Add(new HtmlFragment(headerHtml));
                page.Header = header;
            }

            // Save the resulting PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            string outputPath = "output.pdf";
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
                    Console.WriteLine($"PDF saved to '{outputPath}' (non‑Windows platform)." );
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was not saved, but the program ran without crashing.");
                }
            }
        }
    }

    // Helper method to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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
