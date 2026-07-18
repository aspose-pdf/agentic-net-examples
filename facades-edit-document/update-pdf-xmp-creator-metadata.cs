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

        // PdfXmpMetadata is a facade for XMP manipulation.
        // It implements IDisposable, so wrap it in a using block.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Load the existing PDF.
            xmp.BindPdf(inputPath);

            // Update the creator field in the XMP metadata.
            // The dictionary interface allows adding/replacing entries.
            // "dc:creator" is the standard XMP key for the creator.
            xmp.Add("dc:creator", newCreator);

            // Save the PDF with the modified metadata.
            xmp.Save(outputPath);
        }

        Console.WriteLine($"PDF with updated creator saved to '{outputPath}'.");
    }
}