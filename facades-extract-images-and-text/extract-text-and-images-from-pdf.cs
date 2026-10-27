using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Determine the number of pages in the PDF
        int pageCount;
        using (var doc = new Document(pdfPath))
        {
            pageCount = doc.Pages.Count;
        }

        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file
            extractor.BindPdf(pdfPath);

            // ---------- Text extraction ----------
            // Call the method to start text extraction
            extractor.ExtractText();

            // Get the extracted text via a stream (PdfExtractor has no parameter‑less GetText())
            string extractedText;
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);
                textStream.Position = 0;
                using (StreamReader reader = new StreamReader(textStream))
                {
                    extractedText = reader.ReadToEnd();
                }
            }
            Console.WriteLine("=== Extracted Text ===");
            Console.WriteLine(extractedText);

            // ---------- Image extraction ----------
            // Enable resource‑based image extraction (recommended for most PDFs)
            extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

            for (int page = 1; page <= pageCount; page++)
            {
                // Set the page range for the current iteration
                extractor.StartPage = page;
                extractor.EndPage   = page;

                // Start image extraction for the selected page
                extractor.ExtractImage();

                int imageIndex = 1;
                // Retrieve each image sequentially
                while (extractor.HasNextImage())
                {
                    string imgPath = $"image_page{page}_{imageIndex}.png";
                    extractor.GetNextImage(imgPath); // saves the image to file
                    Console.WriteLine($"Saved image: {imgPath}");
                    imageIndex++;
                }
            }
        }
    }
}
