using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "BmpPages";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Create a PdfConverter instance (Facades API)
        PdfConverter converter = new PdfConverter();

        // Bind the PDF document to the converter
        converter.BindPdf(inputPath);

        // Define the page range to convert (1‑based indexing)
        converter.StartPage = 6;
        converter.EndPage   = 10;
        // Use default resolution – no explicit call required

        // Prepare the internal image iterator
        converter.DoConvert();

        // Iterate over the selected pages and save each as BMP
        for (int page = converter.StartPage; page <= converter.EndPage; page++)
        {
            using (MemoryStream imgStream = new MemoryStream())
            {
                // GetNextImage writes the rendered page image into the supplied stream
                converter.GetNextImage(imgStream);

                // If the stream is empty, no more images are available (safety check)
                if (imgStream.Length == 0)
                    break;

                imgStream.Position = 0; // reset for reading
                using (Image img = Image.FromStream(imgStream))
                {
                    string outPath = Path.Combine(outputDir, $"Page_{page}.bmp");
                    img.Save(outPath, ImageFormat.Bmp);
                    Console.WriteLine($"Saved page {page} as BMP to '{outPath}'.");
                }
            }
        }

        // Release resources held by the converter
        converter.Close();

        Console.WriteLine($"Conversion completed. BMP files are located in '{outputDir}'.");
    }
}
