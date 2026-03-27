using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPattern = "page_{0}.bmp";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("File not found: " + inputPath);
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPath))
        {
            // Apply font substitution for any missing fonts (example substitutes with Arial)
            // Use FontRepository.Substitutions with SimpleFontSubstitution instead of the non‑existent Document.FontSubstitutions property
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("MissingFont", "Arial"));

            // Create a BmpDevice with desired resolution
            Resolution resolution = new Resolution(300);
            BmpDevice bmpDevice = new BmpDevice(resolution);

            // Convert each page to a BMP image
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputPath = string.Format(outputPattern, pageNumber);
                using (FileStream bmpStream = new FileStream(outputPath, FileMode.Create))
                {
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpStream);
                }
            }
        }

        Console.WriteLine("PDF pages have been converted to BMP images.");
    }
}
