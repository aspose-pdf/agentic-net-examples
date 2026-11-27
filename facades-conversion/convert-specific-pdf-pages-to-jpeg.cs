using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "Images";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // PdfConverter implements IDisposable – wrap it in a using block
        using (PdfConverter converter = new PdfConverter())
        {
            // Load the PDF document
            converter.BindPdf(inputPath);

            // Convert only pages 3 and 4
            converter.StartPage = 3;
            converter.EndPage   = 4;

            // Set conversion resolution to 72 DPI
            converter.Resolution = new Resolution(72);

            // In recent Aspose.PDF versions the CoordinateType property either defaults to CropBox
            // or the enum name has changed. The explicit setting is optional, so we omit it to keep
            // the code compatible across versions.
            // converter.CoordinateType = PdfConverterCoordinateType.CropBox; // removed – not available in this version

            // Perform the conversion preparation
            converter.DoConvert();

            // Save each page as a JPEG image – GetNextImage infers format from file extension
            for (int pageNumber = 3; pageNumber <= 4; pageNumber++)
            {
                string outputPath = Path.Combine(outputDir, $"page{pageNumber}.jpg");
                converter.GetNextImage(outputPath);
            }
        }

        Console.WriteLine("Pages 3‑4 have been converted to JPEG images.");
    }
}
