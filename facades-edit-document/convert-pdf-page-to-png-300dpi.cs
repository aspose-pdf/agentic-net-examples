using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "page7.png";
        const int dpi = 300;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Retrieve page size to calculate pixel dimensions for the desired DPI
        double pageWidthPoints;
        double pageHeightPoints;

        using (Document doc = new Document(inputPath))
        {
            if (doc.Pages.Count < 7)
            {
                Console.Error.WriteLine("The PDF does not contain page 7.");
                return;
            }

            Page page = doc.Pages[7]; // 1‑based indexing
            pageWidthPoints = page.PageInfo.Width;
            pageHeightPoints = page.PageInfo.Height;
        }

        int pixelWidth = (int)Math.Round(pageWidthPoints * dpi / 72.0);
        int pixelHeight = (int)Math.Round(pageHeightPoints * dpi / 72.0);

        PdfConverter converter = new PdfConverter();
        converter.BindPdf(inputPath);
        // Convert only page 7
        converter.StartPage = 7;
        converter.EndPage = 7;
        converter.DoConvert();

        // Save the extracted page as a PNG with the calculated pixel dimensions
        converter.GetNextImage(outputPath, ImageFormat.Png, pixelWidth, pixelHeight);

        Console.WriteLine($"Page 7 saved as high‑resolution PNG: {outputPath}");
    }
}