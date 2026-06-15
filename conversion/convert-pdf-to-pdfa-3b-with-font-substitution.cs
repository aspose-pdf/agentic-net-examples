using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for FontEmbeddingOptions

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa3b.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Configure conversion options for PDF/A‑3b
            PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_3B);

            // Enable default font substitution for fonts that cannot be embedded
            convOptions.FontEmbeddingOptions.UseDefaultSubstitution = true;

            // Perform the conversion
            bool success = doc.Convert(convOptions);
            if (!success)
            {
                Console.Error.WriteLine("Conversion to PDF/A‑3b failed.");
                return;
            }

            // Save the converted document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑3b file saved to '{outputPath}'.");
    }
}