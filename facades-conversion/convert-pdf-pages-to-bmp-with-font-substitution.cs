using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF and output folder
        const string inputPath = "input.pdf";
        const string outputDir = "BmpPages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // -------------------------------------------------
        // Configure font substitution (Courier → LiberationMono)
        // -------------------------------------------------
        FontRepository.Substitutions.Add(
            new SimpleFontSubstitution("Courier", "LiberationMono")
        );

        // -------------------------------------------------
        // Initialize PdfConverter and bind the PDF
        // -------------------------------------------------
        var converter = new PdfConverter();
        converter.BindPdf(inputPath);

        // Set page range (1 to 4) and resolution (300 DPI)
        converter.StartPage = 1;
        converter.EndPage   = 4;
        converter.Resolution = new Resolution(300);

        // Prepare the conversion
        converter.DoConvert();

        // -------------------------------------------------
        // Convert each page to BMP and save to disk
        // -------------------------------------------------
        for (int page = converter.StartPage; page <= converter.EndPage; page++)
        {
            string outPath = Path.Combine(outputDir, $"Page_{page}.bmp");
            // GetNextImage saves the current page using the file extension to determine format
            converter.GetNextImage(outPath);
        }

        // Clean up the converter
        converter.Close();

        Console.WriteLine("PDF pages 1‑4 have been converted to BMP images.");
    }
}
