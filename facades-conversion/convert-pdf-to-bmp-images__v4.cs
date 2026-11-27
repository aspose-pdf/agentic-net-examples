using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "BmpPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // Load PDF document
            using var doc = new Document(inputPdf);

            // Initialize the converter and bind the document
            using var converter = new PdfConverter();
            converter.BindPdf(doc);

            // Configure conversion parameters
            converter.StartPage = 1;
            converter.EndPage = doc.Pages.Count;
            converter.Resolution = new Resolution(150); // DPI – adjust as needed

            // Prepare the conversion
            converter.DoConvert();

            // Export each page as a BMP image
            int pageNumber = converter.StartPage;
            while (converter.HasNextImage())
            {
                string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.bmp");
                converter.GetNextImage(outPath, ImageFormat.Bmp);
                Console.WriteLine($"Saved {outPath}");
                pageNumber++;
            }

            // Finalize the conversion process
            converter.Close();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
