using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // required for JpegDevice

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "output_images";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        using (Document pdfDoc = new Document(inputPath))
        {
            // JpegDevice provides image conversion functionality. It uses default
            // resolution and quality when no values are set, which satisfies the
            // "default quality" requirement.
            var jpegDevice = new JpegDevice();

            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNum}.jpeg");
                // Process a single page and save it as a JPEG file.
                jpegDevice.Process(pdfDoc.Pages[pageNum], outPath);
            }
        }

        Console.WriteLine("PDF pages converted to JPEG images.");
    }
}