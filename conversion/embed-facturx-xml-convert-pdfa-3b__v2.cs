using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "Invoice.pdf";
        const string xmlPath = "factur-x.xml";
        const string outputDir = "./output";
        // Path.Combine is evaluated at runtime, so we cannot use it in a const declaration.
        string outputPdf = Path.Combine(outputDir, "InvoiceRel.pdf");

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

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        // Load the source PDF.
        using (Document doc = new Document(pdfPath))
        {
            // Create a FileSpecification for the ZUGFeRD XML.
            var xmlSpec = new FileSpecification(xmlPath, "ZUGFeRD invoice data")
            {
                MIMEType = "text/xml",
                AFRelationship = AFRelationship.Data
            };
            // Set a modification date for PDF/A‑3 compliance.
            xmlSpec.Params.ModDate = DateTime.UtcNow;

            // Add the specification to the document's embedded files collection.
            doc.EmbeddedFiles.Add(xmlSpec);

            // Convert the document to PDF/A‑3B.
            var conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_3B);
            doc.Convert(conversionOptions);

            // Save the resulting PDF/A‑3B file.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF/A‑3B file saved to: {outputPdf}");
    }
}
