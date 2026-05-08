using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Forms;        // Access to signature fields (if needed)
using Aspose.Pdf;               // XmlSaveOptions resides in this namespace

class ExportSignatureInfo
{
    static void Main()
    {
        // Paths – adjust as necessary
        const string inputPdfPath  = "signed_document.pdf";
        const string outputXmlPath = "signature_audit.xml";

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // OPTIONAL: If you need to inspect individual signature fields,
            // you can iterate over them here. The example below simply
            // demonstrates how to access a signature field's properties.
            /*
            foreach (SignatureField sigField in pdfDoc.Form.Signatures)
            {
                Console.WriteLine($"Signature field name: {sigField.PartialName}");
                Console.WriteLine($"Reason: {sigField.Reason}");
                Console.WriteLine($"Location: {sigField.Location}");
                Console.WriteLine($"Date: {sigField.Date}");
                // Additional properties can be logged as needed.
            }
            */

            // Save the entire PDF structure (including digital signature data)
            // to an XML file using XmlSaveOptions.
            XmlSaveOptions xmlOptions = new XmlSaveOptions();
            pdfDoc.Save(outputXmlPath, xmlOptions);
        }

        Console.WriteLine($"Signature information exported to XML: {outputXmlPath}");
    }
}