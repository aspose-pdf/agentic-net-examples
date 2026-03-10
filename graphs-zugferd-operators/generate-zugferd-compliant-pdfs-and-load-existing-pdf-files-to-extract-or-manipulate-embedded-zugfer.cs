using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "invoice.pdf";
        const string zugferdXmlPath = "invoice.xml";
        const string outputZugferdPdfPath = "invoice_zugferd.pdf";
        const string conversionLogPath = "conversion.log";

        // Verify input files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(zugferdXmlPath))
        {
            Console.Error.WriteLine($"ZUGFeRD XML not found: {zugferdXmlPath}");
            return;
        }

        // -------------------------------------------------
        // Create a ZUGFeRD‑compliant PDF
        // -------------------------------------------------
        using (Document doc = new Document(sourcePdfPath))
        {
            // Bind the ZUGFeRD XML invoice to the PDF
            doc.BindXml(zugferdXmlPath);

            // Convert the document to ZUGFeRD format (PDF/A‑3 with embedded XML)
            // The Convert method logs conversion details to the specified file.
            doc.Convert(conversionLogPath, PdfFormat.ZUGFeRD, ConvertErrorAction.Delete);

            // Save the resulting ZUGFeRD PDF
            doc.Save(outputZugferdPdfPath);
        }

        // -------------------------------------------------
        // Load an existing ZUGFeRD PDF and extract the embedded XML
        // -------------------------------------------------
        using (Document zugDoc = new Document(outputZugferdPdfPath))
        {
            // ZUGFeRD XML is stored as an embedded file (attachment) in the PDF.
            // Aspose.Pdf exposes embedded files via the EmbeddedFiles collection.
            // The collection uses 1‑based indexing.
            if (zugDoc.EmbeddedFiles != null && zugDoc.EmbeddedFiles.Count > 0)
            {
                // Retrieve the first embedded file (the ZUGFeRD XML)
                FileSpecification embeddedFile = zugDoc.EmbeddedFiles[1]; // 1‑based index
                string extractedXmlPath = "extracted_invoice.xml";

                // Save the embedded file stream to a physical file
                using (FileStream outStream = File.Create(extractedXmlPath))
                using (Stream content = embeddedFile.Contents)
                {
                    content.CopyTo(outStream);
                }

                Console.WriteLine($"Extracted ZUGFeRD XML saved to '{extractedXmlPath}'.");
            }
            else
            {
                Console.WriteLine("No embedded files found in the ZUGFeRD PDF.");
            }
        }
    }
}
