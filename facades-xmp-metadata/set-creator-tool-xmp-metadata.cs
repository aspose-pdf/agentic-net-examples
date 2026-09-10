using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Example version string – replace with your actual application version if needed
        string appVersion = "MyApp 1.0";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, modify its XMP metadata, and save the result
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the existing PDF file
            xmp.BindPdf(inputPath);

            // Set the xmp:CreatorTool property
            xmp.Add(DefaultMetadataProperties.CreatorTool, new XmpValue(appVersion));

            // Save the updated PDF (overwrites or creates a new file)
            xmp.Save(outputPath);
        }

        Console.WriteLine($"CreatorTool set to \"{appVersion}\" and saved to \"{outputPath}\".");
    }
}