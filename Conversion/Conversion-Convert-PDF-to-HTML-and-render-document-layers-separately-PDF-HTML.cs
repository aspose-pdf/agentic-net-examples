using System;
using System.IO;
using Aspose.Pdf;

class PdfToHtmlWithLayers
{
    static void Main(string[] args)
    {
        // Input PDF path (first argument) and output HTML folder (second argument)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfToHtmlWithLayers <input-pdf> <output-folder>");
            return;
        }

        string pdfPath = args[0];
        string outputFolder = args[1];

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
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

            // Configure HTML conversion options
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Create a separate HTML file for each PDF page
                SplitIntoPages = true,

                // Export marked‑content layers as separate <div> elements
                ConvertMarkedContentToLayers = true,

                // Optional: keep a simple title
                Title = Path.GetFileNameWithoutExtension(pdfPath)
            };

            // The Save method with options requires a full file name.
            // When SplitIntoPages is true, the library will generate files like "output_1.html", "output_2.html", etc.
            string outputPath = Path.Combine(outputFolder, "output.html");

            // Perform the conversion
            pdfDocument.Save(outputPath, htmlOptions);

            Console.WriteLine($"PDF successfully converted to HTML. Files are located in '{outputFolder}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}