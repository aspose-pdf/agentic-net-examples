using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;          // Resolution
using System.Drawing.Imaging;      // ImageFormat

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "Images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        try
        {
            // PdfConverter implements IDisposable, so wrap it in a using block
            using (PdfConverter converter = new PdfConverter())
            {
                // Load the PDF document
                converter.BindPdf(inputPdf);

                // Define the page range: first five pages
                converter.StartPage = 1;
                converter.EndPage   = 5;

                // Optional: set a resolution (dots per inch). Default is 96 DPI.
                converter.Resolution = new Resolution(150);

                // Prepare the conversion – no arguments are required.
                converter.DoConvert();

                // Convert each page in the range to a JPEG image.
                int pageNumber = converter.StartPage;
                while (converter.HasNextImage())
                {
                    string outPath = Path.Combine(outputFolder, $"Page_{pageNumber}.jpg");
                    // GetNextImage writes the current page to the specified file using the given ImageFormat.
                    converter.GetNextImage(outPath, ImageFormat.Jpeg);
                    Console.WriteLine($"Saved: {outPath}");
                    pageNumber++;
                }
            }
        }
        // PdfConverter may still rely on GDI+ for image encoding; handle platform‑specific issues gracefully.
        catch (TypeInitializationException)
        {
            Console.WriteLine("Image conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
