using System;
using System.IO;
using System.Drawing.Imaging;               // ImageFormat enum
using Aspose.Pdf;               // Document, etc.
using Aspose.Pdf.Facades;      // PdfExtractor

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where extracted images will be saved
        const string outputFolder = "ExtractedImages";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string pdfBaseName = Path.GetFileNameWithoutExtension(pdfPath);

            // Use a Document only to obtain the page count (required for 1‑based indexing)
            using (Document doc = new Document(pdfPath))
            {
                int pageCount = doc.Pages.Count; // 1‑based indexing rule

                // PdfExtractor is a Facade and implements IDisposable
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    extractor.BindPdf(pdfPath);

                    // Iterate through pages one by one
                    for (int page = 1; page <= pageCount; page++)
                    {
                        // Restrict extraction to the current page
                        extractor.StartPage = page;
                        extractor.EndPage   = page;

                        // Extract images from the selected page
                        extractor.ExtractImage();

                        int imageIndex = 1;
                        while (extractor.HasNextImage())
                        {
                            // Build output file name: <pdfname>_page<page>_img<index>.png
                            string outFile = Path.Combine(
                                outputFolder,
                                $"{pdfBaseName}_page{page}_img{imageIndex}.png");

                            // Save the image in PNG format – use System.Drawing.Imaging.ImageFormat
                            extractor.GetNextImage(outFile, ImageFormat.Png);
                            imageIndex++;
                        }
                    }
                }
            }
        }

        Console.WriteLine("Batch image extraction completed.");
    }
}
