using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Load the PDF to obtain the page count (wrapped in using for deterministic disposal)
            using (Document doc = new Document(pdfPath))
            {
                int pageCount = doc.Pages.Count; // Aspose.Pdf uses 1‑based page indexing

                // Base name for output files (without extension)
                string baseName = Path.GetFileNameWithoutExtension(pdfPath);

                // Iterate through all pages
                for (int pageNum = 1; pageNum <= pageCount; pageNum++)
                {
                    // Create a new extractor for the current page
                    using (PdfExtractor extractor = new PdfExtractor())
                    {
                        extractor.BindPdf(pdfPath);
                        // Restrict extraction to the current page only
                        extractor.StartPage = pageNum;
                        extractor.EndPage   = pageNum;
                        // Ensure we extract images that are defined in the PDF resources
                        extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;
                        extractor.ExtractImage();

                        int imgIdx = 1;
                        // Loop through all images on the current page
                        while (extractor.HasNextImage())
                        {
                            string outFile = Path.Combine(
                                outputFolder,
                                $"{baseName}_page{pageNum}_img{imgIdx}.png");

                            // Write the image to disk
                            extractor.GetNextImage(outFile);
                            imgIdx++;
                        }
                    }
                }
            }

            Console.WriteLine($"Finished processing: {Path.GetFileName(pdfPath)}");
        }

        Console.WriteLine("All images have been extracted.");
    }
}
