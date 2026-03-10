using System;
using System.IO;
using Aspose.Pdf;                           // Document, PsLoadOptions
using Aspose.Pdf.Facades;                  // PdfConverter
using Aspose.Pdf.Devices;                  // Resolution

class Program
{
    static void Main()
    {
        const string psInputPath  = "input.ps";      // PS file to convert
        const string outputFolder = "PngImages";     // Folder for PNG files

        // Verify input file exists
        if (!File.Exists(psInputPath))
        {
            Console.Error.WriteLine($"File not found: {psInputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PostScript file into a PDF Document (PS is input‑only)
        using (Document pdfDoc = new Document(psInputPath, new PsLoadOptions()))
        {
            // Initialize the PdfConverter facade
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the loaded PDF document
                converter.BindPdf(pdfDoc);

                // Set resolution (default 150 DPI). Use Resolution object as required by the API.
                converter.Resolution = new Resolution(300); // higher resolution for clearer PNGs

                // Define the page range to convert (all pages)
                converter.StartPage = 1;
                converter.EndPage   = pdfDoc.Pages.Count;

                // Prepare the converter
                converter.DoConvert();

                int pageIndex = 1;
                // Loop through all generated images
                while (converter.HasNextImage())
                {
                    // Build output file name
                    string pngPath = Path.Combine(outputFolder, $"page_{pageIndex}.png");

                    // Save the current page as PNG. The overload without ImageFormat infers the format from the file extension.
                    converter.GetNextImage(pngPath);

                    pageIndex++;
                }
            }
        }

        Console.WriteLine("Conversion completed. PNG images are saved in '" + outputFolder + "'.");
    }
}
