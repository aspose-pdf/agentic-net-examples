using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // On non‑Windows platforms Aspose.Pdf may require GDI+ (libgdiplus).
        // Guard the whole operation to avoid TypeInitializationException.
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.WriteLine("Skipping PDF generation – GDI+ (libgdiplus) is not available on this platform.");
            return;
        }

        // Collection of HTML strings to be converted
        List<string> htmlStrings = new List<string>();
        htmlStrings.Add("<html><body><h1>First Page</h1><p>This is the first page.</p></body></html>");
        htmlStrings.Add("<html><body><h2>Second Page</h2><p>Content of second page.</p></body></html>");

        // Create the final PDF document
        using (Document finalDoc = new Document())
        {
            foreach (string html in htmlStrings)
            {
                // Custom rendering options for HTML to PDF conversion
                HtmlLoadOptions loadOptions = new HtmlLoadOptions();
                loadOptions.PageInfo = new PageInfo();
                loadOptions.PageInfo.Width = 595;   // A4 width in points
                loadOptions.PageInfo.Height = 842;  // A4 height in points

                // Load the HTML string into a temporary PDF document
                using (MemoryStream htmlStream = new MemoryStream(Encoding.UTF8.GetBytes(html)))
                using (Document tempDoc = new Document(htmlStream, loadOptions))
                {
                    // Append the generated pages to the final document
                    finalDoc.Pages.Add(tempDoc.Pages);
                }
            }

            // Save the combined PDF – guard against missing GDI+ on non‑Windows platforms
            string outputPath = "output.pdf";
            try
            {
                finalDoc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
            }
        }
    }

    // Helper method to walk the inner‑exception chain and detect a missing native GDI+ library
    private static bool ContainsDllNotFound(Exception ex)
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
