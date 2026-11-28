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
        const string pdfPath = "input.pdf";
        const string bmpPath = "page41.bmp";
        const int pageNumber = 41;
        const int resolutionDpi = 150; // reasonable resolution for bitmap output

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Temporary TIFF file that will be converted to BMP.
        string tempTiff = Path.Combine(Path.GetTempPath(), $"page{pageNumber}.tiff");

        try
        {
            // Use PdfConverter to render the required page.
            using (var converter = new PdfConverter())
            {
                converter.BindPdf(pdfPath);
                converter.StartPage = pageNumber;
                converter.EndPage   = pageNumber;
                converter.Resolution = new Resolution(resolutionDpi);
                converter.DoConvert();

                // Save the selected page as a single‑page TIFF (SaveAsPNG is not supported).
                converter.SaveAsTIFF(tempTiff);
            }

            // Convert the TIFF to BMP using System.Drawing (cross‑platform via System.Drawing.Common).
            using (Image img = Image.FromFile(tempTiff))
            {
                img.Save(bmpPath, ImageFormat.Bmp);
            }

            Console.WriteLine($"Page {pageNumber} saved as BMP to '{bmpPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary TIFF file.
            if (File.Exists(tempTiff))
            {
                try { File.Delete(tempTiff); } catch { /* ignore */ }
            }
        }
    }
}
