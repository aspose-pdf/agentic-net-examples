using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "BmpPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // ------------------------------------------------------------
        // Font substitution: replace Helvetica with Arial
        // ------------------------------------------------------------
        // FontRepository works globally for the current AppDomain.
        // SimpleFontSubstitution maps a source font name to a target font.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Arial"));

        // Load the PDF document using the current Aspose.Pdf API (Document class)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Define the resolution for the bitmap output (150 DPI is a good default)
            Resolution resolution = new Resolution(150);

            // Convert pages 5‑7 to BMP files
            for (int pageNumber = 5; pageNumber <= 7; pageNumber++)
            {
                // Validate page number
                if (pageNumber > pdfDocument.Pages.Count)
                {
                    Console.WriteLine($"Page {pageNumber} does not exist in the document. Skipping.");
                    continue;
                }

                string outputPath = Path.Combine(outputFolder, $"Page_{pageNumber}.bmp");

                // BmpDevice writes a single page to a stream as a BMP image
                using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    BmpDevice bmpDevice = new BmpDevice(resolution);
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], outStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as BMP to '{outputPath}'.");
            }
        }

        Console.WriteLine("Conversion completed.");
    }
}
