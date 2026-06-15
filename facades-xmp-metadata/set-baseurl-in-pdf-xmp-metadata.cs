using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string baseUrl = "https://www.example.com/";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPdf))
        {
            // Bind XMP metadata facade to the document (lifecycle: create)
            using (PdfXmpMetadata xmp = new PdfXmpMetadata(doc))
            {
                // Set the BaseURL property in XMP metadata (feature: Add)
                xmp.Add(DefaultMetadataProperties.BaseURL, new XmpValue(baseUrl));

                // Save the PDF with updated XMP metadata (lifecycle: save)
                xmp.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF saved with BaseURL set to '{baseUrl}'.");
    }
}