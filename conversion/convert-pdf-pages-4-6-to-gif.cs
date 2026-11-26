using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "GifPages";

        // Verify input PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Check that pages 4‑6 are available
                if (doc.Pages.Count < 6)
                {
                    Console.Error.WriteLine("The PDF contains fewer than 6 pages.");
                    return;
                }

                // Process pages 4, 5, and 6
                for (int i = 4; i <= 6; i++)
                {
                    Page page = doc.Pages[i]; // 1‑based indexing
                    string outPath = Path.Combine(outputFolder, $"Page_{i}.gif");

                    // Save each page as a GIF at 300 DPI
                    using (FileStream outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        GifDevice gifDevice = new GifDevice(300, 300); // X and Y DPI
                        gifDevice.Process(page, outStream);
                    }

                    Console.WriteLine($"Saved GIF for page {i} to '{outPath}'.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}