using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Directory containing the PDF and where BMP files will be written
        string dataDir = @"C:\Data\";
        string pdfFileName = "input.pdf";

        string pdfPath = Path.Combine(dataDir, pdfFileName);
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Register a substitution for any missing font (replace with Arial)
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("MissingFont", "Arial"));

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Define the resolution for the BMP images (300 DPI)
            Resolution resolution = new Resolution(300);
            BmpDevice bmpDevice = new BmpDevice(resolution);

            // Iterate over all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string bmpPath = Path.Combine(dataDir, $"page_{pageNumber}.bmp");
                using (FileStream bmpStream = new FileStream(bmpPath, FileMode.Create))
                {
                    // Convert the current page to BMP and write to the stream
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpStream);
                }
                Console.WriteLine($"Saved BMP image: {bmpPath}");
            }
        }
    }
}