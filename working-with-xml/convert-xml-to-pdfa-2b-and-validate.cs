using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlInputPath   = "input.xml";          // source XML file
        const string pdfOutputPath  = "output.pdf";         // PDF/A‑2b result
        const string convertLogPath = "convert_log.txt";    // conversion log
        const string validateLogPath = "validate_log.txt"; // validation log

        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlInputPath}");
            return;
        }

        try
        {
            // Load XML and create a PDF document
            using (Document doc = new Document(xmlInputPath, new XmlLoadOptions()))
            {
                // Convert the document to PDF/A‑2b format
                // Errors are written to convertLogPath; objects that cannot be converted are deleted
                doc.Convert(convertLogPath, PdfFormat.PDF_A_2B, ConvertErrorAction.Delete);

                // Save the PDF/A‑2b compliant file
                doc.Save(pdfOutputPath);

                // Validate the saved PDF against PDF/A‑2b compliance
                bool isCompliant = doc.Validate(validateLogPath, PdfFormat.PDF_A_2B);

                Console.WriteLine($"PDF/A‑2b validation result: {isCompliant}");
                Console.WriteLine($"Conversion log: {convertLogPath}");
                Console.WriteLine($"Validation log: {validateLogPath}");
                Console.WriteLine($"PDF saved to: {pdfOutputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}