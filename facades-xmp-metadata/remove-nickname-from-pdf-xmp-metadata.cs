using System;
using System.IO;
using Aspose.Pdf;               // for DefaultMetadataProperties enum
using Aspose.Pdf.Facades;      // for PdfXmpMetadata

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the XMP metadata facade, bind it to the PDF,
        // remove the Nickname element, and save the updated PDF.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Load the PDF into the facade
            xmp.BindPdf(inputPath);

            // Remove the Nickname element using the enum overload
            xmp.Remove(DefaultMetadataProperties.Nickname);

            // Persist the changes to a new file
            xmp.Save(outputPath);
        }

        Console.WriteLine($"Nickname element removed. Output saved to '{outputPath}'.");
    }
}