using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "pfxPassword";
        const string timestampUrl = "http://timestamp.example.com";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document doc = new Document(inputPdf);

            // Create a visible signature field on the first page (1‑based indexing)
            Rectangle rect = new Rectangle(100, 100, 200, 150); // llx, lly, urx, ury
            SignatureField sigField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "Signature1"
            };
            // Add the field to the document's AcroForm
            doc.Form.Add(sigField, doc.Pages[1].Number);

            // Prepare PKCS7 signature object (the correct class for digital signatures)
            PKCS7 pkcs = new PKCS7(pfxPath, pfxPassword)
            {
                Reason = "Document approved",
                // Configure timestamp server settings via TimestampSettings
                TimestampSettings = new Aspose.Pdf.TimestampSettings(
                    timestampUrl,
                    null, // no basic authentication
                    Aspose.Pdf.DigestHashAlgorithm.Sha256)
            };

            // Sign the field – SignatureField.Sign is the proper way (Signature property is read‑only)
            sigField.Sign(pkcs);

            // Save the signed PDF
            doc.Save(outputPdf);

            Console.WriteLine($"PDF signed successfully: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during signing: {ex.Message}");
        }
    }
}
