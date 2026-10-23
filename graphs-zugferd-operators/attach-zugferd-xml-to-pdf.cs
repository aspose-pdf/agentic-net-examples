using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string zugferdXml = "invoice.xml";
        const string outputPdf = "output_with_zugferd.pdf";

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

        try
        {
            using (Document doc = new Document(inputPdf))
            {
                // Embed the ZUGFeRD XML as an embedded file (AFRelationship.Data).
                using (FileStream xmlStream = File.OpenRead(zugferdXml))
                {
                    var fileSpec = new FileSpecification(xmlStream, Path.GetFileName(zugferdXml), "ZUGFeRD Invoice XML")
                    {
                        MIMEType = "application/xml",
                        AFRelationship = AFRelationship.Data
                    };
                    doc.EmbeddedFiles.Add(fileSpec);
                }

                // Convert the document to PDF/A‑3B which is required for ZUGFeRD compliance.
                string logPath = "convert.log"; // optional conversion log
                doc.Convert(logPath, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

                // Save the resulting PDF.
                doc.Save(outputPdf);
            }

            Console.WriteLine($"ZUGFeRD attached and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
