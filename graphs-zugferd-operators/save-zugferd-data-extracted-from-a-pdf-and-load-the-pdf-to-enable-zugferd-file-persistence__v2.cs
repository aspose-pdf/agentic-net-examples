using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class ZugFeRdExample
{
    static void Main()
    {
        const string pdfInputPath   = "invoice.pdf";          // Existing PDF (may already contain ZUGFeRD data)
        const string xmlDataPath    = "invoice.xml";          // ZUGFeRD XML to embed
        const string pdfOutputPath  = "invoice_with_zugferd.pdf";
        const string extractedXmlPath = "extracted_invoice.xml";

        // ------------------------------------------------------------
        // Load the PDF document (lifecycle: load)
        // ------------------------------------------------------------
        if (!File.Exists(pdfInputPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfInputPath}");
            return;
        }

        using (Document pdfDoc = new Document(pdfInputPath))
        {
            // --------------------------------------------------------
            // Bind (embed) ZUGFeRD XML data into the PDF (persistence)
            // --------------------------------------------------------
            if (File.Exists(xmlDataPath))
            {
                pdfDoc.BindXml(xmlDataPath); // embeds the XML as an associated file
                Console.WriteLine("ZUGFeRD XML bound to PDF.");
            }
            else
            {
                Console.WriteLine("No ZUGFeRD XML file found; skipping bind step.");
            }

            // --------------------------------------------------------
            // Save the PDF with the embedded ZUGFeRD data (lifecycle: save)
            // --------------------------------------------------------
            pdfDoc.Save(pdfOutputPath);
            Console.WriteLine($"PDF saved with ZUGFeRD data: {pdfOutputPath}");
        }

        // ------------------------------------------------------------
        // Load the PDF again to extract the embedded ZUGFeRD XML
        // ------------------------------------------------------------
        if (!File.Exists(pdfOutputPath))
        {
            Console.Error.WriteLine($"Saved PDF not found: {pdfOutputPath}");
            return;
        }

        using (Document pdfDoc = new Document(pdfOutputPath))
        {
            // The ZUGFeRD XML is stored in the document catalog under the key "ZUGFeRD"
            // GetCatalogValue returns an object; cast it to string safely.
            object catalogValue = pdfDoc.GetCatalogValue("ZUGFeRD");
            string zugferdXml = catalogValue as string ?? catalogValue?.ToString();

            if (!string.IsNullOrEmpty(zugferdXml))
            {
                File.WriteAllText(extractedXmlPath, zugferdXml);
                Console.WriteLine($"Extracted ZUGFeRD XML saved to: {extractedXmlPath}");
            }
            else
            {
                Console.WriteLine("No ZUGFeRD XML found in the PDF.");
            }
        }
    }
}
