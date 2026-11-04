using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfx3.pdf";
        const string logPath = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Convert the document to PDF/X‑3. Existing ICC profiles are retained automatically.
                // The Convert overload that accepts a log file, target format and error‑action is used.
                doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the converted PDF/X‑3 document. SaveFormat.Pdf is the correct enum for a normal PDF save.
                doc.Save(outputPath, SaveFormat.Pdf);
            }

            Console.WriteLine($"Conversion successful: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
