using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory containing source PDF files
        string inputDir = @"C:\PdfInput";
        // Directory where resulting TIFF files will be saved
        string outputDir = @"C:\TiffOutput";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Get all PDF files in the input directory
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to convert.");
            return;
        }

        // Prepare TiffDevice with default (no) compression
        Resolution resolution = new Resolution(300);
        TiffSettings tiffSettings = new TiffSettings
        {
            Compression = CompressionType.None,
            Depth = ColorDepth.Default,
            Shape = ShapeType.Landscape,
            SkipBlankPages = false
        };
        TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

        foreach (string pdfPath in pdfFiles)
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string tiffPath = Path.Combine(outputDir, fileNameWithoutExt + ".tif");

            // Load PDF document
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Convert the whole PDF to a multi‑page TIFF archive
                tiffDevice.Process(pdfDocument, tiffPath);
            }

            Console.WriteLine($"Converted: {pdfPath} → {tiffPath}");
        }
    }
}