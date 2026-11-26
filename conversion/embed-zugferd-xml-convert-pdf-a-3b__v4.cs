using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "Order.pdf";
        const string xmlPath = "factur-x.xml";
        const string outputPdf = "OrderConverted.pdf";
        const string conversionLog = "conversion_log.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(pdfPath))
        {
            // Create a FileSpecification for the ZUGFeRD XML and add it to the EmbeddedFiles collection
            using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
            {
                var fileSpec = new FileSpecification(xmlStream, Path.GetFileName(xmlPath), "ZUGFeRD Factur‑X XML")
                {
                    MIMEType = "text/xml",
                    AFRelationship = AFRelationship.Alternative // marks the file as an alternative representation (ZUGFeRD)
                };
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Convert the PDF to PDF/A‑3B (required for embedded files to be ZUGFeRD‑compliant)
            using (FileStream logStream = new FileStream(conversionLog, FileMode.Create, FileAccess.Write))
            {
                doc.Convert(logStream, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);
            }

            // Save the resulting PDF/A‑3B document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF/A‑3B file with embedded ZUGFeRD XML saved as '{outputPdf}'.");
    }
}
