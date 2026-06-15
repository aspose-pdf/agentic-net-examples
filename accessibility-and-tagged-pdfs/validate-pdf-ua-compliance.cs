using System;
using System.IO;
using Aspose.Pdf;

class PdfUaValidator
{
    static void Main()
    {
        // Input PDF to be validated
        const string inputPdfPath = "input.pdf";

        // Path for the validation log (XML/HTML format depending on Aspose.Pdf version)
        const string validationLogPath = "validation_report.xml";

        // Path for the full PDF structure export (optional, useful for debugging)
        const string pdfStructureXmlPath = "pdf_structure.xml";

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (no custom LoadOptions required for a standard PDF)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Quick check: is the document already PDF/UA compliant?
                Console.WriteLine($"IsPdfUaCompliant: {pdfDoc.IsPdfUaCompliant}");

                // Validate the document against PDF/UA (PDF_UA_1) and write the log to a file.
                // The Validate method returns true if validation succeeded without errors.
                bool validationResult = pdfDoc.Validate(validationLogPath, PdfFormat.PDF_UA_1);

                Console.WriteLine($"Validation completed. Success: {validationResult}");
                Console.WriteLine($"Validation report saved to: {validationLogPath}");

                // Optional: export the entire PDF structure to XML for further analysis.
                pdfDoc.SaveXml(pdfStructureXmlPath);
                Console.WriteLine($"PDF structure exported to: {pdfStructureXmlPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception: {ex.Message}");
        }
    }
}