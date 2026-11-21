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
        const string newCreator = "Acme Corp";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Bind the XMP metadata facade to the document
            using (Aspose.Pdf.Facades.PdfXmpMetadata xmp = new Aspose.Pdf.Facades.PdfXmpMetadata(doc))
            {
                // Modify (or add) the creator field in the XMP packet
                // The standard XMP property for creator is "xmp:Creator"
                xmp.Add("xmp:Creator", newCreator);

                // Save the PDF with the updated XMP metadata
                xmp.Save(outputPath);
            }
        }

        Console.WriteLine($"Creator field updated and saved to '{outputPath}'.");
    }
}