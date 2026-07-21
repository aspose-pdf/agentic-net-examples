using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document within a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the XMP metadata facade bound to the document
            PdfXmpMetadata xmp = new PdfXmpMetadata(doc);

            // Retrieve original XMP metadata and log its size
            byte[] originalData = xmp.GetXmpMetadata();
            Console.WriteLine($"Original XMP size: {originalData.Length} bytes");

            // Modify the XMP metadata (add a custom field)
            xmp.Add("my:customField", "CustomValue");

            // Retrieve modified XMP metadata and log its size
            byte[] modifiedData = xmp.GetXmpMetadata();
            Console.WriteLine($"Modified XMP size: {modifiedData.Length} bytes");

            // Save the updated PDF (XMP changes are persisted)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}