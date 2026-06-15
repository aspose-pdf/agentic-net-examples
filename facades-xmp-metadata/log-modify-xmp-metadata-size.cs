using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Bind XMP metadata facade to the document
            using (PdfXmpMetadata xmp = new PdfXmpMetadata(doc))
            {
                // Retrieve current XMP metadata
                byte[] originalData = xmp.GetXmpMetadata();
                Console.WriteLine($"Original XMP size: {originalData.Length} bytes");

                // Modify XMP metadata (example: add creator)
                xmp.Add("dc:creator", "John Doe");

                // Retrieve modified XMP metadata
                byte[] modifiedData = xmp.GetXmpMetadata();
                Console.WriteLine($"Modified XMP size: {modifiedData.Length} bytes");

                // Save the PDF with updated XMP metadata
                xmp.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved with updated XMP metadata to '{outputPath}'.");
    }
}