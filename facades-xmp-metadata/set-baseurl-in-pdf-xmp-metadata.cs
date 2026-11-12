using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output.pdf";         // destination PDF
        const string baseUrl   = "https://www.example.com"; // URL to set in XMP

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Set the BaseUrl property in the XMP metadata.
        // Aspose.Pdf exposes XMP metadata through the Metadata dictionary.
        // The key "BaseURL" follows the XMP specification for the base URL.
        pdfDocument.Metadata["BaseURL"] = baseUrl;

        // Save the updated PDF
        pdfDocument.Save(outputPdf);

        Console.WriteLine($"BaseUrl set to '{baseUrl}' and saved as '{outputPdf}'.");
    }
}
