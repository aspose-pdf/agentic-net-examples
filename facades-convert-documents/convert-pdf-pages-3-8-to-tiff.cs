using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF
        const string outputDir = "TiffPages";        // folder for TIFF files

        // Verify that the input file exists before proceeding
        if (!File.Exists(inputPdf))
        {
            Console.WriteLine($"Input file '{inputPdf}' not found.");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Define the page range (1‑based indexing). Guard against PDFs with fewer pages.
            int startPage = 3;
            int endPage   = Math.Min(8, pdfDoc.Pages.Count);

            // TiffDevice does NOT implement IDisposable – instantiate it once and reuse.
            TiffDevice tiffDevice = new TiffDevice();

            for (int pageNumber = startPage; pageNumber <= endPage; pageNumber++)
            {
                string tiffPath = Path.Combine(outputDir, $"Page_{pageNumber}.tiff");

                // Use a FileStream so we can control the lifetime of the underlying stream.
                using (FileStream tiffStream = new FileStream(tiffPath, FileMode.Create, FileAccess.Write))
                {
                    tiffDevice.Process(pdfDoc.Pages[pageNumber], tiffStream);
                }
            }
        }

        Console.WriteLine("Pages 3‑8 have been converted to TIFF images.");
    }
}
