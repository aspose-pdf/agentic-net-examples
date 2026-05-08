using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // existing PDF/A‑3 document
        const string attachmentPath = "invoice.xml";        // ZUGFeRD XML file to attach
        const string outputPdfPath  = "output_with_attachment.pdf";
        const string validationLog  = "validation_report.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Load the PDF, add the ZUGFeRD attachment, save, then validate PDF/A‑3 compliance.
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPdfPath))
        {
            // Create a file specification for the attachment and add it to the document.
            Aspose.Pdf.FileSpecification fileSpec = new Aspose.Pdf.FileSpecification(
                attachmentPath,                     // path to the file to embed
                "ZUGFeRD invoice attachment");      // description shown in the attachment list

            doc.EmbeddedFiles.Add(fileSpec);

            // Save the modified document.
            doc.Save(outputPdfPath);

            // Validate the resulting PDF against PDF/A‑3A (the level required for ZUGFeRD).
            bool isCompliant = doc.Validate(validationLog, Aspose.Pdf.PdfFormat.PDF_A_3A);

            Console.WriteLine($"PDF/A‑3A compliance check: {(isCompliant ? "PASS" : "FAIL")}");
            Console.WriteLine($"Validation report written to: {validationLog}");
        }
    }
}