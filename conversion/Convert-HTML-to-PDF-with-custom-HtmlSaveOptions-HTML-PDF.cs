using System;
using System.IO;
using Aspose.Pdf;

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
            // Custom load options for the HTML source.
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            // Load the HTML document using the custom options.
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Optional: customize PDF save options (e.g., default font).
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // DefaultFontName = "Arial"
                };

                // Save the loaded document as PDF.
                doc.Save(pdfPath, pdfOptions);
            }

            Console.WriteLine($"HTML successfully converted to PDF: '{pdfPath}'");
        }
        catch (TypeInitializationException)
        {
            // HTML loading relies on GDI+ and is Windows‑only.
            Console.WriteLine("HTML loading requires Windows GDI+. Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}