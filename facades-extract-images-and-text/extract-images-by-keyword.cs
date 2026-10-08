using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputDir  = "ExtractedImages";   // folder for images
        const string keyword    = "YOUR_KEYWORD";      // word to search for (case‑insensitive)

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the document to obtain the total page count (PdfExtractor does not expose PageCount)
        Document pdfDocument = new Document(inputPdf);
        int pageCount = pdfDocument.Pages.Count;

        // PdfExtractor implements IDisposable – wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document once – we will change the page range for each operation
            extractor.BindPdf(inputPdf);

            // Iterate through each page
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                // -------------------------------------------------------------
                // 1. Extract text from the current page to decide whether to keep it
                // -------------------------------------------------------------
                extractor.StartPage = pageNumber;   // set page range for text extraction
                extractor.EndPage   = pageNumber;
                extractor.ExtractText();

                string pageText;
                using (MemoryStream textStream = new MemoryStream())
                {
                    // GetText writes the extracted text into the supplied stream
                    extractor.GetText(textStream);
                    textStream.Position = 0;
                    using (StreamReader reader = new StreamReader(textStream))
                    {
                        pageText = reader.ReadToEnd();
                    }
                }

                if (string.IsNullOrEmpty(pageText) ||
                    pageText.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) < 0)
                {
                    // Keyword not found – skip image extraction for this page
                    continue;
                }

                // ---------------------------------------------------------------
                // 2. Keyword found – now extract all images from this page
                // ---------------------------------------------------------------
                extractor.StartPage = pageNumber;   // set page range for image extraction
                extractor.EndPage   = pageNumber;
                // No ExtractImageMode property in the current library version – default behaviour extracts all images
                extractor.ExtractImage();

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    string outPath = Path.Combine(outputDir,
                        $"page_{pageNumber}_img_{imageIndex}.png");
                    extractor.GetNextImage(outPath);
                    Console.WriteLine($"Saved image: {outPath}");
                    imageIndex++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
