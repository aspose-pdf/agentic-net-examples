using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text; // Font substitution classes

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "output_images";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Substitute missing Helvetica font with Arial
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Arial"));

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Set desired resolution for BMP output
            Resolution resolution = new Resolution(300);
            BmpDevice bmpDevice = new BmpDevice(resolution);

            // Convert pages 5 to 7 (1‑based indexing)
            for (int pageNum = 5; pageNum <= 7 && pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNum}.bmp");
                using (FileStream bmpStream = new FileStream(outPath, FileMode.Create))
                {
                    bmpDevice.Process(pdfDoc.Pages[pageNum], bmpStream);
                }
                Console.WriteLine($"Saved BMP for page {pageNum} to {outPath}");
            }
        }
    }
}