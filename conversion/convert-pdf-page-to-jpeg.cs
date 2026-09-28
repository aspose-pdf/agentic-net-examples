using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJpeg = "page1.jpg";
        const int pageNumber = 1; // 1‑based page index

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Validate the requested page number (Aspose.Pdf uses 1‑based indexing)
                if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Page {pageNumber} is out of range. Document has {pdfDoc.Pages.Count} pages.");
                    return;
                }

                // Retrieve the specific page
                Page page = pdfDoc.Pages[pageNumber];

                // Create a file stream for the JPEG output
                using (FileStream imgStream = new FileStream(outputJpeg, FileMode.Create, FileAccess.Write))
                {
                    // JpegDevice uses the default DPI when constructed without a Resolution argument.
                    JpegDevice jpegDevice = new JpegDevice();
                    // Process overload that accepts a Page and an output stream (2 arguments).
                    jpegDevice.Process(page, imgStream);
                }
            }

            Console.WriteLine($"Page {pageNumber} saved as JPEG to '{outputJpeg}'.");
        }
        // HTML/image conversions may require GDI+ (Windows only)
        catch (TypeInitializationException)
        {
            Console.WriteLine("Image conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (DllNotFoundException)
        {
            Console.WriteLine("GDI+ library not found. Image conversion is Windows‑only.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
