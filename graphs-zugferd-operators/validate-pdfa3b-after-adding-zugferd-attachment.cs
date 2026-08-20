using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string zugferdXml = "invoice.xml";
        const string logFile = "validation_log.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(zugferdXml))
        {
            Console.Error.WriteLine($"ZUGFeRD XML not found: {zugferdXml}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create a FileSpecification for the ZUGFeRD XML and embed it.
            var fileSpec = new FileSpecification(zugferdXml, "ZUGFeRD Invoice");
            // Load the XML content into a stream and assign to the specification.
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(zugferdXml));
            doc.EmbeddedFiles.Add(fileSpec);

            // Validate PDF/A‑3B compliance after adding the attachment
            bool isCompliant = doc.Validate(logFile, PdfFormat.PDF_A_3B);
            Console.WriteLine($"PDF/A‑3B compliant: {isCompliant}");
            Console.WriteLine($"Validation log saved to: {logFile}");
        }
    }
}
