using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xmlAttachment = "invoice.xml";
        const string outputPdf = "output_pdfa3.pdf";
        const string validationLog = "validation_log.xml";

        if (!File.Exists(inputPdf) || !File.Exists(xmlAttachment))
        {
            Console.Error.WriteLine("Required files not found.");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPdf))
            {
                // Embed ZUGFeRD XML as an embedded file (PDF/A‑3 requires embedded files, not Attachments)
                byte[] xmlData = File.ReadAllBytes(xmlAttachment);
                var fileSpec = new FileSpecification(xmlAttachment, "ZUGFeRD Invoice XML")
                {
                    MIMEType = "application/xml",
                    AFRelationship = AFRelationship.Data,
                    Contents = new MemoryStream(xmlData)
                };
                doc.EmbeddedFiles.Add(fileSpec);

                // Convert to PDF/A‑3U (required for ZUGFeRD compliance)
                doc.Convert(validationLog, PdfFormat.PDF_A_3U, ConvertErrorAction.Delete);

                // Validate PDF/A compliance – Document.Validate returns a bool, not a ValidationResult
                bool isCompliant = doc.Validate(validationLog, PdfFormat.PDF_A_3U);
                Console.WriteLine($"PDF/A compliance: {(isCompliant ? "OK" : "FAIL")}");

                // Save the final PDF/A‑3U document
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF/A‑3U file with ZUGFeRD attachment saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
