using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory containing the PDF file
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // PDF file name (include extension)
        string pdfFile = "YOUR_PDF_FILE.pdf";

        // Full path to the source PDF
        string inputPath = Path.Combine(dataDir, pdfFile);

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPath))
        {
            // Define the resolution (DPI) for the EMF output
            Resolution resolution = new Resolution(300);

            // Create an EMF device with the specified resolution
            EmfDevice emfDevice = new EmfDevice(resolution);

            // Iterate through all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Output EMF file path for the current page
                string outPath = Path.Combine(dataDir, $"image{pageNumber}_out.emf");

                // Write the EMF image to a file stream
                using (FileStream emfStream = new FileStream(outPath, FileMode.Create))
                {
                    emfDevice.Process(pdfDocument.Pages[pageNumber], emfStream);
                }
            }
        }

        Console.WriteLine("PDF successfully converted to EMF images.");
    }
}