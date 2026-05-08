using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "password";

        if (!File.Exists(inputPdf) || !File.Exists(pfxPath))
        {
            Console.Error.WriteLine("Input PDF or certificate file not found.");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Define the signature rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect)
            {
                Name = "Signature1"
            };
            doc.Form.Add(sigField);

            // Load the signing certificate
            X509Certificate2 cert = new X509Certificate2(pfxPath, pfxPassword);

            // Create an external signature using SHA‑384
            ExternalSignature externalSig = new ExternalSignature(cert, Aspose.Pdf.DigestHashAlgorithm.Sha384)
            {
                Reason = "Approved",
                ContactInfo = "john.doe@example.com",
                Location = "New York"
            };

            // Sign the signature field
            sigField.Sign(externalSig);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF signed with SHA‑384 and saved to '{outputPdf}'.");
    }
}