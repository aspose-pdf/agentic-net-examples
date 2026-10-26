using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "OutputImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load PDF document (Document implements IDisposable, so we can use a using block)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // 72 DPI keeps the original page size (1 point = 1 pixel at 72 DPI)
            var resolution = new Resolution(72);

            // JpegDevice does NOT implement IDisposable – instantiate it directly
            var jpegDevice = new JpegDevice(resolution, 100); // 100 = max quality

            // Iterate through all pages
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputDir, $"Page_{pageNumber}.jpg");

                // Only the stream needs to be disposed
                using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    jpegDevice.Process(pdfDoc.Pages[pageNumber], outStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as JPEG to '{outputPath}'.");
            }
        }
    }
}
