using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string highResFolder = "HighResImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Load the PDF for page count information
        using (Document doc = new Document(inputPdf))
        {
            // Facade for editing PDF content (image replacement)
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(inputPdf);

                // Iterate through each page (1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    // Use PdfExtractor to determine how many images are on the current page
                    using (PdfExtractor extractor = new PdfExtractor())
                    {
                        extractor.BindPdf(inputPdf);
                        extractor.StartPage = pageNum;
                        extractor.EndPage = pageNum;
                        extractor.ExtractImage();

                        int imageIndex = 1;
                        while (extractor.HasNextImage())
                        {
                            // Expected high‑resolution PNG file name pattern:
                            //   page{page}_img{index}.png
                            string highResPath = Path.Combine(
                                highResFolder,
                                $"page{pageNum}_img{imageIndex}.png");

                            if (File.Exists(highResPath))
                            {
                                // Replace the low‑resolution image with the high‑resolution PNG
                                editor.ReplaceImage(pageNum, imageIndex, highResPath);
                            }

                            // Advance the extractor to the next image (output discarded)
                            string tempFile = Path.GetTempFileName();
                            extractor.GetNextImage(tempFile);
                            File.Delete(tempFile);

                            imageIndex++;
                        }
                    }
                }

                // Save the modified PDF
                editor.Save(outputPdf);
            }
        }

        Console.WriteLine($"High‑resolution image replacement completed. Output saved to '{outputPdf}'.");
    }
}