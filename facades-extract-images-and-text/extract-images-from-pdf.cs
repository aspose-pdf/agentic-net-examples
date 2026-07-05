using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string pdfPath = "input.pdf";

        // Verify the source file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Create a temporary folder for extracted images
        string tempFolder = Path.Combine(Path.GetTempPath(), "PdfImages_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Use PdfExtractor (Facade) to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document
            extractor.BindPdf(pdfPath);

            // Use default extraction mode (DefinedInResources) – no need to set explicitly
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                // Build output file name (e.g., image-1.pdf, image-2.pdf, ...)
                string outputFile = Path.Combine(tempFolder, $"image-{imageIndex}.pdf");

                // Extract the next image to the file
                bool success = extractor.GetNextImage(outputFile);
                if (!success)
                {
                    Console.Error.WriteLine($"Failed to extract image #{imageIndex}");
                }

                imageIndex++;
            }
        }

        Console.WriteLine($"Images extracted to temporary folder: {tempFolder}");
    }
}