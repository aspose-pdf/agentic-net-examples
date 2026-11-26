using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string validationXmlPath = "validation.xml"; // original validation XML to embed
        const string validationReportPath = "validation_report.xml"; // generated validation report
        const string outputPdfPath = "output_pdfa3b.pdf";
        const string conversionLogPath = "conversion_log.xml";

        // Verify that the required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(validationXmlPath))
        {
            Console.Error.WriteLine($"Validation XML not found: {validationXmlPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // -----------------------------------------------------------------
                // 1. Validate the PDF (using Document.Validate). The method writes an
                //    XML report and returns a boolean indicating conformance.
                // -----------------------------------------------------------------
                bool isValid = pdfDoc.Validate(validationReportPath, PdfFormat.PDF_A_3B);
                Console.WriteLine($"PDF validation result: {(isValid ? "Valid" : "Invalid")}");
                Console.WriteLine($"Validation report saved to: {validationReportPath}");

                // -----------------------------------------------------------------
                // 2. Embed the original validation XML into the PDF. PDF/A‑3b permits
                //    arbitrary file attachments, which are stored via the EmbeddedFiles
                //    collection.
                // -----------------------------------------------------------------
                var xmlSpec = new FileSpecification(Path.GetFileName(validationXmlPath))
                {
                    Description = "Validation XML",
                    MIMEType = "text/xml",
                    AFRelationship = AFRelationship.Data,
                    Contents = new MemoryStream(File.ReadAllBytes(validationXmlPath))
                };
                pdfDoc.EmbeddedFiles.Add(xmlSpec);

                // -----------------------------------------------------------------
                // 3. Convert the document to PDF/A‑3b. Use the Document.Convert overload
                //    that accepts a log file, the target PdfFormat and an error‑handling
                //    action.
                // -----------------------------------------------------------------
                pdfDoc.Convert(conversionLogPath, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

                // -----------------------------------------------------------------
                // 4. Save the converted PDF/A‑3b file.
                // -----------------------------------------------------------------
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF/A‑3b file saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
