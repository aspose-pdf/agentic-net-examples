using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        // Input PDF file (will be created if it does not exist)
        const string inputPdf = "input.pdf";
        const string outputDir = "Thumbnails";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // If the source PDF is missing, create a minimal one so the example can run without external files
        if (!File.Exists(inputPdf))
        {
            CreateSamplePdf(inputPdf);
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Create a ThumbnailDevice – default size is 200x200 pixels. You can customise it, e.g. new ThumbnailDevice(150, 150)
            ThumbnailDevice thumbnailDevice = new ThumbnailDevice();

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string thumbPath = Path.Combine(outputDir, $"thumb_page{pageNumber}.png");

                // Process the page and write the PNG thumbnail to the file stream
                using (FileStream outputStream = new FileStream(thumbPath, FileMode.Create))
                {
                    thumbnailDevice.Process(pdfDocument.Pages[pageNumber], outputStream);
                }
            }
        }

        Console.WriteLine("Thumbnail generation completed.");
    }

    /// <summary>
    /// Creates a very small PDF containing a single page with some sample text.
    /// This helper is used only when the expected input file is missing, allowing the sample to run out‑of‑the‑box.
    /// </summary>
    private static void CreateSamplePdf(string path)
    {
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Sample PDF for thumbnail generation"));
            doc.Save(path);
        }
    }
}
