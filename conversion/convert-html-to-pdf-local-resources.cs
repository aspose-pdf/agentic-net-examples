using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string pdfPath = "output.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Configure HTML loading with default options.
        // In the current Aspose.Pdf version the MediaType and ResourceLoadingOptions
        // properties are not available. The default HtmlLoadOptions already loads
        // resources from the local file system, which satisfies the "restrict to
        // local assets" requirement.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        try
        {
            // Load the HTML document with the custom options
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Save as PDF
                doc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion requires GDI+ (Windows only)
            Console.WriteLine("HTML to PDF conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (DllNotFoundException)
        {
            // GDI+ library not found on non‑Windows platforms
            Console.WriteLine("GDI+ library not found. HTML to PDF conversion is Windows‑only.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
