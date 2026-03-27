using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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
            using (PdfConverter converter = new PdfConverter(doc))
            {
                // Use default CropBox coordinates (default CoordinateType)
                converter.StartPage = 3; // first page to convert (1‑based)
                converter.EndPage = 8;   // last page to convert
                // Default resolution (150 DPI) is used automatically
                converter.DoConvert();
                converter.SaveAsTIFF(outputPath);
            }
        }

        Console.WriteLine($"Pages 3‑8 have been saved as TIFF to '{outputPath}'.");
    }
}
