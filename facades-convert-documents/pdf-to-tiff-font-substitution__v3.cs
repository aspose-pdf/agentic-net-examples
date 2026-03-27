using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.tiff";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Substitute Symbol font with Arial Unicode MS for compatibility
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Symbol", "Arial Unicode MS"));

        using (PdfConverter converter = new PdfConverter())
        {
            converter.BindPdf(inputPath);
            // Optional: configure pages
            converter.StartPage = 1;
            converter.EndPage = 0; // 0 = all pages
            // Resolution property removed because the Resolution class is not available in the current Aspose.Pdf version.
            // The default resolution (300 DPI) will be used, or you can set it via SaveOptions if needed.
            converter.DoConvert();
            converter.SaveAsTIFF(outputPath);
        }

        Console.WriteLine($"TIFF image saved to '{outputPath}'.");
    }
}
