using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, HtmlSaveOptions, etc.

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output HTML file that will contain only the <body> markup
        const string outputHtmlPath = "output_body.html";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize HtmlSaveOptions
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions();

            // Generate only the content inside the <body> tag
            htmlOptions.HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent;

            // Optional: embed all resources (images, CSS, fonts) directly into the HTML file
            htmlOptions.PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml;

            // Save the PDF as HTML using the configured options
            pdfDocument.Save(outputHtmlPath, htmlOptions);
        }

        Console.WriteLine($"Successfully created HTML with body only: '{outputHtmlPath}'.");
    }
}