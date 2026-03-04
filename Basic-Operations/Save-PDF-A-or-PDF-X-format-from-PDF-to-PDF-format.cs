using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output PDF file path (the result will be a PDF/A or PDF/X compliant PDF)
        const string outputPdf = "output_converted.pdf";

        // Path to a log file where conversion errors/warnings will be written
        const string conversionLog = "conversion_log.txt";

        // Choose the desired target format:
        // For PDF/A use one of the PdfFormat.PDF_A_* values (e.g., PDF_A_1B)
        // For PDF/X use one of the PdfFormat.PDF_X_* values (e.g., PDF_X_3)
        PdfFormat targetFormat = PdfFormat.PDF_A_1B; // change as needed

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Convert the document to the selected PDF/A or PDF/X format.
                // ConvertErrorAction.Delete tells the converter to delete objects that cannot be converted.
                doc.Convert(conversionLog, targetFormat, ConvertErrorAction.Delete);

                // Save the converted document as a regular PDF file.
                // Document.Save(string) always writes PDF regardless of the extension.
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Conversion completed. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}