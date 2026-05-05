using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputDir = "ExtractedImages";    // folder for JPEGs

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Verify that the source PDF exists before proceeding
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Convert only page 5 to grayscale (Aspose.Pdf uses 1‑based page indexing)
            if (doc.Pages.Count >= 5)
            {
                doc.Pages[5].MakeGrayscale();
            }
            else
            {
                Console.Error.WriteLine("PDF does not contain page 5.");
                return;
            }

            // Set up the extractor to work on page 5 only
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(doc);          // bind the in‑memory document
                extractor.StartPage = 5;         // start page (inclusive)
                extractor.EndPage   = 5;         // end page (inclusive)

                // Extract images from the specified page range
                extractor.ExtractImage();

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    // Build output file name: ExtractedImages\image_5_1.jpg, image_5_2.jpg, ...
                    string outputPath = Path.Combine(
                        outputDir,
                        $"image_5_{imageIndex}.jpg");

                    // Save the next image. The overload without ImageFormat avoids the
                    // Windows‑only System.Drawing.ImageFormat dependency and lets Aspose
                    // infer the format from the file extension.
#pragma warning disable CA1416 // Suppress platform‑specific warning for ImageFormat usage
                    extractor.GetNextImage(outputPath);
#pragma warning restore CA1416

                    imageIndex++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
