using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF
        using (Document pdfDocument = new Document())
        {
            // Add first page with text
            Page page1 = pdfDocument.Pages.Add();
            TextFragment fragment1 = new TextFragment("First page");
            page1.Paragraphs.Add(fragment1);

            // Add second page with text
            Page page2 = pdfDocument.Pages.Add();
            TextFragment fragment2 = new TextFragment("Second page");
            page2.Paragraphs.Add(fragment2);

            // Save the sample PDF
            pdfDocument.Save("input.pdf");
        }

        // Load the PDF and convert to multi-page TIFF
        using (Document pdfDocument = new Document("input.pdf"))
        {
            // Set resolution (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Configure TIFF settings
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.None,
                Depth = ColorDepth.Default,
                Shape = ShapeType.Landscape,
                SkipBlankPages = false
            };

            // Create TIFF device
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Convert all pages to a single multi-page TIFF file
            tiffDevice.Process(pdfDocument, "output.tif");
        }
    }
}