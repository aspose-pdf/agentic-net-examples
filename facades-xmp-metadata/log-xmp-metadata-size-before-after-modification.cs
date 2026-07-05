using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the XMP metadata facade bound to the loaded document
            PdfXmpMetadata xmp = new PdfXmpMetadata(doc);

            // Retrieve the original XMP metadata as a byte array and log its size
            byte[] originalData = xmp.GetXmpMetadata();
            Console.WriteLine($"Original XMP size: {originalData.Length} bytes");

            // Modify the XMP metadata – add a custom property (key/value pair)
            xmp.Add("custom:Modified", "true");

            // Retrieve the modified XMP metadata and log its new size
            byte[] modifiedData = xmp.GetXmpMetadata();
            Console.WriteLine($"Modified XMP size: {modifiedData.Length} bytes");

            // Save the updated PDF document (persisting XMP changes)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}