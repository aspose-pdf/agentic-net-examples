using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "OutputImages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Create a JPEG device with 96 DPI resolution and 80% quality (web‑friendly)
                // JpegDevice does not expose an ImageQuality property in this version; quality is set via the constructor.
                var jpegDevice = new JpegDevice(new Resolution(96), 80);

                // Iterate pages using 1‑based indexing
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    string outPath = Path.Combine(outputDir, $"page_{i}.jpg");
                    // Convert the current page to a JPEG file
                    jpegDevice.Process(doc.Pages[i], outPath);
                }
            }

            Console.WriteLine("PDF pages have been successfully converted to JPEG images.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
