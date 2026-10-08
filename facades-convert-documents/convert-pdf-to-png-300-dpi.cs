using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "OutputImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document using the cross‑platform Document class.
            using (Document pdfDocument = new Document(inputPdf))
            {
                // Create a PNG device with 300 DPI resolution. The default coordinate type (Point) is used.
                var pngDevice = new PngDevice(new Resolution(300));

                int pageCount = pdfDocument.Pages.Count;
                for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
                {
                    string outPath = Path.Combine(outputDir, $"page_{pageNumber}.png");
                    // Render the page to a PNG stream and write it to a file.
                    using (var outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        pngDevice.Process(pdfDocument.Pages[pageNumber], outStream);
                    }
                    Console.WriteLine($"Saved {outPath}");
                }
            }
        }
        // PngDevice relies on GDI+ which is Windows‑only; handle the platform limitation gracefully.
        catch (TypeInitializationException)
        {
            Console.WriteLine("Image conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
