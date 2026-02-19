using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string pdfPath = "input.pdf";

        // Directory where HTML files will be saved
        const string outputDirectory = "output_html";

        // Ensure the input PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        // Create the output directory if it does not exist
        if (!Directory.Exists(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        // Full path for the resulting HTML file (single file containing only the body)
        string htmlOutputPath = Path.Combine(outputDirectory, "output.html");

        // Configure HTML conversion options
        HtmlSaveOptions htmlOptions = new HtmlSaveOptions
        {
            // Generate only the content inside the <body> tag
            HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent,

            // If you need one HTML file per PDF page, set this to true.
            // SplitIntoPages = true,
        };

        // Load the PDF document
        Document pdfDocument = new Document(pdfPath);

        // Save the PDF as HTML using the configured options
        pdfDocument.Save(htmlOutputPath, htmlOptions);

        Console.WriteLine($"Conversion completed. HTML saved to '{htmlOutputPath}'.");
    }
}