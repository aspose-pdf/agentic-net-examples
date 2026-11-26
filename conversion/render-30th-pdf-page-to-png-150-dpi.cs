using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "page30.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Verify the document contains at least 30 pages (Aspose.Pdf uses 1‑based indexing)
            if (pdfDoc.Pages.Count < 30)
            {
                Console.Error.WriteLine("The document has fewer than 30 pages.");
                return;
            }

            // Retrieve the 30th page
            Page page = pdfDoc.Pages[30];

            // Create a PNG device with a resolution of 150 DPI.
            // Devices are concrete but do NOT implement IDisposable, so do NOT wrap them in a using statement.
            PngDevice pngDevice = new PngDevice(new Resolution(150));

            // Render the page to a PNG file – dispose only the stream.
            using (FileStream outStream = new FileStream(outputPath, FileMode.Create))
            {
                pngDevice.Process(page, outStream);
            }
        }

        Console.WriteLine($"Page 30 rendered to PNG at 150 DPI: {outputPath}");
    }
}
