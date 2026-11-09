using System;
using System.IO;
using Aspose.Pdf.Facades; // Use Facades namespace for XMP metadata

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

        // Bind the PDF file to the XMP metadata editor (PdfXmpMetadata lives in Facades)
        PdfXmpMetadata xmp = new PdfXmpMetadata();
        xmp.BindPdf(inputPath);

        // Set XMP metadata fields using the proper Dublin Core schema keys
        xmp.Add("dc:creator", "John Doe");   // Author
        xmp.Add("dc:title", "Project Plan"); // Title

        // Save the PDF with updated metadata and release resources
        xmp.Save(outputPath);
        xmp.Close();

        Console.WriteLine($"XMP metadata updated and saved to '{outputPath}'.");
    }
}
