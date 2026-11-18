using System;
using System.IO;
using System.Net.Http;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // The URL that each QR code will encode.
        // In a real scenario this could be the original image source URL.
        const string placeholderUrl = "https://example.com/original-image";

        // Create a single HttpClient instance for downloading QR code images.
        using (HttpClient httpClient = new HttpClient())
        // Open the PDF document inside a using block for deterministic disposal.
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Iterate over all pages in the document.
            foreach (Page page in pdfDocument.Pages)
            {
                // Build the QR code image URL using a public QR code generation service.
                string qrServiceUrl = $"https://api.qrserver.com/v1/create-qr-code/?size=150x150&data={Uri.EscapeDataString(placeholderUrl)}";

                // Download the QR code image into a memory stream.
                using (Stream qrStream = httpClient.GetStreamAsync(qrServiceUrl).Result)
                {
                    // Add the QR code image to the page resources.
                    // The ImageStamp constructor can accept a stream directly.
                    ImageStamp qrStamp = new ImageStamp(qrStream)
                    {
                        // Position the QR code at the top‑left corner of the page.
                        // Adjust these values as needed for your layout.
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment   = VerticalAlignment.Top,
                        // Optional: set explicit width/height (in points).
                        // 1 point = 1/72 inch.
                        Width  = 100,
                        Height = 100,
                        // Ensure the QR code is placed above existing content.
                        // The default is to overlay the page.
                        // No background to keep it transparent.
                        Background = false
                    };

                    // Apply the stamp to the current page.
                    page.AddStamp(qrStamp);
                }
            }

            // Save the modified PDF.
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with QR code placeholders saved to '{outputPdfPath}'.");
    }
}