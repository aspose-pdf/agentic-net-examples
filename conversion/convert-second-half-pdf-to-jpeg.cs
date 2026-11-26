using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputFolder = "OutputImages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            int totalPages = pdfDoc.Pages.Count;

            // Determine the first page of the second half (1‑based indexing)
            int startPage = totalPages / 2 + 1; // If odd, the middle page belongs to the first half

            // Create a JPEG device with 350 DPI resolution
            JpegDevice jpegDevice = new JpegDevice(350);

            // Iterate over the second half of the pages
            for (int pageNum = startPage; pageNum <= totalPages; pageNum++)
            {
                string outPath = Path.Combine(outputFolder, $"page_{pageNum}.jpg");

                // Save each page as a JPEG image
                using (FileStream imageStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    jpegDevice.Process(pdfDoc.Pages[pageNum], imageStream);
                }

                Console.WriteLine($"Saved page {pageNum} as JPEG to {outPath}");
            }
        }
    }
}