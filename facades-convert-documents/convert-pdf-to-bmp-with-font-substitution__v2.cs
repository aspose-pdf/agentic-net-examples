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
        const string outputDir = "BmpPages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF and register custom font substitution (Times New Roman → Calibri)
        using (Document doc = new Document(inputPath))
        {
            // Register substitution – both fonts must be installed or available to Aspose.Pdf
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("Times New Roman", "Calibri"));

            // Create a concrete BMP device (devices are not IDisposable, so no using block)
            BmpDevice bmpDevice = new BmpDevice(); // you can set resolution, e.g. new BmpDevice(new Resolution(150));

            // Convert each page to a BMP image (pages are 1‑based)
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");
                using (FileStream bmpStream = new FileStream(outPath, FileMode.Create))
                {
                    // The device processes the page and writes the image to the stream
                    bmpDevice.Process(doc.Pages[pageNumber], bmpStream);
                }
            }
        }

        Console.WriteLine("PDF has been successfully converted to BMP images.");
    }
}
