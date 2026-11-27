using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution struct

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "Images";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // PdfConverter implements IDisposable, so we wrap it in a using block.
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file.
            converter.BindPdf(inputPdfPath);

            // Configure the conversion range.
            converter.StartPage = 1;                 // first page to convert
            converter.EndPage   = 5;                 // last page to convert
            // Cropping can be controlled via the CropBox property if needed.
            // In newer Aspose.Pdf versions the CoordinateType enum may be missing.

            // Set the resolution (DPI) for the output images.
            converter.Resolution = new Resolution(150);

            // Begin the conversion process.
            converter.DoConvert();

            int totalPages = converter.EndPage - converter.StartPage + 1;
            for (int i = 0; i < totalPages; i++)
            {
                // Get the image for the next page. In recent versions GetNextImage
                // requires a destination stream instead of returning an Image directly.
                using (MemoryStream ms = new MemoryStream())
                {
                    converter.GetNextImage(ms); // writes JPEG data into the stream
                    ms.Position = 0; // reset for reading
                    using (Image pageImage = Image.FromStream(ms))
                    {
                        int pageNumber = converter.StartPage + i;
                        string outputPath = Path.Combine(outputFolder, $"Page_{pageNumber}.jpg");
                        pageImage.Save(outputPath, ImageFormat.Jpeg);
                        Console.WriteLine($"Saved page {pageNumber} as JPEG: {outputPath}");
                    }
                }
            }

            // In recent Aspose.Pdf versions EndConvert() is no longer required.
        }
    }
}
