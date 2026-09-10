using System;
using System.IO;
using Aspose.Pdf;

class ReplaceImagesWithQrPlaceholder
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string qrPlaceholderPath = "qr_placeholder.png"; // QR code image file

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(qrPlaceholderPath))
        {
            Console.Error.WriteLine($"QR placeholder image not found: {qrPlaceholderPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];
                var images = page.Resources.Images;

                // If the page contains no images, skip it
                if (images.Count == 0)
                    continue;

                // Iterate over the images collection using 1‑based index
                for (int imgIndex = 1; imgIndex <= images.Count; imgIndex++)
                {
                    // Capture the original image reference and its name (used as source identifier)
                    XImage originalImage = images[imgIndex];
                    string originalImageName = images.GetImageName(originalImage);

                    // Replace the image with the QR code placeholder
                    using (FileStream placeholderStream = File.OpenRead(qrPlaceholderPath))
                    {
                        // Replace expects a 1‑based index and a stream containing the new image data
                        images.Replace(imgIndex, placeholderStream);
                    }

                    // After replacement, obtain the new image object to set alternative text
                    XImage replacedImage = images[imgIndex];
                    // Store the original image identifier as alternative text (acts as a link to the source)
                    replacedImage.TrySetAlternativeText($"Original image source: {originalImageName}", page);
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with QR placeholders: {outputPdfPath}");
    }
}