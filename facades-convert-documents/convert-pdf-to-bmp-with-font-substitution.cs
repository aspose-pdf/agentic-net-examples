using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing the PDF file and where BMP images will be saved
        const string dataDir = @"C:\PdfData";
        const string pdfFileName = "input.pdf";

        // Full path to the source PDF
        string pdfPath = Path.Combine(dataDir, pdfFileName);

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document (required for binding to the converter)
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Initialize the PDF converter facade
            PdfConverter converter = new PdfConverter();

            // Bind the loaded document to the converter
            converter.BindPdf(pdfDocument);

            // NOTE: In recent Aspose.Pdf versions font substitution is enabled by default.
            // If a specific property is required, it can be set via RenderingOptions when available.

            // Prepare the converter for image extraction
            converter.DoConvert();

            int pageIndex = 1;
            // Extract each page as a BMP image
            while (converter.HasNextImage())
            {
                string bmpOutputPath = Path.Combine(dataDir, $"image{pageIndex}_out.bmp");
                // Use System.Drawing.Imaging.ImageFormat for BMP output
                converter.GetNextImage(bmpOutputPath, ImageFormat.Bmp);
                pageIndex++;
            }

            // Release resources held by the converter
            converter.Close();
        }

        Console.WriteLine("PDF pages have been successfully converted to BMP images.");
    }
}
