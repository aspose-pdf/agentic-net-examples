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
        const string certificate = "certificate.pfx";
        const string certPassword = "password";

        // Validate required files
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certificate))
        {
            Console.Error.WriteLine($"Certificate not found: {certificate}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPdf);

        // Create a signature field with the desired rectangle (llx, lly, urx, ury)
        SignatureField sigField = new SignatureField(doc, new Aspose.Pdf.Rectangle(100, 100, 200, 150))
        {
            Name = "Signature1"
        };
        doc.Form.Add(sigField);

        // Create PKCS7 signature object and configure a semi‑transparent yellow background
        PKCS7 pkcs = new PKCS7(certificate, certPassword)
        {
            Reason = string.Empty,
            ContactInfo = string.Empty,
            Location = string.Empty,
            CustomAppearance = new SignatureCustomAppearance
            {
                // Alpha 128 (≈50% opacity) + yellow (R=255, G=255, B=0)
                BackgroundColor = Aspose.Pdf.Color.FromArgb(128, 255, 255, 0)
            }
        };

        // Apply the signature to the field
        sigField.Sign(pkcs);

        // Save the signed PDF
        doc.Save(outputPdf);

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
