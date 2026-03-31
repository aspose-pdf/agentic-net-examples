using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string metadataKey = "Confidential";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the XMP metadata handler
        PdfXmpMetadata xmpMetadata = new PdfXmpMetadata();
        xmpMetadata.BindPdf(inputPath);

        // Check whether the custom key exists before reading it
        if (xmpMetadata.Contains(metadataKey))
        {
            // Retrieve the value associated with the key
            XmpValue value = xmpMetadata[metadataKey];
            Console.WriteLine($"Metadata '{metadataKey}' found: {value}");
        }
        else
        {
            Console.WriteLine($"Metadata key '{metadataKey}' does not exist in the document.");
        }
    }
}