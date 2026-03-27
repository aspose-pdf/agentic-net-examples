using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.tiff";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            PdfConverter converter = new PdfConverter(doc);
            converter.StartPage = 4; // start from page 4 (1‑based indexing)
            converter.EndPage = 9;   // end at page 9
            // Set DPI using a Resolution object (correct type)
            converter.Resolution = new Resolution(150);
            // Perform the conversion before saving
            converter.DoConvert();
            converter.SaveAsTIFF(outputPath);
            converter.Close();
        }

        Console.WriteLine($"TIFF image saved to '{outputPath}'.");
    }
}