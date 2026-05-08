using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "Thumbnails";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Initialize the converter facade
        PdfConverter converter = new PdfConverter();
        converter.BindPdf(inputPdf);
        converter.DoConvert();

        int imageIndex = 1;
        // Extract each page as a PNG thumbnail with max 200x200 pixels
        while (converter.HasNextImage())
        {
            string outputPath = Path.Combine(outputFolder, $"thumb_{imageIndex}.png");
            // Overload: GetNextImage(string outputFile, ImageFormat format, int width, int height)
            converter.GetNextImage(outputPath, ImageFormat.Png, 200, 200);
            imageIndex++;
        }

        // Release resources
        converter.Close();

        Console.WriteLine($"Thumbnails saved to '{outputFolder}'.");
    }
}