using System;
using System.IO;
using Aspose.Pdf;                     // Core API
using Aspose.Pdf.Tagged;             // ITaggedContent interface
using Aspose.Pdf.LogicalStructure;   // For structure element types if needed

class PdfUaValidator
{
    static void Main()
    {
        // Input PDF path (must be a tagged PDF for PDF/UA validation)
        const string inputPdfPath = "input.pdf";

        // Path where the validation log (XML report) will be saved
        const string validationReportPath = "validation_report.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Check whether the document is already PDF/UA compliant
            bool isCompliant = doc.IsPdfUaCompliant;
            Console.WriteLine($"PDF/UA compliance before validation: {isCompliant}");

            // Perform PDF/UA validation.
            // The Validate method writes a detailed log (XML) to the specified file.
            // PdfFormat.PDF_UA_1 selects the PDF/UA 1 standard.
            bool validationResult = doc.Validate(validationReportPath, PdfFormat.PDF_UA_1);

            Console.WriteLine($"Validation operation succeeded: {validationResult}");

            // After validation, the IsPdfUaCompliant property reflects the result.
            Console.WriteLine($"PDF/UA compliance after validation: {doc.IsPdfUaCompliant}");

            // Optionally, read and display the XML report content.
            if (File.Exists(validationReportPath))
            {
                string reportXml = File.ReadAllText(validationReportPath);
                Console.WriteLine("=== PDF/UA Validation Report (XML) ===");
                Console.WriteLine(reportXml);
            }
            else
            {
                Console.WriteLine("Validation report file was not created.");
            }

            // No explicit Save is required for validation; the document can be saved
            // if further modifications are needed.
        }
    }
}