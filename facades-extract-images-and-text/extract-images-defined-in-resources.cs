using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string outputFolder = "ExtractedImages";

        // Ensure the input PDF exists – create a minimal one if it does not.
        if (!File.Exists(inputPdf))
        {
            CreatePlaceholderPdf(inputPdf);
        }

        // Make sure the output directory exists.
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor to pull images that are defined in the PDF resources.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            // Retrieve images that are stored as resources (not inline).
            extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"image-{imageIndex}.png");
                // Save the image in its original format; the file extension can stay .png for consistency.
                extractor.GetNextImage(outputPath);
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }

    // Creates a minimal PDF file so the sample runs without requiring an external file.
    private static void CreatePlaceholderPdf(string path)
    {
        var doc = new Document();
        // Add an empty page – the document does not need any content for the extractor to work.
        doc.Pages.Add();
        doc.Save(path);
    }
}
