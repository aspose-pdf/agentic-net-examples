using System;
using System.IO;
using Aspose.Pdf;               // Document, HtmlSaveOptions are in this namespace

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output HTML file path
        const string htmlPath = "output.html";

        // Verify the input file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Open the existing PDF document (wrapped in using for deterministic disposal)
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Initialize HtmlSaveOptions – required when saving to a non‑PDF format
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions();

                // Save the PDF as HTML using the options
                pdfDocument.Save(htmlPath, htmlOptions);
            }

            Console.WriteLine($"PDF successfully converted to HTML: '{htmlPath}'");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}