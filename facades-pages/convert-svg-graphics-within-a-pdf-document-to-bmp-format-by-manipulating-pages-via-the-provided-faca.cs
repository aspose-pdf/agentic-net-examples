using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;
using System.Drawing.Imaging;   // ImageFormat for BMP

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // Source PDF containing SVG graphics
        const string outputDir = "BmpOutput";          // Directory for BMP images

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Initialize the PdfConverter facade with the loaded document
            using (PdfConverter converter = new PdfConverter(pdfDoc))
            {
                // Bind the document (constructor already binds, but explicit call is safe)
                converter.BindPdf(pdfDoc);

                // Convert all pages (Aspose.Pdf uses 1‑based page indexing)
                converter.StartPage = 1;
                converter.EndPage   = pdfDoc.Pages.Count;

                // Set a higher resolution for better bitmap quality
                converter.Resolution = new Resolution(300);

                // Iterate through each page and save it as a BMP image
                for (int page = converter.StartPage; page <= converter.EndPage; page++)
                {
                    string bmpPath = Path.Combine(outputDir, $"page_{page}.bmp");
                    // Save the current page as BMP
                    converter.GetNextImage(bmpPath, ImageFormat.Bmp);
                }

                // Release any resources held by the converter
                converter.Close();
            }
        }

        Console.WriteLine("SVG graphics have been converted to BMP images successfully.");
    }
}