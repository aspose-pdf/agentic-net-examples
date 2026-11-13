using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "invoice.xml";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"ZUGFeRD XML not found: {xmlPath}");
            return;
        }

        using (Document doc = new Document(pdfPath))
        {
            // Embed the ZUGFeRD XML as an embedded file (AFRelationship.Data).
            using (FileStream xmlStream = File.OpenRead(xmlPath))
            {
                var fileSpec = new FileSpecification(xmlStream, Path.GetFileName(xmlPath), "ZUGFeRD Invoice XML")
                {
                    MIMEType = "application/xml",
                    AFRelationship = AFRelationship.Data
                };
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Convert the document to PDF/A‑3B which is required for ZUGFeRD compliance.
            doc.Convert("log.xml", PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

            // Save the resulting PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"ZUGFeRD XML attached and saved to '{outputPath}'.");
    }
}
