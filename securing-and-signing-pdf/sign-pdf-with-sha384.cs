using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // PDF to be signed
        const string outputPdf  = "signed_output.pdf";  // Resulting signed PDF
        const string pfxPath    = "certificate.pfx";    // PFX containing the signing certificate
        const string pfxPassword = "pfxPassword";       // Password for the PFX file

        // Verify that required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the certificate (contains the private key)
        X509Certificate2 certificate = new X509Certificate2(pfxPath, pfxPassword);

        // Create an ExternalSignature that explicitly uses SHA‑384
        ExternalSignature signature = new ExternalSignature(certificate, DigestHashAlgorithm.Sha384);

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a signature field on the first page (adjust rectangle as needed)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);
            SignatureField sigField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "Signature1"
            };

            // Add the signature field to the document's form collection
            doc.Form.Add(sigField, 1);

            // Sign the document using the prepared ExternalSignature
            sigField.Sign(signature);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Document signed with SHA‑384 and saved to '{outputPdf}'.");
    }
}