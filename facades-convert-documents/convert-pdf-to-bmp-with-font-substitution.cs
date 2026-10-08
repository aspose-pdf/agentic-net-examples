using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text; // for SimpleFontSubstitution

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "BmpPages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        Document pdfDocument = new Document(pdfPath);

        // Enable font substitution for missing fonts using FontRepository
        // Substitute common missing fonts with Arial (adjust as needed)
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Times New Roman", "Arial"));
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Arial"));
        // Add more substitutions if required

        // Define the resolution for the bitmap images (150 DPI – adjust as required)
        Resolution resolution = new Resolution(150);

        // Convert each page to a BMP image
        for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.bmp");

            // BmpDevice does not implement IDisposable, so do not use a using block
            BmpDevice bmpDevice = new BmpDevice(resolution);
            bmpDevice.Process(pdfDocument.Pages[pageNumber], outPath);
        }

        Console.WriteLine($"PDF successfully converted to BMP images in '{outputDir}'.");
    }
}
