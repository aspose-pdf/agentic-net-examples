using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";
        const string validationLogPath = "validation_report.txt";
        const string xmlReportPath     = "document_structure.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (tagged PDF expected)
            using (Document doc = new Document(inputPdfPath))
            {
                // Validate against PDF/UA (PdfFormat.PDF_UA_1)
                // The method returns true if the document complies, false otherwise.
                bool isCompliant = doc.Validate(validationLogPath, PdfFormat.PDF_UA_1);
                Console.WriteLine($"PDF/UA validation result: {(isCompliant ? "Compliant" : "Non‑compliant")}");
                Console.WriteLine($"Validation log written to: {validationLogPath}");

                // Save the logical structure of the PDF as XML for detailed inspection.
                doc.SaveXml(xmlReportPath);
                Console.WriteLine($"XML structure report saved to: {xmlReportPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during validation: {ex.Message}");
        }
    }
}