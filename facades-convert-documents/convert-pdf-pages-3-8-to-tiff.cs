using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;   // TiffDevice resides here

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputTiff = "pages3to8.tiff";    // result TIFF file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // TiffDevice with default settings:
            // - Default resolution = 150 DPI
            // - Default CoordinateType = CropBox (already the default)
            TiffDevice tiffDevice = new TiffDevice();

            // Create output stream for the TIFF image
            using (FileStream outStream = new FileStream(outputTiff, FileMode.Create, FileAccess.Write))
            {
                // Convert pages 3 through 8 (inclusive) to TIFF
                tiffDevice.Process(pdfDoc, 3, 8, outStream);
            }
        }

        Console.WriteLine($"TIFF image created at '{outputTiff}'.");
    }
}