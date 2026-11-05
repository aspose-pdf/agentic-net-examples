using System;
using System.IO;
using Aspose.Pdf;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                int imageCounter = 1;

                // Iterate over all pages (1‑based indexing)
                foreach (Page page in doc.Pages)
                {
                    // XImageCollection is not a dictionary; iterate directly
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Build a unique file name for each extracted image
                        string fileName = $"image_{imageCounter:D4}.png";
                        string outPath = Path.Combine(outputDir, fileName);

                        // Save the image as PNG using a FileStream (XImage.Save expects a Stream)
                        using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                        {
                            img.Save(fs, ImageFormat.Png);
                        }

                        Console.WriteLine($"Saved {outPath} (Page {page.Number})");
                        imageCounter++;
                    }
                }
            }

            Console.WriteLine("All images have been extracted successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}
