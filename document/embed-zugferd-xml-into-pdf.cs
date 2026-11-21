using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "invoice.pdf";      // existing PDF to embed XML into
        const string zugferdXmlPath = "invoice.xml";    // ZUGFeRD XML file
        const string outputPdfPath = "invoice_with_xml.pdf";
        const string extractedXmlPath = "extracted_invoice.xml";

        // Verify input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(zugferdXmlPath))
        {
            Console.Error.WriteLine($"ZUGFeRD XML not found: {zugferdXmlPath}");
            return;
        }

        // Load PDF, embed ZUGFeRD XML, and save the result
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a FileSpecification for the XML file and add it to the EmbeddedFiles collection
            using (FileStream xmlStream = File.OpenRead(zugferdXmlPath))
            {
                var fileSpec = new FileSpecification(xmlStream, Path.GetFileName(zugferdXmlPath), "ZUGFeRD Invoice XML")
                {
                    MIMEType = "application/xml",
                    AFRelationship = AFRelationship.Data
                };
                // Optional: set modification date
                fileSpec.Params.ModDate = DateTime.UtcNow;
                pdfDoc.EmbeddedFiles?.Add(fileSpec);
            }

            // Convert to PDF/A‑3B to be ZUGFeRD‑compliant (optional but recommended)
            string conversionLog = Path.GetTempFileName();
            pdfDoc.Convert(conversionLog, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);
            File.Delete(conversionLog);

            // Save the PDF with the embedded XML
            pdfDoc.Save(outputPdfPath);
        }

        // Verify that the XML was embedded by extracting it back
        if (!File.Exists(outputPdfPath))
        {
            Console.Error.WriteLine($"Failed to create output PDF: {outputPdfPath}");
            return;
        }

        using (Document pdfWithXml = new Document(outputPdfPath))
        {
            FileSpecification embeddedSpec = null;
            if (pdfWithXml.EmbeddedFiles != null)
            {
                foreach (FileSpecification spec in pdfWithXml.EmbeddedFiles)
                {
                    if (string.Equals(spec.Name, Path.GetFileName(zugferdXmlPath), StringComparison.OrdinalIgnoreCase))
                    {
                        embeddedSpec = spec;
                        break;
                    }
                }
            }

            if (embeddedSpec != null)
            {
                // Write the embedded contents to a file for verification
                using (FileStream outStream = new FileStream(extractedXmlPath, FileMode.Create, FileAccess.Write))
                using (Stream contentStream = embeddedSpec.Contents)
                {
                    contentStream.CopyTo(outStream);
                }
            }
        }

        // Check if extraction succeeded
        if (File.Exists(extractedXmlPath))
        {
            Console.WriteLine("ZUGFeRD XML successfully embedded and verified.");
            // Optionally compare original and extracted XML for equality
            bool identical = File.ReadAllBytes(zugferdXmlPath).Length == File.ReadAllBytes(extractedXmlPath).Length &&
                             File.ReadAllText(zugferdXmlPath) == File.ReadAllText(extractedXmlPath);
            Console.WriteLine($"Extraction matches original: {identical}");
        }
        else
        {
            Console.Error.WriteLine("Failed to verify embedded ZUGFeRD XML: extraction did not produce a file.");
        }
    }
}
