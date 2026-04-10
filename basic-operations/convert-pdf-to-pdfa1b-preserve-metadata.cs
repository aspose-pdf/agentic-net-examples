using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the PDF/A‑1b output file, and an optional conversion log.
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa1b.pdf";
        const string logPath    = "conversion_log.xml"; // can be any writable location

        // Ensure the source file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Load the existing PDF.
        using (Document pdfDocument = new Document(inputPath))
        {
            // Convert the document to PDF/A‑1b compliance.
            // The conversion is performed in‑place; metadata (Info dictionary) is preserved automatically.
            pdfDocument.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the converted document.
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑1b file saved to '{outputPath}'.");
    }
}
