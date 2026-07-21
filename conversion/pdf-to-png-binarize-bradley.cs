using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // PngDevice and TiffDevice

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "output_images";
        const double threshold = 0.5; // value between 0.0 and 1.0

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Load PDF document (lifecycle rule)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // PNG device for initial conversion (save rule)
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution);

            // TiffDevice provides the Bradley binarization method
            TiffDevice tiffDevice = new TiffDevice();

            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Convert page to PNG in memory
                using (MemoryStream pngStream = new MemoryStream())
                {
                    pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                    pngStream.Position = 0;

                    // Apply Bradley binarization (method from TiffDevice)
                    using (MemoryStream binarizedStream = new MemoryStream())
                    {
                        tiffDevice.BinarizeBradley(pngStream, binarizedStream, threshold);
                        binarizedStream.Position = 0;

                        // Save the binarized PNG to disk
                        string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");
                        using (FileStream fileOut = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                        {
                            binarizedStream.CopyTo(fileOut);
                        }

                        Console.WriteLine($"Binarized PNG saved: {outputPath}");
                    }
                }
            }
        }
    }
}