using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // HTML string that contains a <style> block.
        // The CSS rules set the font family, size, text color,
        // line height (spacing between lines) and paragraph margins.
        string html = @"
            <html>
            <head>
                <style>
                    .styled {
                        font-family: 'Helvetica';
                        font-size: 14pt;
                        color: #003366;
                        line-height: 1.5;
                        margin: 10pt 0;
                    }
                </style>
            </head>
            <body>
                <p class='styled'>This paragraph is styled via embedded CSS.</p>
                <p class='styled'>Another styled paragraph with custom spacing.</p>
            </body>
            </html>";

        // Create an HtmlFragment from the HTML string.
        HtmlFragment fragment = new HtmlFragment(html);

        // Create a new PDF document and add the fragment to the first page.
        using (Document doc = new Document())
        {
            // Add a blank page to host the HTML content.
            Page page = doc.Pages.Add();

            // Insert the HtmlFragment into the page's paragraph collection.
            page.Paragraphs.Add(fragment);

            // Save the document – guard the call on non‑Windows platforms where libgdiplus may be missing.
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

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException.
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
