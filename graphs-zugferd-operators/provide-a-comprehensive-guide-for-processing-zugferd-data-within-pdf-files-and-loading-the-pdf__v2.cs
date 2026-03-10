using System;
using System.IO;
using Aspose.Pdf;

class ZUGFeRDProcessor
{
    static void Main()
    {
        const string pdfPath = "invoice.pdf";               // Input PDF (ZUGFeRD)
        const string extractedXmlPath = "invoice.xml";      // Where to write extracted XML
        const string newXmlPath = "new_invoice.xml";        // New XML to embed (optional)
        const string outputPdfPath = "invoice_updated.pdf"; // Output PDF after binding

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(pdfPath))
        {
            // ------------------------------------------------------------
            // 1. Extract embedded ZUGFeRD XML (if present)
            // ------------------------------------------------------------
            // In recent Aspose.Pdf versions the embedded files are accessible
            // via the Document.EmbeddedFiles collection. Each entry is a
            // FileSpecification that provides the file name (Name) and the
            // content stream (Contents).
            bool xmlExtracted = false;
            foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
            {
                if (!string.IsNullOrEmpty(fileSpec.Name) &&
                    fileSpec.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    using (FileStream outStream = File.Create(extractedXmlPath))
                    {
                        // The file data is available through the Contents stream.
                        fileSpec.Contents.CopyTo(outStream);
                    }
                    Console.WriteLine($"Extracted ZUGFeRD XML to '{extractedXmlPath}'.");
                    xmlExtracted = true;
                    break; // stop after the first XML file is found
                }
            }
            if (!xmlExtracted)
            {
                Console.WriteLine("No ZUGFeRD XML attachment found – PDF may not contain ZUGFeRD data.");
            }

            // ------------------------------------------------------------
            // 2. Bind a new ZUGFeRD XML file to the PDF (optional)
            // ------------------------------------------------------------
            if (File.Exists(newXmlPath))
            {
                // BindXml replaces or adds the XML attachment.
                doc.BindXml(newXmlPath);
                Console.WriteLine($"Bound new ZUGFeRD XML from '{newXmlPath}'.");
            }

            // ------------------------------------------------------------
            // 3. Save the (potentially) modified PDF (lifecycle: save)
            // ------------------------------------------------------------
            doc.Save(outputPdfPath);
            Console.WriteLine($"Saved updated PDF to '{outputPdfPath}'.");
        }
    }
}
