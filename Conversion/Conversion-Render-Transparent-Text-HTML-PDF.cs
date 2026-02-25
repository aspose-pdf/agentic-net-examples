using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, HtmlLoadOptions, etc.)

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML document. HtmlLoadOptions is required for HTML input.
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // No need to call FlattenTransparency – we want to keep any transparency.
                // Save as PDF. No SaveOptions needed for PDF output.
                doc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: '{pdfPath}'.");
        }
        catch (TypeInitializationException)
        {
            // HTML loading may require GDI+ (Windows only). Handle gracefully on other platforms.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}