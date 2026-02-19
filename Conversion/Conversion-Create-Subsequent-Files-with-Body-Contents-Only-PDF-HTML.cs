using System;
using System.IO;
using Aspose.Pdf;

class PdfToHtmlBodyOnly
{
    static void Main(string[] args)
    {
        // Input PDF file path (first argument) and output folder (second argument)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfToHtmlBodyOnly <input-pdf> <output-folder>");
            return;
        }

        string pdfPath = args[0];
        string outputFolder = args[1];

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {pdfPath}");
            return;
        }

        // Ensure the output folder exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(pdfPath);

            // Configure HTML save options:
            // - Split each PDF page into a separate HTML file.
            // - Keep only the content that resides inside the <body> tag.
            // - Do not embed external resources (fonts, images, CSS) to keep files lightweight.
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                SplitIntoPages = true,
                HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent,
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.NoEmbedding
            };

            // Save the PDF as HTML files. When SplitIntoPages is true, the first parameter
            // must be a folder path; Aspose.Pdf will generate one HTML file per page.
            pdfDocument.Save(outputFolder, htmlOptions);

            Console.WriteLine($"Conversion completed. HTML files are saved in: {outputFolder}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}