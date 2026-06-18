using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchImageExtractor
{
    static void Main()
    {
        // Folder containing PDF files to process
        const string inputFolder = @"C:\InputPdfs";
        // Folder where extracted images will be saved
        const string outputFolder = @"C:\ExtractedImages";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string pdfFileName = Path.GetFileNameWithoutExtension(pdfPath);
            try
            {
                // Use Document to obtain the total page count – PdfExtractor does not expose PageCount directly
                using (Document doc = new Document(pdfPath))
                {
                    int pageCount = doc.Pages.Count; // 1‑based page count

                    using (PdfExtractor extractor = new PdfExtractor())
                    {
                        // Bind the current PDF file
                        extractor.BindPdf(pdfPath);

                        // Iterate through each page to keep page number in the output name
                        for (int page = 1; page <= pageCount; page++)
                        {
                            // Restrict extraction to a single page
                            extractor.StartPage = page;
                            extractor.EndPage = page;

                            // Extract images from the current page
                            extractor.ExtractImage();

                            int imageIndex = 1;
                            while (extractor.HasNextImage())
                            {
                                // Build output file name: <pdfname>_page<page>_img<index>.png
                                string outFile = Path.Combine(
                                    outputFolder,
                                    $"{pdfFileName}_page{page}_img{imageIndex}.png");

                                // Save the image in PNG format
                                extractor.GetNextImage(outFile, ImageFormat.Png);
                                imageIndex++;
                            }
                        }
                    }
                }

                Console.WriteLine($"Images extracted from: {pdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch extraction completed.");
    }
}
