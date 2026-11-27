using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // PdfConverter implements IDisposable, wrap in using
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the source PDF
                converter.BindPdf(inputPdf);

                // Optional: set resolution (e.g., 150 DPI)
                // converter.Resolution = new Aspose.Pdf.Devices.Resolution(150);

                // Start the conversion process
                converter.DoConvert();

                int pageIndex = 1;
                // Retrieve each page as a JPEG image using the overload that writes directly to a file
                while (converter.HasNextImage())
                {
                    string outPath = Path.Combine(outputDir, $"Page_{pageIndex}.jpg");
                    // The format is inferred from the file extension (.jpg => JPEG)
                    converter.GetNextImage(outPath);
                    Console.WriteLine($"Saved {outPath}");
                    pageIndex++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
