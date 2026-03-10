using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfe1.pdf";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPath))
            {
                // Prepare conversion options targeting PDF/E‑1 format.
                // PdfFormat.PDF_E_1 is the enum value that represents PDF/E‑1.
                PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_E_1);

                // Perform the conversion. The method returns true on success.
                bool converted = doc.Convert(options);
                if (!converted)
                {
                    Console.Error.WriteLine("Conversion to PDF/E-1 failed.");
                }

                // Save the converted document. The file extension remains .pdf,
                // but the internal format is now PDF/E‑1.
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/E-1 file saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}