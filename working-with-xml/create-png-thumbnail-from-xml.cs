using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // PDF generated from XML
        const string thumbnailPath = "thumbnail.png";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Verify that the document has at least one page
            if (pdfDoc.Pages.Count < 1)
            {
                Console.Error.WriteLine("PDF contains no pages.");
                return;
            }

            // Aspose.Pdf uses 1‑based indexing for pages
            Page firstPage = pdfDoc.Pages[1];

            // Define the resolution for the thumbnail (e.g., 150 DPI)
            const int resolution = 150;

            // Devices are concrete but do NOT implement IDisposable – do NOT wrap them in a using block
            var pngDevice = new PngDevice(new Resolution(resolution))
            {
                TransparentBackground = true // optional, makes background transparent
            };

            // Render the first page into a memory stream
            using (MemoryStream imageStream = new MemoryStream())
            {
                pngDevice.Process(firstPage, imageStream);
                imageStream.Position = 0; // Reset stream before writing

                // Save the PNG bytes to the output file
                using (FileStream fileStream = new FileStream(thumbnailPath, FileMode.Create, FileAccess.Write))
                {
                    imageStream.CopyTo(fileStream);
                }
            }
        }

        Console.WriteLine($"Thumbnail saved to '{thumbnailPath}'.");
    }
}
