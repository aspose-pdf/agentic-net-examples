using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "GifPages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            using (PdfConverter converter = new PdfConverter())
            {
                // Load and bind the PDF document
                converter.BindPdf(pdfPath);

                // Define the page range to convert (pages 1‑3)
                converter.StartPage = 1;
                converter.EndPage   = 3;

                // Set conversion resolution to 150 DPI (Resolution object required)
                converter.Resolution = new Resolution(150);

                // Prepare the conversion
                converter.DoConvert();

                // Convert each page to a GIF image. GetNextImage infers the format from the file extension.
                for (int page = 1; page <= 3; page++)
                {
                    string outPath = Path.Combine(outputDir, $"Page_{page}.gif");
                    converter.GetNextImage(outPath); // saves the current page as GIF
                    Console.WriteLine($"Saved page {page} as GIF: {outPath}");
                }

                // Release any internal resources
                converter.Close();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
