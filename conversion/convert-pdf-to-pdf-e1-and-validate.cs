using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "specificationU.pdf";
        const string outputPath = "specificationU_PDFE1.pdf";
        const string reportPath = "validation_report.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Convert the document to PDF/E‑1 format
            PdfFormatConversionOptions convertOpts = new PdfFormatConversionOptions(PdfFormat.PDF_E_1);
            doc.Convert(convertOpts);
            doc.Save(outputPath); // Save the converted PDF/E‑1 file

            // Validate the resulting PDF/E‑1 file using Document.Validate
            // The method writes an XML report and returns a bool indicating conformance
            bool isConformant = doc.Validate(reportPath, PdfFormat.PDF_E_1);
            Console.WriteLine(isConformant
                ? "PDF/E‑1 validation succeeded."
                : "PDF/E‑1 validation failed. See report for details.");
        }

        Console.WriteLine($"Conversion to PDF/E‑1 completed: {outputPath}");
        Console.WriteLine($"Validation report saved: {reportPath}");
    }
}
