using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputHtmlPath  = "input.html";
        const string outputHtmlPath = "output_body.html";

        if (!File.Exists(inputHtmlPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputHtmlPath}");
            return;
        }

        // Load the HTML file into a Document (Aspose.Pdf can parse HTML)
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();
        using (Document doc = new Document(inputHtmlPath, loadOptions))
        {
            // Configure save options to generate only the <body> content
            HtmlSaveOptions saveOptions = new HtmlSaveOptions();
            saveOptions.HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent;

            // Save as HTML; explicit SaveOptions are required for non‑PDF output
            doc.Save(outputHtmlPath, saveOptions);
        }

        Console.WriteLine($"Body‑only HTML saved to '{outputHtmlPath}'.");
    }
}