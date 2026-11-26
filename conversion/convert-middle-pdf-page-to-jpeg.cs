using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "middle_page.jpg";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Page count uses 1‑based indexing
                int pageCount = pdfDoc.Pages.Count;
                if (pageCount == 0)
                {
                    Console.Error.WriteLine("PDF contains no pages.");
                    return;
                }

                // Compute middle page (ceil for even counts)
                int middlePageNumber = (pageCount / 2) + 1;

                // Retrieve the middle page
                Page middlePage = pdfDoc.Pages[middlePageNumber];

                // JPEG device with 200 DPI resolution and maximum quality
                Resolution resolution = new Resolution(200); // DPI for X and Y
                JpegDevice jpegDevice = new JpegDevice(resolution, 100); // quality 0‑100

                // Convert page to JPEG and save to file
                jpegDevice.Process(middlePage, outputPath);
            }

            Console.WriteLine($"Middle page saved as JPEG to '{outputPath}'.");
        }
        catch (TypeInitializationException)
        {
            // GDI+ is required for image conversion; not available on non‑Windows platforms
            Console.Error.WriteLine("JPEG conversion requires GDI+ and is only supported on Windows.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
