using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Create a temporary folder for extracted images
        string tempFolder = Path.Combine(Path.GetTempPath(), "PdfImages_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        try
        {
            // Initialize the extractor and bind the PDF file
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(pdfPath);

                // Use the default extraction mode (DefinedInResources)
                extractor.ExtractImage();

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    // Build output file name (preserves original image format)
                    string outputFile = Path.Combine(tempFolder, $"image_{imageIndex}.pdf");
                    extractor.GetNextImage(outputFile);
                    imageIndex++;
                }
            }

            Console.WriteLine($"Images extracted to: {tempFolder}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}