using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Pages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // PdfConverter implements IDisposable, so wrap it in a using block
            using (PdfConverter converter = new PdfConverter())
            {
                // Load the PDF document
                converter.BindPdf(inputPdf);

                // Set the conversion range: start at page 4, end at the last page
                converter.StartPage = 4;
                converter.EndPage   = converter.PageCount; // convert to the end

                // Optional: set resolution (dpi) for the rasterized images
                converter.Resolution = new Resolution(150);

                // Prepare the conversion
                converter.DoConvert();

                // Iterate over the generated images and save each as PNG
                int page = converter.StartPage;
                while (converter.HasNextImage())
                {
                    string outPath = Path.Combine(outputDir, $"page_{page}.png");
                    converter.GetNextImage(outPath, ImageFormat.Png);
                    Console.WriteLine($"Saved page {page} as {outPath}");
                    page++;
                }

                // Release resources explicitly (optional, Dispose will also close)
                converter.Close();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
