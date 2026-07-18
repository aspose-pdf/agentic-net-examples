using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input XML file that will be converted to PDF
        const string xmlInputPath = "input.xml";
        // Intermediate PDF generated from the XML
        const string pdfIntermediatePath = "intermediate.pdf";
        // Log file that will contain validation messages
        const string validationLogPath = "validation.log";

        // Ensure the XML source exists
        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlInputPath}");
            return;
        }

        try
        {
            // Load the XML and convert it to PDF using XmlLoadOptions (input‑only format)
            using (Document doc = new Document(xmlInputPath, new XmlLoadOptions()))
            {
                // Save the intermediate PDF
                doc.Save(pdfIntermediatePath);

                // Validate the PDF against PDF/A‑2b compliance.
                // The Validate method returns true if the document complies.
                bool isPdfA2bCompliant = doc.Validate(validationLogPath, PdfFormat.PDF_A_2B);

                // Additionally, the IsPdfaCompliant property reflects the same status.
                Console.WriteLine($"IsPdfaCompliant property: {doc.IsPdfaCompliant}");
                Console.WriteLine($"Validate method result: {isPdfA2bCompliant}");
                Console.WriteLine($"Validation log written to: {validationLogPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}