using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_pdfa4.pdf";
        const string validationXmlPath = "validation.xml";
        const string conversionLogPath = "conversion_log.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdfPath))
            {
                // Validate the PDF (PDF/UA validation) and write the validation report to an XML file
                // The Validate method returns a bool indicating compliance; the XML report is created at the supplied path.
                bool isCompliant = doc.Validate(validationXmlPath, PdfFormat.PDF_UA_1);
                Console.WriteLine($"PDF compliance with PDF/UA‑1: {isCompliant}");

                // Enable auto‑tagging globally (static default instance)
                AutoTaggingSettings.Default.EnableAutoTagging = true;

                // Convert the document to PDF/A‑4 format
                doc.Convert(conversionLogPath, PdfFormat.PDF_A_4, ConvertErrorAction.Delete);

                // Save the converted PDF/A‑4 document
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF/A‑4 file saved to '{outputPdfPath}'. Validation report: '{validationXmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
