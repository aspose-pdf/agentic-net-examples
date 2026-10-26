using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution
using Aspose.Pdf.Text;    // for FontRepository and SimpleFontSubstitution

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTiffPath = "output.tiff";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Register a substitution: replace the Symbol font with Arial Unicode MS.
            // Resolve the system path to Arial Unicode MS (ARIALUNI.TTF).
            string arialUnicodePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Fonts),
                "ARIALUNI.TTF");

            if (!File.Exists(arialUnicodePath))
                throw new FileNotFoundException("Arial Unicode MS font file not found.", arialUnicodePath);

            FontRepository.Substitutions.Add(new SimpleFontSubstitution("Symbol", arialUnicodePath));

            // PdfConverter implements IDisposable, so use a using block
            using (PdfConverter converter = new PdfConverter())
            {
                // Load the source PDF
                converter.BindPdf(inputPdfPath);

                // Set higher resolution for better image quality (Resolution object required)
                converter.Resolution = new Resolution(300); // DPI

                // Convert all pages and save as a multi‑page TIFF
                converter.DoConvert();
                converter.SaveAsTIFF(outputTiffPath);
            }

            Console.WriteLine($"PDF successfully converted to TIFF: {outputTiffPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
