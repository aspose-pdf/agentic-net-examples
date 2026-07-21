using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfXmpMetadata facade to manipulate XMP metadata
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Load the PDF document into the facade
            xmp.BindPdf(inputPath);

            // Remove the Nickname element from XMP metadata
            // Two overloads are available; using the enum version is type‑safe
            xmp.Remove(DefaultMetadataProperties.Nickname);

            // Save the modified PDF back to disk
            xmp.Save(outputPath);
        }

        Console.WriteLine($"Nickname element removed. Output saved to '{outputPath}'.");
    }
}