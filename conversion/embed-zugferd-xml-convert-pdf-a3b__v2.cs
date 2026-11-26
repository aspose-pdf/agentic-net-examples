using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "Invoice.pdf";
        const string xmlPath = "factur-x.xml";
        const string outputPath = "Invoice_PDF_A3B.pdf";

        // Verify input files exist
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
            // Embed the ZUGFeRD XML as an associated file (AF)
            using (var xmlStream = new MemoryStream(File.ReadAllBytes(xmlPath)))
            {
                var fileSpec = new FileSpecification(xmlStream, "factur-x.xml", "ZUGFeRD invoice data")
                {
                    MIMEType = "text/xml",
                    // For ZUGFeRD the relationship should be Alternative (AFRelationship.Alternative)
                    AFRelationship = AFRelationship.Alternative
                };
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Convert the document to PDF/A‑3B (required for ZUGFeRD compliance)
            // The Convert overload writes the converted PDF to a stream; we can use a dummy stream here.
            using (var dummyStream = new MemoryStream())
            {
                doc.Convert(dummyStream, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);
            }

            // Save the resulting PDF/A‑3B file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑3B file saved as '{outputPath}'.");
    }
}
