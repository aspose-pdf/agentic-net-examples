using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "invoice.pdf";
        const string xmlPath = "invoice.xml";
        const string outputPath = "invoice_with_zugferd.pdf";

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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(pdfPath))
        {
            // Embed the ZUGFeRD XML as an attached file (AFRelationship.Data)
            using (FileStream xmlStream = File.OpenRead(xmlPath))
            {
                var fileSpec = new FileSpecification(xmlStream, Path.GetFileName(xmlPath), "ZUGFeRD Invoice XML")
                {
                    MIMEType = "application/xml",
                    AFRelationship = AFRelationship.Data
                };
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Convert to PDF/A‑3B to meet ZUGFeRD compliance (optional but recommended)
            doc.Convert("log.xml", PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

            // Save the PDF with the attached ZUGFeRD data
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with ZUGFeRD attachment: {outputPath}");
    }
}
