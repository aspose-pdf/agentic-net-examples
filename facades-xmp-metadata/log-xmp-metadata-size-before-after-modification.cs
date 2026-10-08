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

        try
        {
            // Load the PDF document
            Document doc = new Document(inputPath);

            // Use the PdfXmpMetadata facade to work with XMP metadata
            using (PdfXmpMetadata xmp = new PdfXmpMetadata(doc))
            {
                // Log size before modification
                byte[] xmpBefore = xmp.GetXmpMetadata();
                int sizeBefore = xmpBefore?.Length ?? 0;
                Console.WriteLine($"XMP size before modification: {sizeBefore} bytes");

                // Register a custom namespace and add a custom property
                const string customNsPrefix = "custom";
                const string customNsUri = "http://example.com/custom";
                xmp.RegisterNamespaceURI(customNsPrefix, customNsUri);
                xmp.Add($"{customNsPrefix}:Modified", "true");

                // Log size after modification
                byte[] xmpAfter = xmp.GetXmpMetadata();
                int sizeAfter = xmpAfter?.Length ?? 0;
                Console.WriteLine($"XMP size after modification: {sizeAfter} bytes");

                // Save the PDF with the updated XMP block
                xmp.Save(outputPath);
            }

            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
