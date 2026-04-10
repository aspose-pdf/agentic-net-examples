using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputHtmlPath = "output.html";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: using for deterministic disposal)
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Configure HTML save options and enable SVG compression
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    CompressSvgGraphicsIfAny = true
                };

                // Save as HTML with explicit options (required for non‑PDF output)
                // Wrap in try‑catch because HTML conversion may require GDI+ (Windows only)
                try
                {
                    pdfDocument.Save(outputHtmlPath, htmlOptions);
                    Console.WriteLine($"PDF successfully converted to HTML: {outputHtmlPath}");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}