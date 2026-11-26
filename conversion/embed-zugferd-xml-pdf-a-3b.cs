using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "Invoice.pdf";
        const string xmlPath = "factur-x.xml";
        const string outputDir = "output";
        const string outputPdfPath = "output/Invoice_ZUGFeRD.pdf";
        const string conversionLog = "conversion_log.xml";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // ---------------------------------------------------------------------
        // 1. Make sure the source PDF exists – if it does not, create a minimal one
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            using (var placeholder = new Document())
            {
                var page = placeholder.Pages.Add();
                page.Paragraphs.Add(new TextFragment("Placeholder invoice – generated because 'Invoice.pdf' was missing."));
                placeholder.Save(inputPdfPath);
            }
        }

        // ---------------------------------------------------------------------
        // 2. Make sure the ZUGFeRD XML exists – if it does not, create a tiny example
        // ---------------------------------------------------------------------
        if (!File.Exists(xmlPath))
        {
            const string sampleXml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<Invoice><ID>INV-001</ID></Invoice>";
            File.WriteAllText(xmlPath, sampleXml, Encoding.UTF8);
        }

        // ---------------------------------------------------------------------
        // 3. Load the PDF, embed the XML, convert to PDF/A‑3B and save the result
        // ---------------------------------------------------------------------
        using (Document doc = new Document(inputPdfPath))
        {
            // Embed the ZUGFeRD XML as an attached file (AFRelationship.Data)
            using (var xmlStream = new MemoryStream(File.ReadAllBytes(xmlPath)))
            {
                var fileSpec = new FileSpecification(xmlStream, Path.GetFileName(xmlPath), "ZUGFeRD invoice data")
                {
                    MIMEType = "text/xml",
                    AFRelationship = AFRelationship.Data
                };
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Convert the document to PDF/A‑3B (required for ZUGFeRD compliance)
            using (var logStream = new FileStream(conversionLog, FileMode.Create, FileAccess.Write))
            {
                doc.Convert(logStream, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);
            }

            // Save the PDF/A‑3B file with the embedded XML
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF/A‑3B file with embedded ZUGFeRD XML saved to '{outputPdfPath}'.");
    }
}
