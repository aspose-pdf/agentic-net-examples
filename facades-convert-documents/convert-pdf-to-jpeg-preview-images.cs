using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "preview_images";

        // Verify source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfConverter (Facade) to render pages as JPEG images
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF file
            converter.BindPdf(inputPdf);

            // Set the page range for the preview (pages 1‑3)
            converter.StartPage = 1;   // minimal value is 1
            converter.EndPage   = 3;

            // Prepare the converter
            converter.DoConvert();

            int imageIndex = 1;
            // Iterate through generated images
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"page_{imageIndex}.jpg");
                // Save the current image; default format is JPEG
                converter.GetNextImage(outputPath);
                imageIndex++;
            }
        }

        Console.WriteLine("Preview JPEG images have been saved.");
    }
}