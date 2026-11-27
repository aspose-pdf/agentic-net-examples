using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;          // for Resolution
using Aspose.Pdf.Text;            // for font substitution

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "TiffPages";
        const string customFontName = "MyCustomFont";               // logical name used in PDF
        const string customFontPath = @"C:\Fonts\MyCustomFont.ttf"; // physical TTF/OTF file

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Register the custom font for substitution.
        // In recent Aspose.PDF versions AddFont was removed; use Substitutions instead.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution(customFontName, customFontPath));

        // PdfConverter does not implement IDisposable, so we manage its lifetime manually.
        PdfConverter converter = new PdfConverter();

        // Load the source PDF.
        converter.BindPdf(inputPdfPath);

        // Define the page range to be converted.
        converter.StartPage = 5;
        converter.EndPage   = 7;

        // Set the resolution (DPI) for the output images.
        converter.Resolution = new Resolution(150);

        // Convert each page in the defined range to a separate single‑page TIFF image.
        for (int pageNum = converter.StartPage; pageNum <= converter.EndPage; pageNum++)
        {
            // Restrict conversion to a single page.
            converter.StartPage = pageNum;
            converter.EndPage   = pageNum;

            // Prepare the conversion for the current page.
            converter.DoConvert();

            string outputPath = Path.Combine(outputFolder, $"Page_{pageNum}.tiff");
            // Save the current page as a TIFF file. SaveAsTIFF creates a single‑page TIFF when the
            // page range contains only one page.
            converter.SaveAsTIFF(outputPath);
            Console.WriteLine($"Page {pageNum} saved as TIFF: {outputPath}");
        }

        // Release resources held by the converter.
        converter.Close();
        Console.WriteLine("Conversion completed.");
    }
}
