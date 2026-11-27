using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "page7.gif";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the converter
        PdfConverter converter = new PdfConverter();
        converter.BindPdf(inputPath);

        // Convert only page 7
        converter.StartPage = 7;
        converter.EndPage   = 7;

        // Set resolution to 72 DPI (Resolution is a struct from Aspose.Pdf.Devices)
        converter.Resolution = new Resolution(72);

        // NOTE: PdfConverter does not expose a direct CropBox property.
        // If a specific crop rectangle is required, you would need to use a
        // device class (e.g., GifDevice) with TiffSettings/CoordinateType, or
        // modify the PDF page before conversion. The current version of the
        // API therefore omits the CropBox setting.

        // Convert the selected page to an image. GetNextImage infers the
        // image format from the file extension, so a .gif extension produces a GIF.
        converter.GetNextImage(outputPath);

        // Release resources
        converter.Close();

        Console.WriteLine($"Page 7 saved as GIF to '{outputPath}'.");
    }
}
