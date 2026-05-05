using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging; // ImageFormat for BMP

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "output_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Initialize the PdfConverter facade
            PdfConverter converter = new PdfConverter();
            converter.BindPdf(pdfDoc);

            // Specify the page range (1‑based indexing)
            converter.StartPage = 2; // start page
            converter.EndPage   = 6; // end page

            // Prepare the converter
            converter.DoConvert();

            int currentPage = converter.StartPage;

            // Iterate through the selected pages and save each as BMP
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"page_{currentPage}.bmp");
                // Save the current page as BMP
                converter.GetNextImage(outputPath, ImageFormat.Bmp);
                currentPage++;
            }

            // Release resources held by the converter
            converter.Close();
        }

        Console.WriteLine("PDF to BMP conversion completed.");
    }
}