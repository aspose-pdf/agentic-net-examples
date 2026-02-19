using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source HTML file
        string inputHtmlPath = "input.html";

        // Path where the body‑only HTML will be saved
        string outputHtmlPath = "output_body.html";

        // Verify that the input file exists
        if (!File.Exists(inputHtmlPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputHtmlPath}");
            return;
        }

        // Load the HTML document
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();
        Document pdfDocument = new Document(inputHtmlPath, loadOptions);

        // Set save options to generate only the content inside the <body> tag
        HtmlSaveOptions saveOptions = new HtmlSaveOptions
        {
            HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent
        };

        // Save the result as HTML containing only body content
        pdfDocument.Save(outputHtmlPath, saveOptions);

        Console.WriteLine($"Body‑only HTML saved to: {outputHtmlPath}");
    }
}