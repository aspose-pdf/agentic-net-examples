using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlFilePath = "input.xml";          // Source XML file
        const string logFilePath = "validation_log.txt"; // Validation log output

        if (!File.Exists(xmlFilePath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlFilePath}");
            return;
        }

        // Load the XML content and create a PDF document in memory
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document pdfDocument = new Document(xmlFilePath, loadOptions))
        {
            // Validate the generated PDF against PDF/A‑2b compliance
            bool isCompliant = pdfDocument.Validate(logFilePath, PdfFormat.PDF_A_2B);

            Console.WriteLine($"PDF/A‑2b compliance: {isCompliant}");
            Console.WriteLine($"Validation log written to: {logFilePath}");
        }
    }
}