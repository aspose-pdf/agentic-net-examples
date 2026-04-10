using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string newCreator = "My New Creator";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the XMP metadata facade
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPath);

            // Modify the creator field (Dublin Core creator)
            xmp["dc:creator"] = newCreator;

            // Save the PDF with the updated XMP metadata
            xmp.Save(outputPath);
        }

        Console.WriteLine($"PDF with updated XMP creator saved to '{outputPath}'.");
    }
}