using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedImages";
        const string manifestPath = "manifest.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Verify that page 5 exists (Aspose.Pdf uses 1‑based indexing)
                if (pdfDoc.Pages.Count < 5)
                {
                    Console.Error.WriteLine("The document has fewer than 5 pages.");
                    return;
                }

                // Get page 5
                Page pageFive = pdfDoc.Pages[5];

                // Counter for naming images
                int imageIndex = 1;

                // Open manifest file for writing
                using (StreamWriter manifestWriter = new StreamWriter(manifestPath, false))
                {
                    // Iterate over all images on page 5
                    foreach (XImage img in pageFive.Resources.Images)
                    {
                        // Build a unique file name
                        string imageFileName = $"page5_image_{imageIndex}.jpg";
                        string imageFullPath = Path.Combine(outputFolder, imageFileName);

                        // Save the embedded image as JPEG using a FileStream (XImage.Save expects a Stream)
                        using (FileStream fs = new FileStream(imageFullPath, FileMode.Create, FileAccess.Write))
                        {
                            img.Save(fs, ImageFormat.Jpeg);
                        }

                        // Write entry to manifest
                        manifestWriter.WriteLine(imageFileName);

                        imageIndex++;
                    }
                }

                Console.WriteLine($"Extraction complete. Manifest written to '{manifestPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
