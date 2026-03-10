using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "invoice.pdf";          // PDF containing ZUGFeRD data
        const string extractedXmlPath = "invoice.xml";        // File to store extracted XML
        const string outputPdfPath  = "invoice_with_zugferd.pdf"; // PDF after re‑binding XML

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Extract the ZUGFeRD XML attachment from the PDF.
        // ------------------------------------------------------------
        using (Document doc = new Document(inputPdfPath))
        {
            // Embedded files are 1‑based. Search for the first .xml attachment.
            FileSpecification xmlSpec = null;
            for (int i = 1; i <= doc.EmbeddedFiles.Count; i++)
            {
                var spec = doc.EmbeddedFiles[i];
                if (!string.IsNullOrEmpty(spec.Name) &&
                    spec.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    xmlSpec = spec;
                    break;
                }
            }

            if (xmlSpec != null)
            {
                // The Contents property returns a Stream; copy it to a file.
                using (FileStream outStream = File.Create(extractedXmlPath))
                using (Stream content = xmlSpec.Contents)
                {
                    content.CopyTo(outStream);
                }
                Console.WriteLine($"ZUGFeRD XML extracted to '{extractedXmlPath}'.");
            }
            else
            {
                Console.WriteLine("No ZUGFeRD XML attachment found in the PDF.");
                return;
            }
        }

        // ------------------------------------------------------------
        // Step 2: Load the PDF again and bind the extracted XML.
        // This embeds the XML back into the PDF, enabling ZUGFeRD persistence.
        // ------------------------------------------------------------
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            if (File.Exists(extractedXmlPath))
            {
                // BindXml attaches the XML file as a ZUGFeRD payload.
                pdfDoc.BindXml(extractedXmlPath);

                // Save the PDF with the embedded ZUGFeRD data.
                pdfDoc.Save(outputPdfPath);
                Console.WriteLine($"PDF saved with ZUGFeRD data to '{outputPdfPath}'.");
            }
            else
            {
                Console.WriteLine("Extracted XML file not found; cannot bind ZUGFeRD data.");
            }
        }
    }
}