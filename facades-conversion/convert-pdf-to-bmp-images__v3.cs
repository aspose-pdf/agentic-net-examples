using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "BmpPages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // PdfConverter implements IDisposable – wrap it in a using block
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document to the converter
            converter.BindPdf(inputPdf);

            // Start the conversion process (default settings)
            converter.DoConvert();

            int pageNumber = 1;

            // Iterate through each generated image
            while (converter.HasNextImage())
            {
                string outPath = Path.Combine(outputFolder, $"Page_{pageNumber}.bmp");

                // GetNextImage requires a destination stream (and optionally an ImageFormat).
                // Write the image directly as BMP into a memory stream and then save the bytes.
                using (MemoryStream ms = new MemoryStream())
                {
                    // The overload (Stream, ImageFormat) writes the image in the specified format.
                    converter.GetNextImage(ms, ImageFormat.Bmp);
                    File.WriteAllBytes(outPath, ms.ToArray());
                }

                Console.WriteLine($"Saved BMP: {outPath}");
                pageNumber++;
            }
        }
    }
}
