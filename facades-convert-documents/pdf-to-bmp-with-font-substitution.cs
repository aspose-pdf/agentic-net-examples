using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class PdfToBmpConverter
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "BmpImages";
        const string tempPdfPath = "temp_substituted.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load original PDF and apply font substitution (Times New Roman → Calibri)
        using (Document originalDoc = new Document(inputPdfPath))
        {
            PdfSaveOptions saveOpts = new PdfSaveOptions
            {
                // When a font is not available, Aspose.Pdf will use this default font
                DefaultFontName = "Calibri"
            };

            // Save to a temporary PDF with the substitution applied
            originalDoc.Save(tempPdfPath, saveOpts);
        }

        // Load the temporary PDF (now using Calibri where Times New Roman was missing)
        using (Document substitutedDoc = new Document(tempPdfPath))
        {
            // Initialize PdfConverter with the substituted document
            using (PdfConverter converter = new PdfConverter(substitutedDoc))
            {
                // Set conversion range (all pages)
                converter.StartPage = 1; // 1‑based indexing
                converter.EndPage = substitutedDoc.Pages.Count;

                // Set desired resolution (e.g., 300 DPI)
                converter.Resolution = new Resolution(300);

                // Prepare the converter
                converter.DoConvert();

                // Loop through each page and save as BMP
                for (int pageNumber = converter.StartPage; pageNumber <= converter.EndPage; pageNumber++)
                {
                    // Create a BmpDevice with the same resolution
                    BmpDevice bmpDevice = new BmpDevice(converter.Resolution);

                    // Define output BMP file name
                    string bmpPath = Path.Combine(outputFolder, $"page_{pageNumber}.bmp");

                    // Convert the current page to BMP and write to file
                    using (FileStream bmpStream = new FileStream(bmpPath, FileMode.Create))
                    {
                        bmpDevice.Process(converter.Document.Pages[pageNumber], bmpStream);
                    }

                    Console.WriteLine($"Saved BMP: {bmpPath}");
                }

                // Release resources held by the converter
                converter.Close();
            }
        }

        // Clean up temporary PDF
        try
        {
            File.Delete(tempPdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Could not delete temporary file: {ex.Message}");
        }

        Console.WriteLine("PDF to BMP conversion completed.");
    }
}
