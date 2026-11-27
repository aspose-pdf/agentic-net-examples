using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputFolder = "PngPages";
        const string customFontPath = "MyFont.ttf"; // path to a custom TrueType font

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Register custom font if it exists (required before loading the document)
            if (File.Exists(customFontPath))
            {
                // In recent Aspose.Pdf versions fonts are added via substitution collection
                // The wildcard "*" substitutes any missing font with the provided TrueType file.
                FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", customFontPath));
            }

            // Load the PDF document
            Document pdfDocument = new Document(pdfPath);

            // Define the page range and resolution
            const int startPage = 2; // 1‑based index
            const int endPage = 5;
            const int dpi = 150;

            // Convert each page in the range to a PNG image
            for (int pageNumber = startPage; pageNumber <= endPage && pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outPath = Path.Combine(outputFolder, $"Page_{pageNumber}.png");

                // PngDevice handles PNG conversion and accepts a Resolution struct
                using (var imageStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    var pngDevice = new PngDevice(new Resolution(dpi));
                    // CompressionLevel property was removed in newer versions; default compression is used.
                    pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as PNG to '{outPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }

        Console.WriteLine("Conversion completed.");
    }
}
