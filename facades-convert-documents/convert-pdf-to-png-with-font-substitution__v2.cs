using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class PdfToPngConverter
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "PngPages";
        const string defaultFont = "Arial"; // fallback font for missing fonts

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Step 1: Load the source PDF
        using (Document srcDoc = new Document(inputPdfPath))
        {
            // Step 2: Save a temporary PDF with font substitution enabled
            // PdfSaveOptions.DefaultFontName is used for missing fonts
            PdfSaveOptions saveOpts = new PdfSaveOptions
            {
                DefaultFontName = defaultFont
            };

            string tempPdfPath = Path.Combine(outputFolder, "temp_substituted.pdf");
            srcDoc.Save(tempPdfPath, saveOpts);

            // Step 3: Load the temporary PDF into PdfConverter
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the PDF file (the one with substituted fonts)
                converter.BindPdf(tempPdfPath);

                // Set resolution using the Resolution object (DPI)
                converter.Resolution = new Resolution(150);

                // Prepare for conversion
                converter.DoConvert();

                int pageNumber = 1;
                while (converter.HasNextImage())
                {
                    string outFile = Path.Combine(outputFolder, $"page_{pageNumber}.png");
                    // Save the next page; format is inferred from the file extension
                    converter.GetNextImage(outFile);
                    Console.WriteLine($"Saved page {pageNumber} → {outFile}");
                    pageNumber++;
                }

                // Release resources used by the converter
                converter.Close();
            }

            // Clean up the temporary PDF
            try { File.Delete(tempPdfPath); } catch { /* ignore cleanup errors */ }
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}
