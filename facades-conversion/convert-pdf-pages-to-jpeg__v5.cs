using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "Images";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // PdfConverter does not implement IDisposable, so we manage its lifetime manually.
        var converter = new PdfConverter();

        try
        {
            // Bind the PDF document to the converter.
            converter.BindPdf(pdfPath);

            // Configure the conversion range and resolution.
            converter.StartPage = 1;
            converter.EndPage = converter.PageCount; // convert all pages
            converter.Resolution = new Resolution(150); // DPI – adjust as needed

            // Prepare the conversion.
            converter.DoConvert();

            // Iterate through the generated images.
            int pageNumber = converter.StartPage;
            while (converter.HasNextImage())
            {
                string imagePath = Path.Combine(outputDir, $"Page_{pageNumber}.jpg");
                // GetNextImage writes the current page as the specified image format.
                converter.GetNextImage(imagePath, ImageFormat.Jpeg);
                Console.WriteLine($"Saved page {pageNumber} as JPEG: {imagePath}");
                pageNumber++;
            }
        }
        finally
        {
            // Ensure resources are released.
            converter.Close();
        }
    }
}
