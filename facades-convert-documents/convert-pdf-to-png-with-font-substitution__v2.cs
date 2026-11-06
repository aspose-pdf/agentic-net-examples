using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text; // Required for SimpleFontSubstitution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "OutputImages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Register simple font substitutions for any fonts that might be missing.
        // Substitute common missing fonts with Arial (adjust as needed).
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Times New Roman", "Arial"));
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Arial"));

        // Create a PNG device with the desired resolution (e.g., 300 DPI)
        var pngDevice = new PngDevice(new Resolution(300));

        // Iterate through all pages (1‑based indexing)
        for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            string outPath = Path.Combine(outputDir, $"page_{pageNumber}.png");
            // Convert the current page to PNG and write directly to file
            pngDevice.Process(pdfDocument.Pages[pageNumber], outPath);
            Console.WriteLine($"Saved page {pageNumber} as PNG to {outPath}");
        }
    }
}