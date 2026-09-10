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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Bind the document to the XMP metadata facade
            using (PdfXmpMetadata xmp = new PdfXmpMetadata(doc))
            {
                // Retrieve the original XMP block as a byte array and log its size
                byte[] originalData = xmp.GetXmpMetadata();
                int originalSize = originalData?.Length ?? 0;
                Console.WriteLine($"Original XMP size: {originalSize} bytes");

                // Example modification: add a custom creator entry
                // The key follows the XMP namespace convention (e.g., "dc:creator")
                xmp.Add("dc:creator", "Aspose.Pdf Sample");

                // Retrieve the modified XMP block and log its new size
                byte[] modifiedData = xmp.GetXmpMetadata();
                int modifiedSize = modifiedData?.Length ?? 0;
                Console.WriteLine($"Modified XMP size: {modifiedSize} bytes");

                // Save the PDF with the updated XMP metadata
                xmp.Save(outputPath);
                Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
            }
        }
    }
}