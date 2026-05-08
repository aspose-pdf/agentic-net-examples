using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string placeholderImg = "placeholder.png"; // QR‑code image prepared beforehand

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(placeholderImg))
        {
            Console.Error.WriteLine($"Placeholder image not found: {placeholderImg}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                // Process only odd‑numbered pages
                if (pageIndex % 2 == 1)
                {
                    Page page = pdfDoc.Pages[pageIndex];
                    var imageCollection = page.Resources.Images;

                    // XImageCollection uses 1‑based indexing as well
                    int imageCount = imageCollection.Count;

                    for (int imgIdx = 1; imgIdx <= imageCount; imgIdx++)
                    {
                        // Retrieve the original image (optional – for alt text)
                        XImage originalImage = imageCollection[imgIdx];

                        // Set alternative text indicating replacement (helps accessibility)
                        originalImage.TrySetAlternativeText(
                            $"Image on page {pageIndex} replaced by QR‑code placeholder.", page);

                        // Replace the image with the prepared QR‑code placeholder
                        using (FileStream placeholderStream = File.OpenRead(placeholderImg))
                        {
                            // Replace expects a stream containing JPEG data; ensure the placeholder is JPEG
                            imageCollection.Replace(imgIdx, placeholderStream);
                        }
                    }
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with placeholders: {outputPdfPath}");
    }
}