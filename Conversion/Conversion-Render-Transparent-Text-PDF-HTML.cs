using System;
using System.IO;
using Aspose.Pdf;

class PdfToHtmlTransparentText
{
    static void Main(string[] args)
    {
        // Input PDF path (first argument) and output HTML path (second argument)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfToHtmlTransparentText <input.pdf> <output.html>");
            return;
        }

        string pdfPath = args[0];
        string htmlPath = args[1];

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(pdfPath);

            // Configure HTML conversion options
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Render all text (including transparent text) as images so it becomes visible in HTML
                RenderTextAsImage = true,

                // Keep the default layout (fixed layout) to preserve appearance
                FixedLayout = true
            };

            // Save the PDF as HTML using the configured options
            pdfDocument.Save(htmlPath, htmlOptions);
            Console.WriteLine($"Conversion completed successfully. HTML saved to '{htmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}