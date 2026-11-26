using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string validationXml = "validation.xml";
        const string outputPdf = "output_pdfa3b.pdf";
        const string conversionLog = "conversion_log.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(validationXml))
        {
            Console.Error.WriteLine($"Validation XML not found: {validationXml}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPdf))
            {
                // Validate PDF/A‑3b compliance and write the XML report
                bool isCompliant = doc.Validate(validationXml, PdfFormat.PDF_A_3B);
                Console.WriteLine($"PDF/A‑3b compliance: {isCompliant}");

                // Enable auto‑tagging globally
                AutoTaggingSettings.Default.EnableAutoTagging = true;

                // Convert to PDF/A‑3b, writing a conversion log
                doc.Convert(conversionLog, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

                // Save the converted PDF/A‑3b document
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF/A‑3b file saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
