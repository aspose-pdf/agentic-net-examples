using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "Images";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Pages are 1‑based in Aspose.Pdf
                for (int pageNumber = 2; pageNumber <= 4 && pageNumber <= pdfDoc.Pages.Count; pageNumber++)
                {
                    Page page = pdfDoc.Pages[pageNumber];

                    // JpegDevice does NOT implement IDisposable – instantiate directly
                    try
                    {
                        // Set resolution to 250 DPI
                        Resolution resolution = new Resolution(250);
                        // Quality 100 (max)
                        JpegDevice jpegDevice = new JpegDevice(resolution, 100);

                        string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.jpg");
                        using (FileStream imageStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                        {
                            jpegDevice.Process(page, imageStream);
                        }
                        Console.WriteLine($"Saved page {pageNumber} as JPEG to '{outputPath}'.");
                    }
                    catch (TypeInitializationException)
                    {
                        Console.WriteLine("JPEG conversion requires GDI+ and is only supported on Windows. Skipping page conversion.");
                        break;
                    }
                    catch (DllNotFoundException)
                    {
                        Console.WriteLine("GDI+ library not found. JPEG conversion is Windows‑only. Skipping page conversion.");
                        break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
