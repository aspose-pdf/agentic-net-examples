using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF path
        const string inputPdf = "input.pdf";

        // Output directory and file name for the PDF/X document
        const string outputDir = "Converted";
        const string outputFileName = "output_pdfx3.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create the output directory if it does not exist
        Directory.CreateDirectory(outputDir);

        // Full path for the converted PDF/X document
        string outputPath = Path.Combine(outputDir, outputFileName);

        // Path for the conversion log (optional but required by Convert method)
        string logPath = Path.Combine(outputDir, "conversion_log.xml");

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Convert the document to PDF/X-3 format.
                // The Convert method writes a log file and changes the document in-place.
                doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the converted PDF/X document to the designated output path.
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/X document saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}