using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfx3.pdf";
        const string logPath    = "conversion_log.xml";

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
                // Convert the document to PDF/X-3 format.
                // The Convert overload writes conversion details to the log file
                // and removes objects that cannot be converted (Delete action).
                doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the converted PDF/X-3 document.
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully converted to PDF/X-3 and saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}