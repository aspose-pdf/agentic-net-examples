using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "TiffPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Apply custom font substitution: replace Symbol with Arial Unicode MS
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("Symbol", "Arial Unicode MS"));

            // Convert each page to a single‑page TIFF file using PdfConverter (cross‑platform, no System.Drawing)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                using (PdfConverter converter = new PdfConverter())
                {
                    converter.BindPdf(pdfDoc);
                    // Optional: set resolution for better quality
                    converter.Resolution = new Resolution(150);
                    // Convert only the current page
                    converter.StartPage = pageNumber;
                    converter.EndPage   = pageNumber;
                    converter.DoConvert();

                    string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.tiff");
                    converter.SaveAsTIFF(outputPath);
                    Console.WriteLine($"Saved page {pageNumber} as TIFF: {outputPath}");
                }
            }
        }

        Console.WriteLine("PDF to TIFF conversion completed.");
    }
}
