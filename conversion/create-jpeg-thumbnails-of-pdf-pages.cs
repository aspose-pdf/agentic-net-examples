using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "Thumbnails";
        const int targetHeightPx = 80; // desired thumbnail height in pixels
        const int jpegQuality = 90;    // JPEG quality (0‑100)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document – Document implements IDisposable
            using (Document doc = new Document(inputPath))
            {
                // Aspose.Pdf uses 1‑based page indexing
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    // Page height is expressed in points (1 point = 1/72 inch)
                    double pageHeightPoints = doc.Pages[i].PageInfo.Height;

                    // Compute a resolution that will render the page with the required pixel height.
                    // heightPx = (resolution * pageHeightPoints) / 72  =>  resolution = heightPx * 72 / pageHeightPoints
                    double resolutionValue = targetHeightPx * 72.0 / pageHeightPoints;
                    // Resolution constructor expects an integer DPI; round to nearest whole number.
                    var resolution = new Resolution((int)Math.Round(resolutionValue));

                    // Create a concrete JPEG device – it does NOT implement IDisposable, so no using block.
                    var jpegDevice = new JpegDevice(resolution, jpegQuality);

                    string outputPath = Path.Combine(outputDir, $"page_{i}.jpg");

                    // Render the page to a JPEG file
                    using (FileStream imageStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        jpegDevice.Process(doc.Pages[i], imageStream);
                    }
                }
            }

            Console.WriteLine("JPEG thumbnails created successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
