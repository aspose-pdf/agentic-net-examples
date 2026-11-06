using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF
        const string outputDir = "PreviewImages";    // folder for JPEGs

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (required for device‑based conversion)
        Document pdfDocument = new Document(inputPdf);

        // Define the preview range: pages 1 through 3 (or less if the PDF is shorter)
        int startPage = 1;
        int endPage   = Math.Min(3, pdfDocument.Pages.Count);

        // Configure JPEG conversion – resolution and quality
        Resolution resolution = new Resolution(150); // 150 DPI
        int jpegQuality = 80;                         // 0‑100 (higher = better quality)
        JpegDevice jpegDevice = new JpegDevice(resolution, jpegQuality);

        // Convert each page in the range to a JPEG image
        for (int pageNumber = startPage; pageNumber <= endPage; pageNumber++)
        {
            using (MemoryStream imageStream = new MemoryStream())
            {
                // Convert the specific page to JPEG and write to the stream
                jpegDevice.Process(pdfDocument.Pages[pageNumber], imageStream);

                // Save the stream content to a .jpeg file
                string outPath = Path.Combine(outputDir, $"page_{pageNumber}.jpeg");
                File.WriteAllBytes(outPath, imageStream.ToArray());
                Console.WriteLine($"Saved preview image: {outPath}");
            }
        }
    }
}
