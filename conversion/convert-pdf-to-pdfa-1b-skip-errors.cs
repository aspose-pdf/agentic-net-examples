using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Create conversion options for PDF/A‑1b with error action set to skip (None)
            PdfFormatConversionOptions conversionOptions =
                new PdfFormatConversionOptions(PdfFormat.PDF_A_1B, ConvertErrorAction.None);

            // Perform the conversion
            doc.Convert(conversionOptions);

            // Save the converted PDF/A document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF converted to PDF/A‑1b and saved as '{outputPath}'.");
    }
}