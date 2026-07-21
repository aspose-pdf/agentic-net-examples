using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa1b.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF and convert it to PDF/A‑1b compliance.
        // The Convert method embeds any missing fonts automatically.
        using (Document doc = new Document(inputPath))
        {
            // Optional: path for a conversion log that contains details of any issues.
            const string conversionLog = "conversion_log.xml";

            // Convert the document to PDF/A‑1b. ConvertErrorAction.Delete removes objects that prevent compliance.
            doc.Convert(conversionLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the compliant document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑1b file saved to '{outputPath}'.");
    }
}
