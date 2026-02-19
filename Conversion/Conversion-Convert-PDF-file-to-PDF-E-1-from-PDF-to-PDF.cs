using System;
using System.IO;
using Aspose.Pdf;

class PdfToPdfE1Converter
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfToPdfE1Converter <input.pdf> <output.pdf>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – '{inputPath}'.");
            return;
        }

        try
        {
            // Load the source PDF document
            Document srcDoc = new Document(inputPath);

            // Set up conversion options to PDF/E‑1 format
            PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_E_1);

            // Perform the conversion; Convert returns a bool indicating success
            bool conversionResult = srcDoc.Convert(convOptions);
            if (!conversionResult)
            {
                Console.Error.WriteLine("Conversion failed: Aspose.Pdf returned false.");
                return;
            }

            // Save the converted document (the original Document instance is now in PDF/E‑1 format)
            srcDoc.Save(outputPath);

            Console.WriteLine($"Conversion successful. PDF/E‑1 file saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
