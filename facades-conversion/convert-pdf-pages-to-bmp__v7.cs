using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution

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

        // PdfConverter implements IDisposable; wrap it in a using block.
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file.
            converter.BindPdf(inputPdf);

            // Set the page range (1‑based indexing) and resolution (DPI).
            converter.StartPage = 2;   // start from page 2
            converter.EndPage   = 8;   // end at page 8 (inclusive)
            converter.Resolution = new Resolution(150); // 150 DPI

            // Prepare the conversion.
            converter.DoConvert();

            // Convert each page in the range to a BMP image.
            for (int page = converter.StartPage; page <= converter.EndPage; page++)
            {
                string outPath = Path.Combine(outputDir, $"Page_{page}.bmp");

                // Save the current page as BMP. The format is inferred from the file extension.
                converter.GetNextImage(outPath);

                Console.WriteLine($"Saved page {page} as BMP → {outPath}");
            }

            // Release resources associated with the converter.
            converter.Close();
        }
    }
}
