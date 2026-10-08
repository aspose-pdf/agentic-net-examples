using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document to obtain the page count (PdfExtractor does not expose it directly)
            Document pdfDoc = new Document(inputPath);
            int totalPages = pdfDoc.Pages.Count;

            // PdfExtractor implements IDisposable, so wrap it in a using block
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file
                extractor.BindPdf(inputPath);

                // Configure extraction to keep images in their original format.
                // The correct property in this library version is ExtractImageMode.
                // Using DefinedInResources extracts the image streams exactly as stored in the PDF.
                extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

                // Set the page range to the whole document
                extractor.StartPage = 1;
                extractor.EndPage   = totalPages;

                // Perform the extraction
                extractor.ExtractImage();

                // Save each extracted image. The extractor provides HasNextImage/GetNextImage methods.
                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    string outPath = Path.Combine(outputDir, $"Image_{imageIndex}.png");
                    extractor.GetNextImage(outPath);
                    imageIndex++;
                }
            }

            Console.WriteLine($"Images have been extracted to '{outputDir}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
