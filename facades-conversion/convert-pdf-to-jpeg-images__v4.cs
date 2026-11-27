using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPattern = "page_{0}.jpg"; // {0} will be replaced by page number

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // PdfConverter implements IDisposable – wrap it in a using block.
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF document.
            converter.BindPdf(inputPdfPath);

            // Set the desired resolution (dots per inch).
            converter.Resolution = new Resolution(96);

            // Convert the whole document.
            converter.StartPage = 1;
            converter.EndPage   = converter.PageCount; // all pages
            converter.DoConvert();

            // Save each page as a JPEG image using the file‑extension inference overload.
            for (int page = 1; page <= converter.PageCount; page++)
            {
                string outputPath = string.Format(outputPattern, page);
                // GetNextImage infers the image format from the file extension (".jpg" → JPEG).
                converter.GetNextImage(outputPath);
                Console.WriteLine($"Saved page {page} as {outputPath}");
            }
        }
    }
}
