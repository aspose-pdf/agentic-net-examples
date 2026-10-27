using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string tempImageFolder = "extracted_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the folder for extracted images exists and is empty
        if (Directory.Exists(tempImageFolder))
            Directory.Delete(tempImageFolder, true);
        Directory.CreateDirectory(tempImageFolder);

        // Extract images using PdfExtractor (Facades API)
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            // The ExtractImageMode property is not available in the referenced Aspose.Pdf version.
            // The default behavior of ExtractImage() extracts all images, so no explicit mode is required.
            // extractor.ExtractImageMode = ExtractImageMode.DefinedInResources; // removed for compatibility
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string outPath = Path.Combine(tempImageFolder, $"image_{imageIndex}.png");
                extractor.GetNextImage(outPath);
                imageIndex++;
            }
        }

        // Determine if any image files were produced
        string[] extractedFiles = Directory.GetFiles(tempImageFolder);
        bool isTextOnly = extractedFiles.Length == 0;

        Console.WriteLine(isTextOnly
            ? "The PDF is text‑only (no images extracted)."
            : $"The PDF contains images ({extractedFiles.Length} file(s) extracted).");

        // Optional cleanup
        Directory.Delete(tempImageFolder, true);
    }
}
