using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "portfolio.pdf";
        const string outputPdfPath = "portfolio_with_thumbnails.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Desired thumbnail size (in points; 1 point = 1/72 inch)
            const int thumbWidth = 100; // approx 1.39 inches
            const int thumbHeight = 100;

            // Iterate over all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Render the page to a thumbnail image using ThumbnailDevice
                using (MemoryStream thumbStream = new MemoryStream())
                {
                    // ThumbnailDevice does NOT implement IDisposable, so we instantiate it without a using block
                    ThumbnailDevice thumbDevice = new ThumbnailDevice(thumbWidth, thumbHeight);
                    thumbDevice.Process(page, thumbStream);

                    // Reset stream position before reading
                    thumbStream.Position = 0;

                    // Copy the thumbnail data to a new stream that will stay alive until the PDF is saved
                    byte[] thumbBytes = thumbStream.ToArray();
                    MemoryStream imageStream = new MemoryStream(thumbBytes);

                    // Create an Image object from the thumbnail stream
                    Image thumbImage = new Image
                    {
                        ImageStream = imageStream,
                        FixWidth = thumbWidth,
                        FixHeight = thumbHeight,
                        // Position the thumbnail at the top‑left corner of the page
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top,
                        // Optional margin from the page edges
                        Margin = new MarginInfo { Left = 10, Top = 10 }
                    };

                    // Add the thumbnail image to the page's content
                    page.Paragraphs.Add(thumbImage);
                }
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with thumbnails saved to '{outputPdfPath}'.");
    }
}
