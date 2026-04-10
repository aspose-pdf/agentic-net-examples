using System;
using System.IO;
using System.Text;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";               // source PDF
        const string outputDir  = "ExtractedImages";         // folder for images
        const string keyword    = "YOUR_KEYWORD";            // text to search for (case‑insensitive)

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // PdfExtractor implements IDisposable – use a using block for deterministic cleanup
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the facade
            extractor.BindPdf(inputPdf);

            // Total number of pages (1‑based indexing)
            int pageCount = extractor.Document.Pages.Count;

            // Iterate through each page, extract its text, and decide whether to extract images
            for (int pageNum = 1; pageNum <= pageCount; pageNum++)
            {
                // Limit operations to the current page
                extractor.StartPage = pageNum;
                extractor.EndPage   = pageNum;

                // ----- TEXT SCAN -------------------------------------------------
                extractor.ExtractText();                     // extract text of the current page

                // Retrieve the extracted text into a string (Unicode encoding)
                using (MemoryStream textStream = new MemoryStream())
                {
                    extractor.GetText(textStream);          // writes page text to the stream
                    string pageText = Encoding.Unicode.GetString(textStream.ToArray());

                    // Check for the keyword (case‑insensitive)
                    if (pageText.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // ----- IMAGE EXTRACTION ---------------------------------
                        // The ExtractImageMode property is not available in the current Aspose.Pdf version.
                        // Image extraction works by simply calling ExtractImage().
                        // extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed; // removed for compatibility

                        // Extract images from the same page range (single page)
                        extractor.ExtractImage();

                        int imageIndex = 1;
                        while (extractor.HasNextImage())
                        {
                            string imagePath = Path.Combine(
                                outputDir,
                                $"page{pageNum}_img{imageIndex}.png");

                            // Save each image as PNG (other formats are also supported)
                            extractor.GetNextImage(imagePath, ImageFormat.Png);
                            imageIndex++;
                        }
                    }
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
