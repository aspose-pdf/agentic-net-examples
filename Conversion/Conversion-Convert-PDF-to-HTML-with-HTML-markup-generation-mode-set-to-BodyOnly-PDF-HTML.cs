using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Output HTML file path
        const string outputHtmlPath = "output.html";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdfPath);

        // Configure HTML save options to generate only the body content
        HtmlSaveOptions htmlOptions = new HtmlSaveOptions();
        htmlOptions.HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent;

        // Save the PDF as HTML using the configured options
        pdfDocument.Save(outputHtmlPath, htmlOptions);

        Console.WriteLine($"PDF successfully converted to HTML. Output saved at '{outputHtmlPath}'.");
    }
}