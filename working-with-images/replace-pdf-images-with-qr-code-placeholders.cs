using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string placeholderImagePath = "qr_placeholder.png"; // QR code image that links to the original source
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(placeholderImagePath))
        {
            Console.Error.WriteLine($"Placeholder image not found: {placeholderImagePath}");
            return;
        }

        // Load the PDF document (using the required create‑load‑save pattern)
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdfPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Aspose.Pdf.Page page = pdfDoc.Pages[pageIndex];
                Aspose.Pdf.XImageCollection images = page.Resources.Images;

                // Replace each image on the page with the QR‑code placeholder
                for (int imgIndex = 1; imgIndex <= images.Count; imgIndex++)
                {
                    // Keep a reference to the original image (to use its name in alt‑text)
                    Aspose.Pdf.XImage originalImage = images[imgIndex];

                    // Replace the image with the placeholder (the Replace method expects a 1‑based index)
                    using (FileStream placeholderStream = File.OpenRead(placeholderImagePath))
                    {
                        images.Replace(imgIndex, placeholderStream);
                    }

                    // After replacement obtain the new image object
                    Aspose.Pdf.XImage newImage = images[imgIndex];

                    // Set alternative text that points back to the original image identifier
                    string altText = $"Original image name: {originalImage.Name}";
                    newImage.TrySetAlternativeText(altText, page);
                }
            }

            // Save the modified PDF (the Save method without SaveOptions always writes PDF)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with QR‑code placeholders: {outputPdfPath}");
    }
}