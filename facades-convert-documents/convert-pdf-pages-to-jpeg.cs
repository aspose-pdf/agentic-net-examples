using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "output_images";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Create the PdfConverter facade (create rule)
        PdfConverter converter = new PdfConverter();

        // Bind the PDF document to the converter (load rule)
        converter.BindPdf(inputPdfPath);

        // Perform initial conversion setup (required before extracting images)
        converter.DoConvert();

        int pageIndex = 1;
        // Iterate through each page image sequentially (process rule)
        while (converter.HasNextImage())
        {
            // Build output file name with page number suffix
            string outputPath = Path.Combine(outputFolder, $"page_{pageIndex}.jpg");

            // Save the current page as JPEG (save rule)
            converter.GetNextImage(outputPath, ImageFormat.Jpeg);

            pageIndex++;
        }

        // Release resources held by the converter
        converter.Close();
    }
}