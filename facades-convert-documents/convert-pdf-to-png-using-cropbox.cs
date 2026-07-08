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
        const string outputDir = "output_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Initialize PdfConverter facade
        using (PdfConverter converter = new PdfConverter())
        {
            // Recent Aspose.Pdf versions use CropBox by default; no need to set CoordinateType
            // If a specific enum is required, use the fully‑qualified name:
            // converter.CoordinateType = Aspose.Pdf.CoordinateType.CropBox;

            // Bind the source PDF file
            converter.BindPdf(inputPdf);

            // Prepare conversion
            converter.DoConvert();

            int pageIndex = 1;
            while (converter.HasNextImage())
            {
                string outPath = Path.Combine(outputDir, $"page_{pageIndex}.png");
                converter.GetNextImage(outPath, ImageFormat.Png);
                pageIndex++;
            }
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}
