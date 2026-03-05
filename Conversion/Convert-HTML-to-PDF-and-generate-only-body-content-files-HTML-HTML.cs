using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputHtml = "input.html";
        const string outputPdf = "output.pdf";
        const string outputBodyHtml = "output_body.html";

        if (!File.Exists(inputHtml))
        {
            Console.Error.WriteLine($"Input file not found: {inputHtml}");
            return;
        }

        // Load the HTML file into a Document object.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();
        using (Document doc = new Document(inputHtml, loadOptions))
        {
            // Save the document as PDF.
            doc.Save(outputPdf);

            // Configure HtmlSaveOptions to generate only the body content.
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions();
            htmlOptions.HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent;

            // Save the document back to HTML, containing only the <body> markup.
            doc.Save(outputBodyHtml, htmlOptions);
        }

        Console.WriteLine("HTML to PDF conversion and body‑only HTML generation completed.");
    }
}