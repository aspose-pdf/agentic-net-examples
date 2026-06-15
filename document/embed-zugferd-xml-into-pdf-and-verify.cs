using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "invoice.pdf";   // Existing PDF invoice
        const string xmlPath = "invoice.xml";   // ZUGFeRD XML file
        const string outputPath = "invoice_zugferd.pdf";

        // Ensure input files exist
        if (!File.Exists(pdfPath) || !File.Exists(xmlPath))
        {
            Console.Error.WriteLine("Required input files not found.");
            return;
        }

        // Load the PDF, embed the ZUGFeRD XML, convert to ZUGFeRD (PDF/A‑3) and save
        using (Document doc = new Document(pdfPath))
        {
            // Create a FileSpecification for the XML attachment (no MimeType property in current API)
            var xmlSpec = new FileSpecification(xmlPath, "ZUGFeRD Invoice");

            // Add the attachment to the PDF
            doc.EmbeddedFiles.Add(xmlSpec);

            // Convert to ZUGFeRD (PDF/A‑3) format – required for ZUGFeRD compliance
            doc.Convert("conversion_log.xml", PdfFormat.ZUGFeRD, ConvertErrorAction.Delete);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        // Verify that the XML was embedded
        using (Document verifyDoc = new Document(outputPath))
        {
            bool xmlEmbedded = false;
            // EmbeddedFiles collection holds all attached files
            foreach (FileSpecification spec in verifyDoc.EmbeddedFiles)
            {
                if (spec.Name.Equals(Path.GetFileName(xmlPath), StringComparison.OrdinalIgnoreCase))
                {
                    xmlEmbedded = true;
                    break;
                }
            }

            Console.WriteLine(xmlEmbedded
                ? "ZUGFeRD XML embedded successfully."
                : "Failed to embed ZUGFeRD XML.");
        }
    }
}
