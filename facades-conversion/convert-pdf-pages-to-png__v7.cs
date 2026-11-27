using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Pages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdf);
            int pageCount = pdfDocument.Pages.Count;

            // Create a PNG device with the desired resolution (DPI)
            // Devices are concrete but do NOT implement IDisposable, so no using block.
            var pngDevice = new PngDevice(new Resolution(300))
            {
                TransparentBackground = true // optional, makes background transparent
            };

            // Iterate over each page (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                // Render the current page into a memory stream
                using (MemoryStream imageStream = new MemoryStream())
                {
                    pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                    imageStream.Position = 0;

                    string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.png");
                    File.WriteAllBytes(outputPath, imageStream.ToArray());
                    Console.WriteLine($"Saved page {pageNumber} → {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
