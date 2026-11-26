using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input1.pdf";
        const string outputPath = "output_pdfx4.pdf";
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
                // Convert the document to PDF/X‑4. No external ICC profile is supplied,
                // so the conversion uses the default color management behavior.
                doc.Convert(logPath, PdfFormat.PDF_X_4, ConvertErrorAction.Delete);

                // Save the converted PDF/X‑4 document.
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/X‑4 file saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}