using System;
using System.IO;
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

        // Load the PDF and manipulate its XMP metadata
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPath);

            // Add author and title using standard XMP keys
            xmp.Add("dc:creator", "John Doe");
            xmp.Add("dc:title", "Project Plan");

            // Save the PDF with the updated XMP metadata
            xmp.Save(outputPath);
        }

        Console.WriteLine($"XMP metadata added. Saved to '{outputPath}'.");
    }
}