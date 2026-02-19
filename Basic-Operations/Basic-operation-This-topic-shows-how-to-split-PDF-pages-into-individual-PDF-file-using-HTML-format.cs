using System;
using System.IO;
using Aspose.Pdf;

class SplitPdfToHtmlPages
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }

        // Directory where the per‑page HTML files will be written
        const string outputFolder = "HtmlPages";

        try
        {
            // Ensure the output directory exists
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Configure HTML save options to split each PDF page into its own HTML file
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                SplitIntoPages = true
            };

            // Save the document – when SplitIntoPages is true, providing a folder path
            // causes Aspose.Pdf to generate one HTML file per page inside that folder.
            pdfDocument.Save(outputFolder, htmlOptions);

            Console.WriteLine($"PDF pages have been split into HTML files in folder: {outputFolder}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}