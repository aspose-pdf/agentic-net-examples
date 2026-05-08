using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfXmpMetadata facade
using Aspose.Pdf;           // DefaultMetadataProperties enum

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_nickname.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the XMP metadata facade
        PdfXmpMetadata xmp = new PdfXmpMetadata();

        // Load (bind) the PDF document into the facade
        xmp.BindPdf(inputPath);

        // Remove the Nickname element from the XMP metadata
        xmp.Remove(DefaultMetadataProperties.Nickname);
        // Alternative using string key:
        // xmp.Remove("xmp:Nickname");

        // Save the modified PDF back to disk
        xmp.Save(outputPath);

        // Release resources held by the facade
        xmp.Close();

        Console.WriteLine($"Nickname element removed. Output saved to '{outputPath}'.");
    }
}