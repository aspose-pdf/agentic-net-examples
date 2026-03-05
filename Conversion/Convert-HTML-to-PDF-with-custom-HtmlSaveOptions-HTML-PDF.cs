using System;
using System.IO;
using Aspose.Pdf; // Document, HtmlLoadOptions, PdfSaveOptions are all in this namespace

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";   // Path to source HTML file
        const string pdfPath  = "output.pdf";   // Desired PDF output path

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML document using HtmlLoadOptions (empty base path)
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Customize PDF saving options (example: set a default font)
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    DefaultFontName = "Arial"   // Substitute missing fonts with Arial
                };

                // Save the loaded document as PDF with the custom options
                doc.Save(pdfPath, pdfOptions);
                Console.WriteLine($"HTML successfully converted to PDF: '{pdfPath}'");
            }
        }
        catch (TypeInitializationException)
        {
            // HTML loading may require GDI+ (Windows only)
            Console.WriteLine("HTML loading requires Windows (GDI+). Conversion skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
