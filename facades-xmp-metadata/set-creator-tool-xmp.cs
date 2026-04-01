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
        const string appVersion = "MyApp 1.0.0";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize XMP metadata facade for the document
            PdfXmpMetadata xmp = new PdfXmpMetadata(doc);

            // Create XMP value for CreatorTool
            XmpValue creatorToolValue = new XmpValue(appVersion);

            // Add or update the CreatorTool property
            xmp.Add(DefaultMetadataProperties.CreatorTool, creatorToolValue);

            // Save the PDF with updated metadata
            xmp.Save(outputPath);

            Console.WriteLine($"CreatorTool set to '{appVersion}' and saved to '{outputPath}'.");
        }
    }
}