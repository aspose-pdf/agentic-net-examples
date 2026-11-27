using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // for Resolution struct
using Aspose.Pdf.Text;   // for FontRepository and SimpleFontSubstitution

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";
        const string outputDir = "output_images";
        const string fontPath  = @"C:\Fonts\CustomFont.ttf"; // full path to the TrueType font

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Register the custom TrueType font so that the converter can use it when the PDF references it.
        // In recent Aspose.PDF versions FontRepository.AddFont was removed; use a substitution instead.
        // The wildcard "*" maps any missing font name to the supplied custom font.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", fontPath));

        // Convert pages 5 and 6 to separate high‑quality TIFF files (300 DPI).
        for (int pageNumber = 5; pageNumber <= 6; pageNumber++)
        {
            string outPath = Path.Combine(outputDir, $"page_{pageNumber}.tiff");

            using (PdfConverter converter = new PdfConverter())
            {
                // Load the source PDF.
                converter.BindPdf(pdfPath);

                // Restrict conversion to a single page.
                converter.StartPage = pageNumber;
                converter.EndPage   = pageNumber;

                // Set rasterization resolution to 300 DPI.
                converter.Resolution = new Resolution(300);

                // Perform the conversion.
                converter.DoConvert();

                // Save the page as a TIFF image. The output is a lossless, high‑quality TIFF.
                converter.SaveAsTIFF(outPath);
            }

            Console.WriteLine($"Page {pageNumber} saved to {outPath}");
        }
    }
}
