using System;
using System.IO;
using Aspose.Pdf; // Document, PdfFormat, ConvertErrorAction

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfx4.pdf";
        const string logPath    = "conversion_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Convert the document to PDF/X-4 format.
            // The Convert method writes conversion errors to the specified log file.
            // ConvertErrorAction.Delete removes objects that cannot be converted.
            doc.Convert(logPath, PdfFormat.PDF_X_4, ConvertErrorAction.Delete);

            // Save the converted document as a regular PDF file.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Conversion completed. Output saved to '{outputPath}'.");
    }
}