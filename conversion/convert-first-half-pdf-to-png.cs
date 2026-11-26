using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // PngDevice and Resolution for image rendering

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "PageImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdfPath))
            {
                // Aspose.Pdf uses 1‑based page indexing
                int totalPages = doc.Pages.Count;
                int halfPages = totalPages / 2; // integer division, floors if odd

                // Prepare a 200 DPI resolution object
                Resolution dpi200 = new Resolution(200);

                // Iterate over the first half of the pages
                for (int pageNumber = 1; pageNumber <= halfPages; pageNumber++)
                {
                    string outputPath = Path.Combine(outputFolder, $"Page_{pageNumber}.png");

                    // Render each page to PNG; this operation requires GDI+ (Windows only)
                    try
                    {
                        using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
                        {
                            PngDevice pngDevice = new PngDevice(dpi200);
                            pngDevice.Process(doc.Pages[pageNumber], imageStream);
                        }

                        Console.WriteLine($"Saved page {pageNumber} as PNG → {outputPath}");
                    }
                    catch (TypeInitializationException)
                    {
                        Console.WriteLine("Image rendering requires GDI+ and is only supported on Windows. Skipping remaining pages.");
                        break;
                    }
                    catch (DllNotFoundException)
                    {
                        Console.WriteLine("GDI+ library not found. Image rendering is unavailable on this platform.");
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