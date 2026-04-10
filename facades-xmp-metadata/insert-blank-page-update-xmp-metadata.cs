using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Insert a new blank page at the end of the document
            doc.Pages.Add();

            // Bind the XMP metadata facade to the same document instance
            Aspose.Pdf.Facades.PdfXmpMetadata xmp = new Aspose.Pdf.Facades.PdfXmpMetadata(doc);

            // Update standard XMP properties (using string keys)
            xmp.Add("dc:title",   "Updated Document Title");
            xmp.Add("dc:creator", "John Doe");
            xmp.Add("pdf:Producer", "Aspose.Pdf");

            // Save the document together with the updated XMP metadata
            xmp.Save(outputPath);
        }

        Console.WriteLine($"PDF with inserted page and updated XMP saved to '{outputPath}'.");
    }
}