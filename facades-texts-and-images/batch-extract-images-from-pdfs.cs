using System;
using System.IO;
using System.Drawing.Imaging; // ImageFormat enum
using Aspose.Pdf;               // Document class
using Aspose.Pdf.Facades;       // PdfExtractor class

class BatchImageExtractor
{
    static void Main()
    {
        // Folder containing source PDF files
        const string inputFolder = @"C:\InputPdfs";
        // Folder where extracted images will be saved
        const string outputFolder = @"C:\ExtractedImages";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Use Document only to obtain the page count (required for naming)
            using (Document doc = new Document(pdfPath))
            {
                int pageCount = doc.Pages.Count; // 1‑based indexing

                // Iterate through each page to extract images per page
                for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
                {
                    // PdfExtractor implements IDisposable, so wrap it in a using block
                    using (PdfExtractor extractor = new PdfExtractor())
                    {
                        // Bind the current PDF file
                        extractor.BindPdf(pdfPath);

                        // Restrict extraction to a single page
                        extractor.StartPage = pageNumber;
                        extractor.EndPage   = pageNumber;

                        // Prepare the extractor for image extraction
                        extractor.ExtractImage();

                        int imageIndex = 1; // Counter for images on the current page

                        // Retrieve all images on this page
                        while (extractor.HasNextImage())
                        {
                            // Build a filename that includes the original PDF name,
                            // page number, and image index (e.g., Report_page3_img2.png)
                            string outputFileName = $"{Path.GetFileNameWithoutExtension(pdfPath)}_page{pageNumber}_img{imageIndex}.png";
                            string outputPath = Path.Combine(outputFolder, outputFileName);

                            // Save the extracted image as PNG
                            extractor.GetNextImage(outputPath, ImageFormat.Png);

                            imageIndex++;
                        }
                    }
                }
            }

            Console.WriteLine($"Finished extracting images from: {Path.GetFileName(pdfPath)}");
        }

        Console.WriteLine("Batch image extraction completed.");
    }
}