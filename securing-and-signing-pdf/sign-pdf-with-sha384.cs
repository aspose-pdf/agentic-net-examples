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
        const string inputPdf   = "input.pdf";          // PDF to be signed
        const string outputPdf  = "signed_output.pdf"; // Resulting signed PDF
        const string pfxPath    = "certificate.pfx";   // PFX containing signing certificate
        const string pfxPassword = "pfxPassword";      // Password for the PFX file

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

        // Load the certificate (private key must be exportable or accessible)
        X509Certificate2 cert = new X509Certificate2(pfxPath, pfxPassword, X509KeyStorageFlags.MachineKeySet);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the signature appearance will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Add a signature field to the first page
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect)
            {
                // Optional: set a name for the field
                PartialName = "Signature1"
            };
            doc.Form.Add(sigField);

            // Create an ExternalSignature specifying SHA‑384 as the digest algorithm
            ExternalSignature externalSig = new ExternalSignature(cert, DigestHashAlgorithm.Sha384);

            // Sign the document using the signature field
            sigField.Sign(externalSig);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Document signed with SHA‑384 and saved to '{outputPdf}'.");
    }
}