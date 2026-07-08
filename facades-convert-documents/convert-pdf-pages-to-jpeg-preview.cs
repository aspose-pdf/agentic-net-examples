using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging; // ImageFormat for JPEG

class PdfToJpegPreview
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // source PDF
        const string outputFolder  = "PreviewImages";     // folder for JPEGs

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfConverter (Facade) to convert pages to JPEG
        using (Aspose.Pdf.Facades.PdfConverter converter = new Aspose.Pdf.Facades.PdfConverter())
        {
            // Bind the PDF file
            converter.BindPdf(inputPdfPath);

            // Set the page range for preview (pages 1 to 3)
            converter.StartPage = 1;   // minimal value is 1
            converter.EndPage   = 3;   // inclusive end page

            // Prepare the converter
            converter.DoConvert();

            int imageIndex = 1;
            while (converter.HasNextImage())
            {
                // Build output file name: e.g., PreviewImages/page1.jpg
                string outputPath = Path.Combine(outputFolder, $"page{imageIndex}.jpg");

                // Save the next image as JPEG (default format is JPEG)
                converter.GetNextImage(outputPath, ImageFormat.Jpeg);

                imageIndex++;
            }
        }

        Console.WriteLine("Preview JPEG images have been created.");
    }
}