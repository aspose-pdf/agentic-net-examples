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

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Pages are 1‑based in Aspose.Pdf
                for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
                {
                    Page page = pdfDoc.Pages[pageIndex];
                    bool imageSaved = false;

                    // Iterate over the image collection (not a dictionary)
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Save the first image of the page as JPEG using a FileStream
                        string outputPath = Path.Combine(outputFolder, $"Page_{pageIndex}_Image.jpg");
                        using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                        {
                            img.Save(fs, ImageFormat.Jpeg);
                        }
                        Console.WriteLine($"Saved image from page {pageIndex} to '{outputPath}'.");
                        imageSaved = true;
                        break; // Only the first image per page is required
                    }

                    if (!imageSaved)
                    {
                        Console.WriteLine($"No images found on page {pageIndex}.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
