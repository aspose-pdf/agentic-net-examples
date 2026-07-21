using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory containing the PDF file
        const string dataDir = @"YOUR_DATA_DIRECTORY";
        // PDF file name (include extension)
        const string pdfFile = "YOUR_PDF_FILE.pdf";

        string pdfPath = Path.Combine(dataDir, pdfFile);
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Desired resolution for the EMF output (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Create an EMF device with the specified resolution
            EmfDevice emfDevice = new EmfDevice(resolution);

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build the output EMF file path
                string emfPath = Path.Combine(dataDir, $"image{pageNumber}_out.emf");

                // Create a file stream for the EMF output
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