using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlInputPath   = "input.xml";
        const string pdfOutputPath  = "output.pdf";
        const string convertLogPath = "convert_log.txt";
        const string validateLogPath = "validate_log.txt";

        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML source not found: {xmlInputPath}");
            return;
        }

        try
        {
            // Load XML and create a PDF document
            using (Document doc = new Document(xmlInputPath, new XmlLoadOptions()))
            {
                // Convert the document to PDF/A‑2b (PDF_A_2B) format
                doc.Convert(convertLogPath, PdfFormat.PDF_A_2B, ConvertErrorAction.Delete);
                doc.Save(pdfOutputPath);

                // Validate the resulting PDF/A‑2b document
                bool isCompliant = doc.Validate(validateLogPath, PdfFormat.PDF_A_2B);
                Console.WriteLine(isCompliant
                    ? "PDF/A‑2b validation succeeded."
                    : "PDF/A‑2b validation failed. See log for details.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}