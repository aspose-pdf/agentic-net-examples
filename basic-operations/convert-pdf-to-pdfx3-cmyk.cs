using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // source PDF
        const string outputPath = "output_pdfx3.pdf"; // PDF/X‑3 result
        const string logPath = "conversion_log.txt";   // conversion log (optional)
        const string iccPath = "CMYK.icc";            // path to a CMYK ICC profile

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Verify ICC profile exists
        if (!File.Exists(iccPath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Attach an OutputIntent that forces CMYK colour space.
            // The OutputIntent is added to the document before conversion.
            doc.OutputIntents.Add(new OutputIntent(iccPath));

            // Convert the document to PDF/X‑3. Use the overload that accepts
            // a log file path, the target format and an error‑handling action.
            doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the converted document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/X‑3 compliant file saved to '{outputPath}'.");
    }
}
