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

        // Load the PDF, add custom XMP metadata, and save.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the existing PDF file.
            xmp.BindPdf(inputPath);

            // Add a custom XMP field named "ProjectID" with value "12345".
            xmp.Add("ProjectID", "12345");

            // Save the PDF with the updated XMP metadata.
            xmp.Save(outputPath);
        }

        Console.WriteLine($"Custom XMP metadata added. Saved to '{outputPath}'.");
    }
}