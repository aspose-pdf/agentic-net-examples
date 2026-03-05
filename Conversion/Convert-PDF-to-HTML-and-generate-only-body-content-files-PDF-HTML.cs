using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputHtmlPath = "output_body.html";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure HTML save options
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions();

            // Generate only the content inside the <body> tag
            htmlOpts.HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent;

            // Optional: embed all resources (images, CSS, fonts) into the single HTML file
            htmlOpts.PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml;

            // Save the PDF as HTML using the configured options
            pdfDoc.Save(outputHtmlPath, htmlOpts);
        }

        Console.WriteLine($"HTML body-only file created at '{outputHtmlPath}'.");
    }
}