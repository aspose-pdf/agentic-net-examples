using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Entry point – simulate a web‑service call by passing the host URL as an argument.
    static void Main(string[] args)
    {
        // Expected arguments: input PDF path, output PDF path, host URL (e.g. "https://example.com")
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: <inputPdf> <outputPdf> <hostUrl>");
            return;
        }

        string inputPdfPath  = args[0];
        string outputPdfPath = args[1];
        string hostUrl       = args[2];

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            UpdateXmpBaseUrl(inputPdfPath, outputPdfPath, hostUrl);
            Console.WriteLine($"XMP BaseURL updated and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Updates the XMP BaseURL property of a PDF document.
    static void UpdateXmpBaseUrl(string inputPdf, string outputPdf, string baseUrl)
    {
        // Load the source PDF (lifecycle rule – use using block for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Bind the XMP metadata facade to the loaded document
            PdfXmpMetadata xmpMetadata = new PdfXmpMetadata(pdfDoc);

            // Add or replace the BaseURL entry in the XMP packet
            // DefaultMetadataProperties.BaseURL corresponds to the xmp:BaseURL property.
            // XmpValue wraps the string value.
            xmpMetadata.Add(DefaultMetadataProperties.BaseURL, new XmpValue(baseUrl));

            // Save the updated PDF (save rule – use the facade's Save method)
            // This writes the modified XMP packet back into the PDF.
            xmpMetadata.Save(outputPdf);
        }
    }
}