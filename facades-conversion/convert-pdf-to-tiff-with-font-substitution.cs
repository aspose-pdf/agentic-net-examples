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
        const string inputPdf = "input.pdf";
        const string outputDir = "output_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // --------------------------------------------------------------------
        // Add a custom font substitution.
        // Map a missing font name (e.g., "MyMissingFont") to an existing font.
        // FontRepository.Substitutions expects SimpleFontSubstitution objects.
        // --------------------------------------------------------------------
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("MyMissingFont", "Arial"));

        // --------------------------------------------------------------------
        // Initialize PdfConverter, bind the PDF, and configure conversion.
        // --------------------------------------------------------------------
        PdfConverter converter = new PdfConverter();
        converter.BindPdf(inputPdf);

        // Convert all pages.
        converter.StartPage = 1;
        converter.EndPage   = converter.PageCount;
        converter.Resolution = new Resolution(300); // DPI

        // Perform the conversion.
        converter.DoConvert();

        // --------------------------------------------------------------------
        // Save the result as a multi‑page TIFF file (cross‑platform).
        // --------------------------------------------------------------------
        string outputPath = Path.Combine(outputDir, "output.tiff");
        converter.SaveAsTIFF(outputPath);
        converter.Close();

        Console.WriteLine($"Conversion completed. TIFF file saved at '{outputPath}'.");
    }
}
