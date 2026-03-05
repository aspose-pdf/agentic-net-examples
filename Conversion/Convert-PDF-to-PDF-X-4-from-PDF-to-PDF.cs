using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfx4.pdf";
        const string logPath = "conversion_log.txt";

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
                // Convert the document to PDF/X-4 format.
                // Errors are logged to the specified file; objects that cannot be converted are deleted.
                doc.Convert(logPath, PdfFormat.PDF_X_4, ConvertErrorAction.Delete);

                // Save the converted document as a PDF file.
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully converted to PDF/X-4 and saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}