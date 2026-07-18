using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";               // source PDF (already PDF/A)
        const string outputPdfPath = "output_zugferd.pdf";    // PDF with ZUGFeRD attachment
        const string zugFerdXmlPath = "invoice.xml";          // ZUGFeRD XML file
        const string validationLogPath = "validation_log.txt"; // validation report

        // Ensure required files exist
        if (!File.Exists(inputPdfPath) || !File.Exists(zugFerdXmlPath))
        {
            Console.Error.WriteLine("Input PDF or ZUGFeRD XML file not found.");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Add ZUGFeRD attachment using the EmbeddedFiles collection.
            // FileSpecification constructor (filePath, description) is the correct way.
            var attachment = new FileSpecification(zugFerdXmlPath, "ZUGFeRD Invoice");
            pdfDoc.EmbeddedFiles.Add(attachment);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            pdfDoc.Save(outputPdfPath);

            // Validate PDF/A compliance (using PDF/A‑1b as an example)
            bool isCompliant = pdfDoc.Validate(validationLogPath, PdfFormat.PDF_A_1B);
            Console.WriteLine($"PDF/A‑1b compliance: {isCompliant}");
            Console.WriteLine($"Validation log written to: {validationLogPath}");
        }
    }
}