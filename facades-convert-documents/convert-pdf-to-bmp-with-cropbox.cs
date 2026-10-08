using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "BmpPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Desired resolution (dots per inch)
        var resolution = new Resolution(150);

        // Iterate through each page and render it as a BMP using the CropBox area
        for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            Page page = pdfDocument.Pages[pageNumber];

            // Use the CropBox if it is defined; otherwise fall back to the page rectangle
            var cropRect = (!page.CropBox.IsEmpty) ? page.CropBox : page.Rect;

            // BmpDevice does NOT implement IDisposable – instantiate it directly
            BmpDevice bmpDevice = new BmpDevice((int)cropRect.Width, (int)cropRect.Height, resolution);

            // Render the page into a memory stream (the stream *is* disposable)
            using (var ms = new MemoryStream())
            {
                bmpDevice.Process(page, ms);
                string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.bmp");
                File.WriteAllBytes(outPath, ms.ToArray());
                Console.WriteLine($"Saved: {outPath}");
            }
        }
    }
}
