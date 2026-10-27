using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputFolder = "Thumbnails";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Pages are 1‑based; iterate accordingly
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    // Create a PNG device with thumbnail dimensions (e.g., 150×200 pixels)
                    // PngDevice does NOT implement IDisposable, so we instantiate it directly.
                    var pngDevice = new PngDevice(150, 200)
                    {
                        // Optional: make background transparent for a cleaner thumbnail
                        TransparentBackground = true
                    };

                    string outPath = Path.Combine(outputFolder, $"page_{pageNum}.png");
                    // Write the PNG image to a file stream inside a using block (the stream *is* disposable)
                    using (FileStream outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        // Convert the specific page to PNG and save it
                        pngDevice.Process(pdfDoc.Pages[pageNum], outStream);
                    }
                }
            }

            Console.WriteLine($"Thumbnails saved to '{outputFolder}'.");
        }
        catch (TypeInitializationException)
        {
            // Image conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("Image conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
