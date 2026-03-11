using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class PsToPngConverter
{
    static void Main()
    {
        const string psInputPath = "input.ps";
        const string outputFolder = "PngPages";

        if (!File.Exists(psInputPath))
        {
            Console.Error.WriteLine($"PostScript file not found: {psInputPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PS file into a PDF Document (PS is input‑only)
            using (Document pdfDoc = new Document(psInputPath, new PsLoadOptions()))
            {
                // Initialize the PdfConverter facade with the loaded document
                using (PdfConverter converter = new PdfConverter(pdfDoc))
                {
                    // Set resolution using Aspose.Pdf.Devices.Resolution
                    converter.Resolution = new Resolution(300); // 300 DPI for clearer PNGs

                    // Convert all pages
                    converter.StartPage = 1;
                    converter.EndPage   = pdfDoc.Pages.Count;

                    // Prepare the converter
                    converter.DoConvert();

                    int pageIndex = 1;
                    // Loop through generated images
                    while (converter.HasNextImage())
                    {
                        string pngPath = Path.Combine(outputFolder, $"page_{pageIndex}.png");
                        // Save each page as PNG – format inferred from file extension
                        converter.GetNextImage(pngPath);
                        pageIndex++;
                    }
                }
            }

            Console.WriteLine($"Conversion completed. PNG files are in '{outputFolder}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
