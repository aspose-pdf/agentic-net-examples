using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string outputFolder = "ExtractedImages";    // folder for JPEGs

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        try
        {
            // PdfConverter implements IDisposable – wrap it in a using block.
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the PDF file to the converter.
                converter.BindPdf(inputPdfPath);

                // Prepare internal structures for conversion.
                converter.DoConvert();

                int imageIndex = 1;
                // Iterate over all images in the PDF.
                while (converter.HasNextImage())
                {
                    // Build the output file name.
                    string outputFile = Path.Combine(outputFolder, $"image{imageIndex}.jpg");

                    // Export the current image as JPEG with quality = 85.
                    // ImageFormat is from System.Drawing.Imaging – use fully qualified name.
                    converter.GetNextImage(
                        outputFile,
                        System.Drawing.Imaging.ImageFormat.Jpeg,
                        85); // quality (0‑100)

                    imageIndex++;
                }
            }

            Console.WriteLine($"Images extracted to folder: {outputFolder}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}