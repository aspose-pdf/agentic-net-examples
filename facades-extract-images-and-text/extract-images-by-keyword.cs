using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputDir  = "ExtractedImages";
        const string keyword    = "YOUR_KEYWORD"; // replace with the desired keyword

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Initialize the extractor facade
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file
            extractor.BindPdf(inputPdf);

            // Total number of pages (Aspose.Pdf uses 1‑based indexing)
            int pageCount = extractor.Document.Pages.Count;

            // Iterate through each page
            for (int page = 1; page <= pageCount; page++)
            {
                // Limit operations to the current page
                extractor.StartPage = page;
                extractor.EndPage   = page;

                // ----- Extract text from the current page -----
                extractor.ExtractText();

                // Capture the extracted text into a string
                string pageText;
                using (MemoryStream textStream = new MemoryStream())
                {
                    extractor.GetText(textStream);
                    pageText = Encoding.UTF8.GetString(textStream.ToArray());
                }

                // Check for the keyword (case‑insensitive)
                if (pageText.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    // ----- Extract images from the same page -----
                    extractor.ExtractImage();

                    int imageIndex = 1;
                    while (extractor.HasNextImage())
                    {
                        string imagePath = Path.Combine(
                            outputDir,
                            $"page{page}_img{imageIndex}.png"); // extension can be any supported image type

                        // Save the next image to file
                        extractor.GetNextImage(imagePath);
                        imageIndex++;
                    }
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}