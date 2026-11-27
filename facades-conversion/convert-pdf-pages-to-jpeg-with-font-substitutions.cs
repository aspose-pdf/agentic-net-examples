using System;
using System.IO;
using Aspose.Pdf;                     // Document class
using Aspose.Pdf.Text;                // FontRepository & SimpleFontSubstitution
using Aspose.Pdf.Devices;             // JpegDevice and Resolution

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";
        const string outputDir = "Images";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Register font substitutions (missing font name -> substitute font name)
        // SimpleFontSubstitution replaces a missing font with an existing one.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("MissingFont1", "Arial"));
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("MissingFont2", "Times New Roman"));
        // Add more substitutions as needed:
        // FontRepository.Substitutions.Add(new SimpleFontSubstitution("MissingFont3", "Calibri"));

        // Load the PDF document
        Document pdfDocument = new Document(pdfPath);

        // Convert pages 1‑3 to JPEG images using JpegDevice (cross‑platform, no System.Drawing)
        for (int pageNumber = 1; pageNumber <= 3 && pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.jpg");
            using (FileStream imageStream = new FileStream(outPath, FileMode.Create))
            {
                // Set desired resolution (e.g., 150 DPI)
                JpegDevice jpegDevice = new JpegDevice(new Resolution(150));
                jpegDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
            }

            Console.WriteLine($"Saved page {pageNumber} as JPEG to '{outPath}'.");
        }
    }
}
