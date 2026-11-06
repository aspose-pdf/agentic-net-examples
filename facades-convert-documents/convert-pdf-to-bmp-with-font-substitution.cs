using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text; // for SimpleFontSubstitution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "BmpPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document.
            Document pdfDoc = new Document(inputPdf);

            // Configure font substitution for any missing fonts using FontRepository.
            // Example substitution: replace unknown fonts with Arial.
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("MissingFont", "Arial"));

            // Create a BMP device (devices are not IDisposable, so no using block).
            BmpDevice bmpDevice = new BmpDevice(); // default resolution; customize if needed

            // Convert each page to a BMP image.
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");
                bmpDevice.Process(pdfDoc.Pages[pageNumber], outPath);
                Console.WriteLine($"Saved {outPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
