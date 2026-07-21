using System;
using System.IO;
using Aspose.Pdf;   // Core Aspose.Pdf namespace (no Facades)

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";          // Source PDF
        const string outputPdfPath     = "output.pdf";         // Result PDF
        const string placeholderImgPath = "qr_placeholder.png"; // QR‑code placeholder image

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(placeholderImgPath))
        {
            Console.Error.WriteLine($"Placeholder image not found: {placeholderImgPath}");
            return;
        }

        // Open the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over every page in the document
            foreach (Page page in pdfDoc.Pages)
            {
                // Access the image collection of the current page
                XImageCollection images = page.Resources.Images;

                // XImageCollection uses 1‑based indexing, so loop from 1 to Count inclusive
                for (int i = 1; i <= images.Count; i++)
                {
                    // OPTIONAL: retrieve original image information (e.g., name) here
                    // string originalName = images[i].Name;

                    // Replace the image at position i with the QR‑code placeholder.
                    // A fresh stream is required for each Replace call.
                    using (FileStream placeholderStream = File.OpenRead(placeholderImgPath))
                    {
                        images.Replace(i, placeholderStream);
                    }
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"All images have been replaced with the QR placeholder. Output saved to '{outputPdfPath}'.");
    }
}