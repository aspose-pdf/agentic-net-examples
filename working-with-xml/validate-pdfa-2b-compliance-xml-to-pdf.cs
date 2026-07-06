using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";          // Source XML file
        const string pdfPath = "output.pdf";         // Destination PDF/A‑2b file
        const string logPath = "validation.log";    // Validation log file

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML content into a PDF document using XmlLoadOptions
        using (Document doc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Convert the document to PDF/A‑2b format
            PdfFormatConversionOptions conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_2B);
            doc.Convert(conversionOptions);

            // Validate PDF/A‑2b compliance; results are written to logPath
            bool isCompliant = doc.Validate(logPath, PdfFormat.PDF_A_2B);
            Console.WriteLine(isCompliant
                ? "Document is PDF/A‑2b compliant."
                : "Document is NOT PDF/A‑2b compliant. See validation.log for details.");

            // Save the compliant PDF/A‑2b document
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF/A‑2b file saved to '{pdfPath}'.");
    }
}