using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed_document.pdf";
        const string outputXml = "signature_audit.xml";

        // Verify the source PDF exists.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Export the PDF's internal structure (including digital signature data) to XML.
            // The SaveXml method writes the document model to an XML file.
            pdfDoc.SaveXml(outputXml);
        }

        Console.WriteLine($"Digital signature information exported to '{outputXml}'.");
    }
}