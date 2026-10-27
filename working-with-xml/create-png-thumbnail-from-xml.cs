using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string thumbnailPath = "thumb.png";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Verify that the document contains at least one page
            if (pdfDoc.Pages.Count < 1)
            {
                Console.Error.WriteLine("The PDF does not contain any pages.");
                return;
            }

            // Define the resolution (DPI) for the thumbnail image
            Resolution resolution = new Resolution(150); // 150 DPI gives a reasonable thumbnail size

            // Create a PNG device. Width and height set to 0 keep the original page dimensions.
            PngDevice pngDevice = new PngDevice(0, 0, resolution);

            // Render the first page to a memory stream
            using (MemoryStream imageStream = new MemoryStream())
            {
                // Process the first page (pages are 1‑based)
                pngDevice.Process(pdfDoc.Pages[1], imageStream);

                // Save the resulting PNG bytes to a file
                File.WriteAllBytes(thumbnailPath, imageStream.ToArray());
            }

            Console.WriteLine($"Thumbnail image saved to '{thumbnailPath}'.");
        }
    }
}