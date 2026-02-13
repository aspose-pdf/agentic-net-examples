using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for source and destination PDFs
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // OPTIONAL: Convert to PDF/A‑1b if the PdfFormat enum supports it.
            // The Convert method returns a bool, not a Document, so we do not assign its result.
            // Uncomment the following lines if your Aspose.Pdf version contains PdfFormat.PdfA1b.
            //
            // var conversionOptions = new PdfFormatConversionOptions(PdfFormat.PdfA1b);
            // pdfDocument.Convert(conversionOptions);

            // Save the (possibly converted) document as a PDF file
            pdfDocument.Save(outputPath);
            Console.WriteLine($"Conversion completed successfully. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during processing: {ex.Message}");
        }
    }
}
