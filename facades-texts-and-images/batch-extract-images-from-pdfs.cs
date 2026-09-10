using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfExtractor resides here
using System.Drawing.Imaging;      // ImageFormat for specifying output format

class BatchImageExtractor
{
    static void Main()
    {
        // Folder containing PDF files to process
        const string inputFolder  = @"C:\InputPdfs";
        // Folder where extracted images will be saved
        const string outputFolder = @"C:\ExtractedImages";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        foreach (string pdfPath in pdfFiles)
        {
            // Use PdfExtractor inside a using block for deterministic disposal
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the current PDF file
                extractor.BindPdf(pdfPath);

                // Total pages in the document (Aspose.Pdf uses 1‑based indexing)
                int pageCount = extractor.Document.Pages.Count;

                // Iterate through each page to keep track of the page number
                for (int pageNum = 1; pageNum <= pageCount; pageNum++)
                {
                    // Restrict extraction to a single page
                    extractor.StartPage = pageNum;
                    extractor.EndPage   = pageNum;

                    // Extract images from the specified page
                    extractor.ExtractImage();

                    int imageIndex = 1; // Reset image counter for each page

                    // Retrieve all images found on this page
                    while (extractor.HasNextImage())
                    {
                        // Build a file name that includes the original PDF name,
                        // page number, and image index (e.g., Sample_page3_img2.png)
                        string outputFileName = $"{Path.GetFileNameWithoutExtension(pdfPath)}_page{pageNum}_img{imageIndex}.png";
                        string outputPath     = Path.Combine(outputFolder, outputFileName);

                        // Save the image as PNG (any ImageFormat supported by System.Drawing.Imaging can be used)
                        extractor.GetNextImage(outputPath, ImageFormat.Png);

                        imageIndex++;
                    }
                }
            }

            Console.WriteLine($"Images extracted from: {Path.GetFileName(pdfPath)}");
        }

        Console.WriteLine("Batch extraction completed.");
    }
}