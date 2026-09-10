using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string nickname  = "CustomIdentifier123";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the XMP metadata facade and bind the source PDF
        PdfXmpMetadata xmp = new PdfXmpMetadata();
        xmp.BindPdf(inputPdf);

        // Add or update the xmp:Nickname property
        xmp.Add(DefaultMetadataProperties.Nickname, nickname);

        // Save the PDF with the updated XMP metadata
        xmp.Save(outputPdf);

        Console.WriteLine($"Nickname '{nickname}' has been set and saved to '{outputPdf}'.");
    }
}