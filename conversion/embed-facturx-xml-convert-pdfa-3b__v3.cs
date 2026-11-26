using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "Invoice.pdf";
        const string xmlPath = "factur-x.xml";

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

        string timestamp = DateTime.Now.ToString("yyyyMMdd");
        string outputPath = $"Invoice_{timestamp}.pdf";

        using (Document doc = new Document(pdfPath))
        {
            // Embed Factur‑X XML as an associated file (AF)
            var fileSpec = new FileSpecification("factur-x.xml", "Factur‑X invoice data")
            {
                MIMEType = "text/xml",
                AFRelationship = AFRelationship.Alternative
            };
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(xmlPath));
            doc.EmbeddedFiles.Add(fileSpec);

            // Convert the document to PDF/A‑3B (PDF/A‑3 with embedded files)
            // The first argument is a path for the conversion log (can be any file name).
            doc.Convert("conversion_log.xml", PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

            // Save the resulting PDF/A‑3B file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑3B file saved as '{outputPath}'.");
    }
}
