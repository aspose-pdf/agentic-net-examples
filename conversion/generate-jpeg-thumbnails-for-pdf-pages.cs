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

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Iterate using 1‑based page indexing (Aspose.Pdf rule)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Page dimensions are in points (1 point = 1/72 inch)
                    double pageWidthPoints = page.PageInfo.Width;
                    double pageHeightPoints = page.PageInfo.Height;

                    const int targetPixelWidth = 120; // Desired thumbnail width in pixels

                    // Compute DPI needed to obtain the target pixel width
                    // DPI = pixels / (points / 72)
                    double dpi = targetPixelWidth * 72.0 / pageWidthPoints;
                    int dpiInt = (int)Math.Round(dpi);

                    // Create a JpegDevice with the calculated DPI and maximum quality (0‑100)
                    Resolution resolution = new Resolution(dpiInt);
                    JpegDevice jpegDevice = new JpegDevice(resolution, 100);

                    // Determine the output file path
                    string outPath = Path.Combine(outputDir, $"page_{i}.jpg");

                    // Render the page directly to a file stream (devices are not IDisposable)
                    using (FileStream outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        jpegDevice.Process(page, outStream);
                    }
                }
            }

            Console.WriteLine("Thumbnails generated successfully.");
        }
        // Image conversion may require GDI+ (Windows only)
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
