using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchImageExtractor
{
    static void Main()
    {
        // Use platform‑agnostic paths (relative to the current working directory)
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputPdfs");
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "ExtractedImages");

        // Ensure the directories exist (create if missing)
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Base name of the PDF (without extension) for naming output files
            string pdfBaseName = Path.GetFileNameWithoutExtension(pdfPath);

            // Get page count using Document (required for per‑page extraction)
            int pageCount;
            using (Document doc = new Document(pdfPath))
            {
                pageCount = doc.Pages.Count; // 1‑based indexing
            }

            // Create a PdfExtractor for the current PDF
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file to the extractor
                extractor.BindPdf(pdfPath);

                // Iterate over each page to extract images with page number context
                for (int page = 1; page <= pageCount; page++)
                {
                    // Restrict extraction to a single page
                    extractor.StartPage = page;
                    extractor.EndPage   = page;

                    // Perform image extraction for the specified page
                    extractor.ExtractImage();

                    int imageIndex = 1;
                    // Retrieve all images found on this page
                    while (extractor.HasNextImage())
                    {
                        // Build output file name: <pdfname>_page<page>_img<index>.png
                        string outputFile = Path.Combine(
                            outputFolder,
                            $"{pdfBaseName}_page{page}_img{imageIndex}.png");

                        // Save the image using the overload that does not require System.Drawing.ImageFormat
                        // This avoids the CA1416 platform‑specific warning.
                        extractor.GetNextImage(outputFile);
                        imageIndex++;
                    }
                }
            }

            Console.WriteLine($"Images extracted from '{pdfPath}'.");
        }

        Console.WriteLine("Batch extraction completed.");
    }
}
