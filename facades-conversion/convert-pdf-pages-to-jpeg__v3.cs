using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using System.Drawing.Imaging; // optional, kept for completeness

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "Images";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (Document implements IDisposable, so we can use a using statement)
        using var pdfDocument = new Document(pdfPath);

        // Define the resolution (dots per inch) for the output images
        var resolution = new Resolution(150);

        // JpegDevice does NOT implement IDisposable – instantiate it directly
        var jpegDevice = new JpegDevice(resolution, 90); // 90% quality

        // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
        for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            // Build a sequential file name, e.g., page_001.jpg
            string outPath = Path.Combine(outputDir, $"page_{pageNumber:D3}.jpg");

            // Convert the current page to JPEG and write directly to a file stream
            using var outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write);
            jpegDevice.Process(pdfDocument.Pages[pageNumber], outStream);

            Console.WriteLine($"Saved {outPath}");
        }
    }
}
