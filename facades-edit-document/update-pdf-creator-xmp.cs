using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string newCreator = "New Creator";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the existing PDF file
            xmp.BindPdf(inputPath);

            // Modify the creator field in XMP metadata
            xmp["dc:creator"] = newCreator;

            // Save the PDF with updated metadata
            xmp.Save(outputPath);
        }

        Console.WriteLine($"Creator updated and saved to '{outputPath}'.");
    }
}
