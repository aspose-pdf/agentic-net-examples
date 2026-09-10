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

        // Initialize the XMP metadata facade, bind the source PDF,
        // add author and title fields, and save the result.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPath);                     // Load the PDF
            xmp.Add("dc:creator", "John Doe");          // Author field
            xmp.Add("dc:title", "Project Plan");        // Title field
            xmp.Save(outputPath);                       // Write updated PDF
        }

        Console.WriteLine($"XMP metadata added and saved to '{outputPath}'.");
    }
}