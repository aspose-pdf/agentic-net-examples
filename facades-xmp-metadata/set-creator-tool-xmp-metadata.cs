using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Get current application version (e.g., "1.2.3.0")
        string appVersion = Assembly.GetExecutingAssembly()
                                    .GetName()
                                    .Version
                                    .ToString();

        // Open the PDF document within a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Bind XMP metadata facade to the opened document
            PdfXmpMetadata xmp = new PdfXmpMetadata(doc);

            // Set the CreatorTool property in XMP metadata
            // DefaultMetadataProperties.CreatorTool corresponds to xmp:CreatorTool
            xmp.Add(DefaultMetadataProperties.CreatorTool, new XmpValue($"MyApp v{appVersion}"));

            // Save the PDF with updated XMP metadata
            xmp.Save(outputPath);
        }

        Console.WriteLine($"CreatorTool metadata set and saved to '{outputPath}'.");
    }
}