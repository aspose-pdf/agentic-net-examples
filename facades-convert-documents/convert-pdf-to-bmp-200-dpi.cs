using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main(string[] args)
    {
        // Directory containing the PDF file and where BMP images will be saved
        const string dataDir = @"C:\Data\";
        const string pdfFileName = "sample.pdf";

        // Full path to the input PDF
        string pdfPath = Path.Combine(dataDir, pdfFileName);

        // Verify that the PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Create a Resolution object with 200 DPI – note the correct namespace Aspose.Pdf.Devices
            Aspose.Pdf.Devices.Resolution resolution = new Aspose.Pdf.Devices.Resolution(200);

            // Initialize the BmpDevice with the specified resolution.
            // The default CoordinateType is CropBox, which satisfies the requirement.
            BmpDevice bmpDevice = new BmpDevice(resolution);

            // Iterate over all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Define the output BMP file name for the current page
                string bmpPath = Path.Combine(dataDir, $"page_{pageNumber}.bmp");

                // Convert the page to BMP and write it to the file stream
                using (FileStream bmpStream = new FileStream(bmpPath, FileMode.Create))
                {
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as BMP: {bmpPath}");
            }
        }

        Console.WriteLine("PDF to BMP conversion completed.");
    }
}
