using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_nickname.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF into the XMP metadata facade, remove the Nickname element, and save.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPath);
            // Remove the Nickname element using the predefined enum key.
            xmp.Remove(DefaultMetadataProperties.Nickname);
            // Save the updated PDF.
            xmp.Save(outputPath);
        }

        Console.WriteLine($"Nickname removed. Saved to '{outputPath}'.");
    }
}