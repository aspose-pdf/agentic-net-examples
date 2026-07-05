using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string zugFeRdXml = "invoice.xml";
        const string outputPdf = "output_with_zugferd.pdf";
        const string validationLog = "validation_log.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(zugFeRdXml))
        {
            Console.Error.WriteLine($"ZUGFeRD XML not found: {zugFeRdXml}");
            return;
        }

        // Load the PDF, embed the ZUGFeRD XML, save, then validate PDF/A compliance.
        using (Document doc = new Document(inputPdf))
        {
            // Embed ZUGFeRD XML as an embedded file (the correct API for file attachments).
            var fileSpec = new FileSpecification(zugFeRdXml, "ZUGFeRD-invoice.xml");
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(zugFeRdXml));
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the modified PDF.
            doc.Save(outputPdf);

            // Validate PDF/A compliance (using PDF/A‑2B as the target format).
            bool validationResult = doc.Validate(validationLog, PdfFormat.PDF_A_2B);

            // Report the validation outcome.
            Console.WriteLine($"Validation log written to: {validationLog}");
            Console.WriteLine($"Is PDF/A compliant after attachment? {validationResult}");
        }
    }
}
