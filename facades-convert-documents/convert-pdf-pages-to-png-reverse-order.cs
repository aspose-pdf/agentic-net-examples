using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "PngPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // PdfConverter implements IDisposable, so wrap it in using as well
                using (PdfConverter converter = new PdfConverter())
                {
                    // Bind the PDF document to the converter
                    converter.BindPdf(pdfDoc);

                    // Iterate pages in reverse order (last page to first page)
                    for (int pageNumber = pdfDoc.Pages.Count; pageNumber >= 1; pageNumber--)
                    {
                        // Build the output PNG file name
                        string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");

                        // Render the page to PNG using PngDevice (cross‑platform rasterizer)
                        using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
                        {
                            PngDevice pngDevice = new PngDevice();
                            pngDevice.Process(pdfDoc.Pages[pageNumber], imageStream);
                        }

                        Console.WriteLine($"Saved page {pageNumber} as PNG to '{outputPath}'.");
                    }
                }
            }

            Console.WriteLine("All pages have been converted to PNG images in reverse order.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}