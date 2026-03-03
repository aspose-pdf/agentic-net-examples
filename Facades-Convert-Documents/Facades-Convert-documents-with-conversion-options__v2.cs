using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // Facade namespace (used for other facade examples if needed)

class Program
{
    static void Main()
    {
        // Input PDF to be converted
        const string inputPdfPath  = "input.pdf";
        // Output PDF after conversion (e.g., PDF/A-1B)
        const string outputPdfPath = "output_pdfa.pdf";
        // Log file where conversion comments will be stored
        const string logFilePath   = "conversion_log.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the source PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure conversion options:
                //   - Convert to PDF/A-1B format
                //   - Delete objects that cannot be converted
                //   - Optimize file size (optional)
                PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(
                    PdfFormat.PDF_A_1B,               // Target PDF format
                    ConvertErrorAction.Delete);       // Action for non‑convertible objects

                // Optional: enable file‑size optimization during conversion
                convOptions.OptimizeFileSize = true;

                // Set the path for the conversion log file
                convOptions.LogFileName = logFilePath;

                // Perform the conversion
                bool success = pdfDoc.Convert(convOptions);
                if (!success)
                {
                    Console.Error.WriteLine("Conversion reported failure (see log for details).");
                }

                // Save the converted document
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Document successfully converted and saved to '{outputPdfPath}'.");
            Console.WriteLine($"Conversion log written to '{logFilePath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}