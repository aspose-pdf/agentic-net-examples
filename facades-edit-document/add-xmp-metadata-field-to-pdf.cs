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

        // Create XMP metadata facade, bind the source PDF, add the custom field, and save.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPath);               // Load the PDF
            xmp.Add("Department", "Finance");     // Add XMP field Department = Finance
            xmp.Save(outputPath);                 // Persist changes to a new file
        }

        Console.WriteLine($"XMP metadata added. Output saved to '{outputPath}'.");
    }
}