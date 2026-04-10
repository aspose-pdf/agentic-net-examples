using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory that contains the source PDF and where EMF files will be written
        const string dataDir = @"YOUR_DATA_DIRECTORY";
        // Name of the PDF file to convert
        const string pdfFile = "YOUR_PDF_FILE.pdf";

        string pdfPath = Path.Combine(dataDir, pdfFile);
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Define the resolution (dots per inch) for the raster part of the EMF
            Resolution resolution = new Resolution(300);

            // Create an EMF device with the specified resolution
            EmfDevice emfDevice = new EmfDevice(resolution);

            // Iterate over all pages (Aspose.Pdf uses 1‑based page indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build the output file name for the current page
                string emfPath = Path.Combine(dataDir, $"image{pageNumber}_out.emf");

                // Open a file stream for the EMF output
                using (FileStream emfStream = new FileStream(emfPath, FileMode.Create))
                {
                    // Convert the current page to EMF and write it to the stream
                    emfDevice.Process(pdfDocument.Pages[pageNumber], emfStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as EMF: {emfPath}");
            }
        }
    }
}