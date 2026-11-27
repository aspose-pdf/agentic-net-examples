using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // Resolution, PageCoordinateType, etc.

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputBmp = "page3.bmp";
        const string tempTiff = "page3.tiff";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Convert the PDF page to an image using PdfConverter (cross‑platform API)
            using (PdfConverter converter = new PdfConverter())
            {
                converter.BindPdf(inputPdf);
                converter.StartPage = 3;                     // only page 3
                converter.EndPage   = 3;
                // Correct enum: PageCoordinateType (CropBox, MediaBox, …)
                converter.CoordinateType = PageCoordinateType.CropBox; // crop to CropBox
                // Resolution struct also lives in Aspose.Pdf.Devices
                converter.Resolution = new Resolution(150); // optional DPI setting
                converter.DoConvert();
                converter.SaveAsTIFF(tempTiff);            // intermediate TIFF file
            }

            // Load the intermediate TIFF and save it as BMP (System.Drawing types)
            using (System.Drawing.Image img = System.Drawing.Image.FromFile(tempTiff))
            {
                img.Save(outputBmp, System.Drawing.Imaging.ImageFormat.Bmp);
            }

            // Clean up the temporary TIFF file
            if (File.Exists(tempTiff))
                File.Delete(tempTiff);

            Console.WriteLine($"Page 3 saved as BMP: {outputBmp}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
