using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

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

        // Load the PDF document.
        using (Document pdfDoc = new Document(inputPdf))
        {
            const int resolutionDpi = 200; // Desired DPI for balanced quality.
            Resolution resolution = new Resolution(resolutionDpi);

            // Iterate through each page and convert it to a BMP image.
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.bmp");

                using (FileStream imageStream = new FileStream(outPath, FileMode.Create))
                {
                    // BmpDevice now receives a Resolution object instead of an int.
                    BmpDevice bmpDevice = new BmpDevice(resolution);
                    bmpDevice.Process(pdfDoc.Pages[pageNumber], imageStream);
                }
            }
        }

        Console.WriteLine($"PDF pages have been saved as BMP images in '{outputDir}'.");
    }
}
