using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string nickname   = "MyCustomIdentifier";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the XMP metadata facade and bind it to the source PDF
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPath);

            // Add or update the xmp:Nickname property
            xmp.Add("xmp:Nickname", nickname);

            // Save the PDF with the updated XMP metadata
            xmp.Save(outputPath);
        }

        Console.WriteLine($"Nickname '{nickname}' has been set and saved to '{outputPath}'.");
    }
}