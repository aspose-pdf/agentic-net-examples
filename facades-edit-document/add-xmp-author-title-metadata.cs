using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfXmpMetadata facade to add XMP metadata
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the existing PDF
            xmp.BindPdf(inputPath);

            // Add author and title metadata (dc namespace)
            xmp.Add("dc:creator", "John Doe");
            xmp.Add("dc:title",   "Project Plan");

            // Save the PDF with updated XMP metadata
            xmp.Save(outputPath);
        }

        Console.WriteLine($"XMP metadata added and saved to '{outputPath}'.");
    }
}