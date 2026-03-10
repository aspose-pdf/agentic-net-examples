using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory containing the source PDF and where EMF files will be written
        string dataDir = @"YOUR_DATA_DIRECTORY";
        // Name of the PDF file to process
        string pdfFile = "YOUR_PDF_FILE";

        // Build full path to the input PDF
        string inputPath = Path.Combine(dataDir, pdfFile);
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(dataDir);

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document pdfDocument = new Document(inputPath))
        {
            // Create a resolution object for the EMF output (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);
            // Initialize the EMF device with the desired resolution
            EmfDevice emfDevice = new EmfDevice(resolution);

            // Pages are 1‑based (page indexing rule)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Define the output EMF file name for the current page
                string outputFile = Path.Combine(dataDir, $"image{pageNumber}_out.emf");

                // Create a file stream for the EMF output (lifecycle rule: wrap in using)
                using (FileStream emfStream = new FileStream(outputFile, FileMode.Create))
                {
                    // Convert the specific page to EMF and write it to the stream
                    emfDevice.Process(pdfDocument.Pages[pageNumber], emfStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as EMF to '{outputFile}'.");
            }
        }
    }
}