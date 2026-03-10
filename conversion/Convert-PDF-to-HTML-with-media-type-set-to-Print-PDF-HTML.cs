using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Included as per requirement

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputHtml = "output.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Initialize HtmlSaveOptions for PDF → HTML conversion
            HtmlSaveOptions saveOptions = new HtmlSaveOptions();

            // Note: HtmlSaveOptions does not have a MediaType property.
            // The media type (e.g., "print") is applicable when loading HTML
            // via HtmlLoadOptions for HTML → PDF conversion, not for PDF → HTML.
            // Therefore, we proceed without setting a media type.

            // Save the document as HTML
            pdfDocument.Save(outputHtml, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to HTML: {outputHtml}");
    }
}