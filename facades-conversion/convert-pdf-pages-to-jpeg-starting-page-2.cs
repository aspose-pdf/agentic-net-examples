using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "Images";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        using (var converter = new PdfConverter())
        {
            // Bind the source PDF document
            converter.BindPdf(inputPath);

            // Convert from page 2 to the last page (EndPage left unset)
            converter.StartPage = 2;

            // Prepare the conversion – no arguments are required
            converter.DoConvert();

            int pageNumber = converter.StartPage;
            while (converter.HasNextImage())
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNumber}.jpg");
                // Write the current page as JPEG using the default settings
                converter.GetNextImage(outPath, ImageFormat.Jpeg);
                pageNumber++;
            }

            converter.Close();
        }

        Console.WriteLine("PDF pages converted to JPEG images successfully.");
    }
}
