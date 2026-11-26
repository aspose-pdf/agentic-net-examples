using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath  = "input.pdf";
        const string outputDir     = "OutputPdfA";
        const string outputFile    = "document_pdfa.pdf";
        const string conversionLog = "conversion_log.xml";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Create output directory if it does not exist
        Directory.CreateDirectory(outputDir);
        string outputPdfPath = Path.Combine(outputDir, outputFile);

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdfPath))
            {
                // Convert the document to PDF/A‑1B.
                // The first argument is a log file that records conversion details.
                doc.Convert(conversionLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                // Save the converted PDF/A document to the designated output path.
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF/A saved successfully to: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}