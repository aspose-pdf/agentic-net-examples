using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "OutputImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Initialize PdfConverter facade
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdf);

            // Retrieve total page count from the underlying Document
            int totalPages = converter.Document.Pages.Count;

            // Process pages in reverse order
            for (int pageIndex = totalPages; pageIndex >= 1; pageIndex--)
            {
                // Set the range to a single page
                converter.StartPage = pageIndex;
                converter.EndPage   = pageIndex;

                // Prepare conversion for the specified page
                converter.DoConvert();

                // Build output file path
                string outputPath = Path.Combine(outputFolder, $"page_{pageIndex}.png");

                // Save the page as PNG
                converter.GetNextImage(outputPath, ImageFormat.Png);
            }

            // Release resources held by the converter
            converter.Close();
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}