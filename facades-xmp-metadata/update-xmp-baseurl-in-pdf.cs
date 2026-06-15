using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public class PdfMetadataService
{
    // Updates the XMP BaseURL of a PDF to match the supplied base URL.
    public void UpdatePdfBaseUrl(string inputPdfPath, string outputPdfPath, string baseUrl)
    {
        // Ensure the base URL ends with a slash (XMP expects a directory‑style URL).
        if (!baseUrl.EndsWith("/"))
            baseUrl += "/";

        // Load the PDF document.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Bind the XMP metadata facade to the loaded document.
            PdfXmpMetadata xmpMeta = new PdfXmpMetadata(pdfDoc);

            // Set the BaseURL XMP property.
            xmpMeta.Add(DefaultMetadataProperties.BaseURL, new XmpValue(baseUrl));

            // Retrieve the updated XMP packet.
            byte[] xmpBytes = xmpMeta.GetXmpMetadata();

            // Apply the updated XMP metadata back to the document.
            using (MemoryStream xmpStream = new MemoryStream(xmpBytes))
            {
                pdfDoc.SetXmpMetadata(xmpStream);
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: <inputPdfPath> <outputPdfPath> <baseUrl>
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <inputPdfPath> <outputPdfPath> <baseUrl>");
            return;
        }

        string inputPdf = args[0];
        string outputPdf = args[1];
        string baseUrl = args[2];

        var service = new PdfMetadataService();
        service.UpdatePdfBaseUrl(inputPdf, outputPdf, baseUrl);

        Console.WriteLine("XMP BaseURL updated successfully.");
    }
}