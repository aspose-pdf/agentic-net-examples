using System;
using System.IO;
using Aspose.Pdf;

class SplitPdfToHtml
{
    static void Main(string[] args)
    {
        // Input PDF file path (first argument) and output folder (second argument)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: SplitPdfToHtml <input-pdf> <output-folder>");
            return;
        }

        string inputPdfPath = args[0];
        string outputFolder = args[1];

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Ensure the output folder exists
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            // Configure HTML save options to split each PDF page into a separate HTML file
            HtmlSaveOptions saveOptions = new HtmlSaveOptions
            {
                SplitIntoPages = true,                     // one HTML file per PDF page
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteAllHtml
            };

            // Save the PDF as HTML pages. The output path is a folder; Aspose.Pdf will create files like "page1.html", "page2.html", etc.
            string outputPath = Path.Combine(outputFolder, "output.html"); // the file name is used as a base name
            pdfDocument.Save(outputPath, saveOptions);

            Console.WriteLine($"PDF successfully split into HTML pages in folder: {outputFolder}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
