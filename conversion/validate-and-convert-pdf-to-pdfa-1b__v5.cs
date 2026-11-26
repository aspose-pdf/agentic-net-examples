using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string validationXml = "validation.xml";
        const string logFile = "conversion_log.xml";
        const string outputPdf = "output_pdfa.pdf";

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

        // Enable auto‑tagging globally for all documents
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        try
        {
            using (Document doc = new Document(inputPdf))
            {
                // Validate the PDF against PDF/A‑1b. The overload writes an XML report and returns the result.
                bool conforms = doc.Validate(validationXml, PdfFormat.PDF_A_1B);
                if (!conforms)
                {
                    Console.WriteLine("Document does not conform to PDF/A‑1b. See validation report for details.");
                }
                else
                {
                    Console.WriteLine("Document already conforms to PDF/A‑1b.");
                }

                // Convert to PDF/A‑1b, providing a conversion log and specifying error handling.
                doc.Convert(logFile, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                // Save the resulting PDF/A‑1b document.
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF/A‑1b file saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
