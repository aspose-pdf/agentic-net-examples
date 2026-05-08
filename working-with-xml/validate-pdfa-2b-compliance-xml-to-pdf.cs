using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string pdfPath = "intermediate.pdf";
        const string logPath = "validation_log.txt";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // Load XML and convert to PDF
            XmlLoadOptions loadOptions = new XmlLoadOptions();
            using (Document doc = new Document(xmlPath, loadOptions))
            {
                // Save the intermediate PDF
                doc.Save(pdfPath);

                // Validate PDF/A‑2b compliance
                bool isCompliant = doc.Validate(logPath, PdfFormat.PDF_A_2B);
                Console.WriteLine($"PDF/A‑2b compliant: {isCompliant}");
                Console.WriteLine($"Validation log written to: {logPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}