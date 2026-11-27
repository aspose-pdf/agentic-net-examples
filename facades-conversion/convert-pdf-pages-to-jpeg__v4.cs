using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // PdfConverter implements IDisposable, so wrap it in a using block
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdf);

            // Set the desired resolution (DPI). Use the Resolution struct from Aspose.Pdf.Devices.
            converter.Resolution = new Resolution(300);

            // Initialise the conversion process
            converter.DoConvert();

            int pageNumber = 1;
            // Use HasNextImage() to check for remaining pages and GetNextImage(Stream) to retrieve them.
            while (converter.HasNextImage())
            {
                using (var ms = new MemoryStream())
                {
                    // Write the next page image into the memory stream.
                    converter.GetNextImage(ms);

                    // Reset the stream position before creating the Image.
                    ms.Position = 0;
                    using (Image pageImage = Image.FromStream(ms))
                    {
                        string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.jpg");
                        pageImage.Save(outputPath, ImageFormat.Jpeg);
                    }
                }
                pageNumber++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
