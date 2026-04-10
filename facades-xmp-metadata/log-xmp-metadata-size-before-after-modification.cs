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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the XMP metadata facade for the loaded document
            PdfXmpMetadata xmp = new PdfXmpMetadata(doc);

            // Retrieve XMP metadata before any changes
            byte[] beforeData = xmp.GetXmpMetadata();
            int beforeSize = beforeData?.Length ?? 0;
            Console.WriteLine($"XMP size before modification: {beforeSize} bytes");

            // Example modification: add a creator entry
            xmp.Add("dc:creator", "AsposeUser");

            // Retrieve XMP metadata after the modification
            byte[] afterData = xmp.GetXmpMetadata();
            int afterSize = afterData?.Length ?? 0;
            Console.WriteLine($"XMP size after modification: {afterSize} bytes");

            // Save the updated PDF document
            xmp.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}