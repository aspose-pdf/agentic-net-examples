using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text; // Added for TextFragment

class PdfToPngConverter
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "PngPages";

        // Create a minimal PDF if it does not already exist in the sandbox
        if (!File.Exists(pdfPath))
        {
            using (Document seed = new Document())
            {
                // Add a single page with some placeholder content
                Page page = seed.Pages.Add();
                page.Paragraphs.Add(new TextFragment("Sample page for PNG conversion"));
                seed.Save(pdfPath);
            }
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF and convert each page to a 300 DPI PNG image
        using (Document pdfDocument = new Document(pdfPath))
        {
            Resolution resolution = new Resolution(300); // 300 DPI for high quality
            PngDevice pngDevice = new PngDevice(resolution);

            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string pngPath = Path.Combine(outputDir, $"page_{pageNumber}.png");
                using (FileStream pngStream = new FileStream(pngPath, FileMode.Create))
                {
                    pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                }
                Console.WriteLine($"Page {pageNumber} saved as PNG: {pngPath}");
            }
        }
    }
}
