using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Directory containing the SVG file
        string dataDir = @"YOUR_DATA_DIRECTORY";
        // Input SVG file path
        string svgPath = Path.Combine(dataDir, "input.svg");
        // Output directory for BMP images
        string outputDir = Path.Combine(dataDir, "BmpOutput");

        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"SVG file not found: {svgPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the SVG file into a PDF Document
        using (Document pdfDoc = new Document(svgPath, new SvgLoadOptions()))
        {
            // Initialize the PdfConverter facade
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the PDF document to the converter
                converter.BindPdf(pdfDoc);
                // Set the page range to convert (all pages)
                converter.StartPage = 1;
                converter.EndPage   = pdfDoc.Pages.Count;
                // Prepare the converter
                converter.DoConvert();

                int pageNumber = 1;
                // Iterate through each page and save as BMP
                while (converter.HasNextImage())
                {
                    string bmpFile = Path.Combine(outputDir, $"page_{pageNumber}.bmp");
                    // Save the current page as BMP
                    converter.GetNextImage(bmpFile, ImageFormat.Bmp);
                    pageNumber++;
                }

                // Release resources held by the converter
                converter.Close();
            }
        }

        Console.WriteLine("SVG to BMP conversion completed.");
    }
}