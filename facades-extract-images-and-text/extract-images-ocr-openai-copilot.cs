using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // for potential text handling if needed

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string imagesOutputDir = "ExtractedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(imagesOutputDir);

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                int imageCounter = 1;

                // Iterate over all pages (1‑based indexing)
                for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
                {
                    Page page = pdfDoc.Pages[pageIndex];

                    // Iterate over the XImage collection directly (no dictionary semantics)
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Build a unique file name for each extracted image
                        string imagePath = Path.Combine(imagesOutputDir, $"Page{pageIndex}_Image{imageCounter}.png");

                        // Save the image to the file system
                        using (FileStream imgStream = File.Create(imagePath))
                        {
                            img.Save(imgStream);
                        }

                        Console.WriteLine($"Extracted image saved to: {imagePath}");

                        // ------------------------------------------------------------
                        // OCR placeholder:
                        // At this point you can feed the saved image file to an OCR engine
                        // (e.g., Aspose.OCR, Tesseract, etc.) to obtain hidden textual data.
                        // The OCR implementation is outside the scope of Aspose.Pdf.
                        // ------------------------------------------------------------

                        imageCounter++;
                    }
                }
            }

            Console.WriteLine("Image extraction completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}