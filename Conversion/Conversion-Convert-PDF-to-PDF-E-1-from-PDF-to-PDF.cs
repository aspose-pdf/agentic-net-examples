using System;
using System.IO;
using Aspose.Pdf;

class PdfToPdfE1Converter
{
    static void Main(string[] args)
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_pdf_e1.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the source PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Set up conversion options to produce PDF/E‑1 output
            PdfFormatConversionOptions conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_E_1);

            // Apply the conversion options – this changes the internal format to PDF/E‑1
            pdfDocument.Convert(conversionOptions);

            // Save the converted document. After conversion the document is already PDF/E‑1,
            // so we save it using the regular PDF SaveFormat.
            pdfDocument.Save(outputPdfPath, SaveFormat.Pdf);

            Console.WriteLine($"Conversion completed successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}