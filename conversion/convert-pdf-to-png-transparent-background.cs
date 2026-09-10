using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class PdfToPngConverter
{
    static void Main()
    {
        // Base directory – the folder where the executable resides.
        // This avoids hard‑coded placeholders and works regardless of where the program is run.
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Name of the PDF file that must be present in the base directory.
        // Change this value to match your actual file name.
        const string pdfFileName = "sample.pdf";

        string pdfPath = Path.Combine(baseDir, pdfFileName);
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Create an output folder ("output") next to the executable.
        string outputDir = Path.Combine(baseDir, "output");
        Directory.CreateDirectory(outputDir);

        // Load the PDF document – the using statement guarantees proper disposal.
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Desired resolution (e.g., 300 DPI).
            Resolution resolution = new Resolution(300);

            // PNG device with transparent background enabled.
            PngDevice pngDevice = new PngDevice(resolution)
            {
                TransparentBackground = true
            };

            // Iterate through all pages (1‑based indexing).
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputDir, $"image{pageNumber}_out.png");
                using (FileStream pngStream = new FileStream(outputPath, FileMode.Create))
                {
                    pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                }
            }
        }

        Console.WriteLine("PDF conversion to PNG with transparent background completed.");
    }
}
