using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new empty PDF document
        using (Document doc = new Document())
        {
            // Add a blank page to host the HTML fragment
            Page page = doc.Pages.Add();

            // Define CSS rules to control font, color, and spacing
            string css = @"
                <style>
                    .styledParagraph {
                        font-family: Arial, Helvetica, sans-serif;
                        color: #FF4500;               /* orange‑red text */
                        margin-top: 12pt;
                        margin-bottom: 12pt;
                        line-height: 1.5;             /* spacing between lines */
                    }
                </style>";

            // HTML content that uses the CSS class defined above
            string html = $@"
                {css}
                <p class='styledParagraph'>
                    This is a sample paragraph rendered from an HtmlFragment.
                    The font, color, and line spacing are controlled by the embedded CSS.
                </p>";

            // Create the HtmlFragment with the HTML string (including CSS)
            HtmlFragment htmlFragment = new HtmlFragment(html);

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(htmlFragment);

            // Save the resulting PDF – guard against missing libgdiplus on non‑Windows platforms
            string outputPath = "HtmlFragmentWithCss.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }

        Console.WriteLine("PDF creation attempt finished.");
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