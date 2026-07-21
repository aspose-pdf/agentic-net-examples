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

        // Load the PDF into the XMP metadata facade, add the custom field, and save.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPath);               // Load the source PDF
            xmp.Add("Department", "Finance");      // Add custom XMP metadata
            xmp.Save(outputPath);                  // Save the PDF with updated XMP
        }

        Console.WriteLine($"XMP metadata 'Department: Finance' added and saved to '{outputPath}'.");
    }
}