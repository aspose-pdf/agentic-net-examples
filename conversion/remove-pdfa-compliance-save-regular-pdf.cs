using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input_pdfa.pdf";
        const string outputPath = "output_regular.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF/A‑1b document
            using (Document doc = new Document(inputPath))
            {
                // Convert the PDF/A document to a regular PDF by changing the PDF version.
                // The conversion log is written to a temporary file.
                string logFile = Path.Combine(Path.GetTempPath(), "pdfa_conversion_log.xml");
                doc.Convert(logFile, PdfFormat.v_1_4, ConvertErrorAction.Delete);

                // Save the resulting non‑PDF/A PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Converted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
