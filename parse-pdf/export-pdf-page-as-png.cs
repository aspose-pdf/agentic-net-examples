using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class ExportPageAsRaster
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputImgPath = "page_raster.png";
        const int pageNumber = 1;      // 1‑based index
        const int dpi = 300;           // Desired resolution in DPI

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Document implements IDisposable, wrap it in using for deterministic cleanup
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Access the required page (Aspose.Pdf uses 1‑based indexing)
                Page page = pdfDoc.Pages[pageNumber];

                // Set the raster resolution – use the Resolution class (ImageDeviceResolution does not exist)
                var resolution = new Resolution(dpi);

                // PngDevice is a concrete device and does NOT implement IDisposable – do NOT use a using block
                var pngDevice = new PngDevice(resolution)
                {
                    TransparentBackground = true // optional: keep background transparent
                };

                // Render the whole page (all vector elements combined) to the output file
                pngDevice.Process(page, outputImgPath);
            }

            Console.WriteLine($"Page {pageNumber} exported as raster image at {dpi} DPI to '{outputImgPath}'.");
        }
        // GDI+ related exceptions may be thrown on non‑Windows platforms
        catch (TypeInitializationException)
        {
            Console.WriteLine("Raster image conversion requires GDI+ and is only supported on Windows.");
        }
        catch (DllNotFoundException)
        {
            Console.WriteLine("Required GDI+ library not found. This operation is Windows‑only.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
