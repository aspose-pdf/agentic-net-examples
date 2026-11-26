using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "Receipt.pdf";
        const string xmlPath  = "factur-x.xml";
        const string outputPdf = "Receipt_ZUGFeRD.pdf";
        const string logPath   = "conversion_log.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"ZUGFeRD XML not found: {xmlPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPdf))
            {
                // Create a file specification for the ZUGFeRD XML attachment
                var xmlSpec = new FileSpecification(xmlPath, "ZUGFeRD invoice")
                {
                    MIMEType = "text/xml",
                    AFRelationship = AFRelationship.Alternative
                };

                // Add the specification to the EmbeddedFiles collection
                doc.EmbeddedFiles.Add(xmlSpec);

                // Convert the document to PDF/A‑3B (required for ZUGFeRD)
                doc.Convert(logPath, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

                // Save the resulting PDF/A‑3B file
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF/A‑3B with ZUGFeRD saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
