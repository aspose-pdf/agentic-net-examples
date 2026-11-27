using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;   // needed for PngDevice (image conversion)

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputFolder = "TableBorders";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Document must be wrapped in a using block (lifecycle rule)
            using (Document doc = new Document(inputPath))
            {
                // Pages are 1‑based (page-indexing-one-based rule)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    // Process only odd‑numbered pages
                    if (pageNum % 2 == 0) continue;

                    // Render the page to PNG. The PngDevice rasterizes the page,
                    // which includes table borders as part of the graphics.
                    // Resolution 300 DPI gives good quality for borders.
                    PngDevice pngDevice = new PngDevice(new Resolution(300));

                    using (MemoryStream ms = new MemoryStream())
                    {
                        // Convert the page to PNG and write to memory stream
                        pngDevice.Process(doc.Pages[pageNum], ms);
                        ms.Position = 0;

                        // Save PNG file with a name that reflects the source page
                        string outputPath = Path.Combine(outputFolder, $"Page_{pageNum}_TableBorders.png");
                        File.WriteAllBytes(outputPath, ms.ToArray());

                        Console.WriteLine($"Saved borders of page {pageNum} to '{outputPath}'.");
                    }
                }
            }
        }
        // HTML‑to‑image and PDF‑to‑image conversions require GDI+ (Windows only)
        catch (TypeInitializationException)
        {
            Console.WriteLine("Image conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
