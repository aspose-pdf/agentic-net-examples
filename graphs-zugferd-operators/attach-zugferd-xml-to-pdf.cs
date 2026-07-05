using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "invoice.pdf";          // Input PDF
        const string zugferdXmlPath = "invoice.xml";   // ZUGFeRD XML file
        const string outputPath = "invoice_with_zugferd.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(zugferdXmlPath))
        {
            Console.Error.WriteLine($"ZUGFeRD XML not found: {zugferdXmlPath}");
            return;
        }

        // Load the PDF, embed the ZUGFeRD XML, convert to PDF/A‑3B (required for ZUGFeRD), and save.
        using (Document doc = new Document(pdfPath))
        {
            // Embed the ZUGFeRD XML as an attached file.
            using (FileStream xmlStream = File.OpenRead(zugferdXmlPath))
            {
                var fileSpec = new FileSpecification(xmlStream, "invoice.xml", "ZUGFeRD Invoice XML")
                {
                    MIMEType = "application/xml",
                    AFRelationship = AFRelationship.Data
                };
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Convert the document to PDF/A‑3B to satisfy ZUGFeRD compliance.
            // The first argument is a log file path; it can be any writable location.
            doc.Convert("convert.log", PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

            // Save the resulting PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with attached ZUGFeRD saved to '{outputPath}'.");
    }
}
