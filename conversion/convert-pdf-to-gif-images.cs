using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "GifPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Iterate using 1‑based page indexing (Aspose.Pdf rule)
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    Page page = pdfDoc.Pages[pageNum];

                    // Build output file name for each page
                    string outPath = Path.Combine(outputDir, $"Page_{pageNum}.gif");

                    // GifDevice is a concrete implementation of ImageDevice for GIF output
                    GifDevice gifDevice = new GifDevice();

                    // Render the page to GIF with default parameters
                    gifDevice.Process(page, outPath);
                }
            }

            Console.WriteLine($"PDF successfully converted to GIF images in '{outputDir}'.");
        }
        // GDI+ (required for image rendering) is Windows‑only; handle cross‑platform scenarios
        catch (TypeInitializationException)
        {
            Console.WriteLine("Image conversion requires GDI+ and is only supported on Windows.");
        }
        catch (DllNotFoundException)
        {
            Console.WriteLine("Required GDI+ library not found. Image conversion skipped.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
