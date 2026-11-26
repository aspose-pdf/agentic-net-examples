using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "Invoice.pdf";
        const string xmlPath = "factur-x.xml";
        const string outputPath = "ConformanceSet.pdf";
        const string logPath = "conversion.log";

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
            // Create a FileSpecification for the ZUGFeRD XML attachment
            var xmlSpec = new FileSpecification(xmlPath, "ZUGFeRD invoice data")
            {
                MIMEType = "text/xml",
                // For ZUGFeRD the relationship is typically "Alternative"
                AFRelationship = AFRelationship.Alternative
            };

            // Add the file to the EmbeddedFiles collection (this also registers it for PDF/A‑3B)
            doc.EmbeddedFiles.Add(xmlSpec);

            // Convert the document to PDF/A‑3B (the conversion automatically sets the conformance level)
            doc.Convert(logPath, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

            // Save the resulting PDF/A‑3B file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑3B file saved as '{outputPath}'.");
    }
}
