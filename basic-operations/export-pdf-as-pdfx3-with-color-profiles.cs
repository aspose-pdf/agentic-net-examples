using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // source PDF
        const string outputPath = "output_pdfx3.pdf"; // destination PDF/X‑3 file
        const string logPath = "conversion_log.xml";  // optional conversion log

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Convert the document to PDF/X‑3 compliance.
            // The conversion preserves existing color profiles.
            doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the converted document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF successfully exported as PDF/X‑3 to '{outputPath}'.");
    }
}
