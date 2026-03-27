using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

public class ConvertPdfPagesToJpeg
{
    public static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("File not found: " + inputPath);
            return;
        }

        // Create and configure the PDF converter
        PdfConverter converter = new PdfConverter();
        converter.BindPdf(inputPath);
        // Convert pages 1 through 10
        converter.StartPage = 1;
        converter.EndPage = 10;
        // Set the resolution to 150 DPI (use Resolution object)
        converter.Resolution = new Resolution(150);
        // Perform the conversion preparation (CropBox is used by default)
        converter.DoConvert();

        int pageNumber = 1;
        while (converter.HasNextImage())
        {
            string outputFile = $"page{pageNumber}.jpg";
            // Save the current page as a JPEG image
            converter.GetNextImage(outputFile, ImageFormat.Jpeg);
            Console.WriteLine("Saved " + outputFile);
            pageNumber++;
        }

        // Release resources used by the converter
        converter.Close();
    }
}
