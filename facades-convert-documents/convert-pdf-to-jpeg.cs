using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Images";
        Directory.CreateDirectory(outputDir);

        // If the source PDF does not exist, create a minimal placeholder so the program can run.
        if (!File.Exists(inputPdf))
        {
            using (Document placeholder = new Document())
            {
                Page page = placeholder.Pages.Add();
                page.Paragraphs.Add(new TextFragment("Sample PDF generated because 'input.pdf' was missing."));
                placeholder.Save(inputPdf);
                Console.WriteLine($"Created placeholder PDF: {inputPdf}");
            }
        }

        // Load the PDF document.
        using (Document pdfDocument = new Document(inputPdf))
        {
            // 72 DPI keeps the original page size (1 point = 1 pixel at 72 DPI).
            Resolution resolution = new Resolution(72);
            // JPEG device – default colour depth is 24‑bit; 100 is the quality (0‑100).
            JpegDevice jpegDevice = new JpegDevice(resolution, 100);

            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpg");
                using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
                {
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                }
            }
        }

        Console.WriteLine("PDF conversion to JPEG completed successfully.");
    }
}
