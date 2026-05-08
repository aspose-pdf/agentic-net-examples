using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // Source PDF file
        const string outputPath = "output_pdfa1b.pdf"; // Destination PDF/A‑1b file
        const string logPath = "conversion_log.xml";   // Optional conversion log

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block to ensure deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Convert the document to PDF/A‑1b. This process embeds any missing fonts.
            doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the converted document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑1b file saved to '{outputPath}'.");
    }
}
