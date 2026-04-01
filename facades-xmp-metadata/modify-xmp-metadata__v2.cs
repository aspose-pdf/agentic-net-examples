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
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        // PdfXmpMetadata is part of Aspose.Pdf.Facades. No additional XMP namespace is required.
        using (PdfXmpMetadata xmpMetadata = new PdfXmpMetadata())
        {
            // Bind the existing PDF document
            xmpMetadata.BindPdf(inputPath);

            // Update a standard XMP property (Title)
            xmpMetadata.Add("Title", "New Document Title");

            // Add a custom XMP field
            xmpMetadata.Add("CustomProperty", "CustomValue");

            // Save the PDF with the modified XMP metadata
            xmpMetadata.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated XMP metadata to '{outputPath}'.");
    }
}
