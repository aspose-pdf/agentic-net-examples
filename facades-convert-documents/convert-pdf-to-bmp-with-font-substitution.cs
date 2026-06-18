using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputFolder  = "BmpPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Register font substitution: Times New Roman → Calibri
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Times New Roman", "Calibri"));

        // Create a BmpDevice with desired resolution (e.g., 150 DPI)
        BmpDevice bmpDevice = new BmpDevice(new Resolution(150));

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Prepare output file path for each page
                string outputPath = Path.Combine(outputFolder, $"Page_{pageNumber}.bmp");

                // Convert the page to BMP and write to file
                using (FileStream imageStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                }

                Console.WriteLine($"Saved BMP image: {outputPath}");
            }
        }

        Console.WriteLine("PDF to BMP conversion completed.");
    }
}