using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Pages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // JpegDevice converts PDF pages to JPEG images
            // You can customize resolution and quality if needed
            JpegDevice jpegDevice = new JpegDevice();

            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpeg");

                // Convert the current page to a JPEG file using a FileStream
                using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    jpegDevice.Process(pdfDoc.Pages[pageNumber], fs);
                }

                Console.WriteLine($"Saved page {pageNumber} as {outputPath}");
            }
        }

        Console.WriteLine("PDF to JPEG conversion completed.");
    }
}
