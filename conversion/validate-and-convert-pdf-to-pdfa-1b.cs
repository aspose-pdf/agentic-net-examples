using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa1b.pdf";
        const string validationLog = "validation_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Validate the original PDF against PDF/A‑1b and write the log
            bool isValid = doc.Validate(validationLog, PdfFormat.PDF_A_1B);
            Console.WriteLine($"Validation result: {(isValid ? "OK" : "Issues found")} (log saved to {validationLog})");

            // Prepare conversion options for PDF/A‑1b
            PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B)
            {
                // Delete objects that cannot be converted
                ErrorAction = ConvertErrorAction.Delete
            };

            // Convert the document to PDF/A‑1b
            doc.Convert(convOptions);

            // Save the converted PDF/A‑1b document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑1b file saved to '{outputPath}'.");
    }
}