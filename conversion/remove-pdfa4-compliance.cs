using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input_pdfa4.pdf";
        const string outputPath = "output_regular.pdf";
        const string logPath = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF/A‑4 document
            using (Document doc = new Document(inputPath))
            {
                // Convert to a regular PDF (removes PDF/A compliance) by converting to a standard PDF version
                // PdfFormat enum does not contain a "Pdf" member; use a version enum such as v_2_0 (PDF 2.0) or v_1_7.
                doc.Convert(logPath, PdfFormat.v_2_0, ConvertErrorAction.Delete);

                // Save as a regular PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Conversion completed: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
