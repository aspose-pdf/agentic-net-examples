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
        const string keyword    = "CONFIDENTIAL";      // word to search for (case‑insensitive)

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // PdfExtractor is a Facade – it implements IDisposable, so wrap it in a using block.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor.
            extractor.BindPdf(inputPdf);

            // Get total number of pages (1‑based indexing).
            int pageCount = extractor.Document.Pages.Count;

            // Loop through each page, extract its text and decide whether to extract images.
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                // ----- STEP 1: Extract text of the current page -----
                extractor.StartPage = pageNumber;
                extractor.EndPage   = pageNumber;
                extractor.ExtractText();

                // Retrieve the page text into a memory stream.
                using (MemoryStream textStream = new MemoryStream())
                {
                    extractor.GetNextPageText(textStream);
                    textStream.Position = 0;
                    string pageText = new StreamReader(textStream).ReadToEnd();

                    // Check for the keyword (case‑insensitive).
                    if (pageText.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // ----- STEP 2: Extract images from this page -----
                        // Use the mode that extracts only actually used images.
                        extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;

                        // Set the same page range again before extracting images.
                        extractor.StartPage = pageNumber;
                        extractor.EndPage   = pageNumber;
                        extractor.ExtractImage();

                        int imageIndex = 1;
                        while (extractor.HasNextImage())
                        {
                            // Build a file name that includes page and image numbers.
                            string imagePath = Path.Combine(
                                outputDir,
                                $"page{pageNumber}_img{imageIndex}.png");

                            // Save the image. The format is inferred from the file extension.
                            extractor.GetNextImage(imagePath);
                            imageIndex++;
                        }
                    }
                }
            }

            // Close the underlying document (optional, Dispose will also handle it).
            extractor.Close();
        }

        Console.WriteLine("Image extraction completed.");
    }
}