using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "preview_images";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize PdfConverter facade with the loaded document
            using (PdfConverter converter = new PdfConverter(pdfDocument))
            {
                // Set the page range for the preview (pages 1 to 3)
                converter.StartPage = 1;   // minimal value is 1
                converter.EndPage   = 3;   // inclusive end page

                // Prepare the converter for image extraction
                converter.DoConvert();

                int imageIndex = 1;
                // Iterate through available images and save each as JPEG
                while (converter.HasNextImage())
                {
                    string outputPath = Path.Combine(outputFolder, $"page_{imageIndex}.jpg");
                    // GetNextImage saves the image using the default JPEG format
                    converter.GetNextImage(outputPath);
                    imageIndex++;
                }

                // Release resources held by the converter
                converter.Close();
            }
        }

        Console.WriteLine("PDF pages 1‑3 have been converted to JPEG images.");
    }
}