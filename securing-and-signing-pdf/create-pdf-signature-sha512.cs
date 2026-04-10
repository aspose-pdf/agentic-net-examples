using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_output.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "password";

        // Verify required files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the signature will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "Signature1"
            };
            // Add the signature field to the document's form (creation)
            doc.Form.Add(sigField, 1);

            // Load the signing certificate (PFX)
            X509Certificate2 cert = new X509Certificate2(pfxPath, pfxPassword, X509KeyStorageFlags.Exportable);

            // Create an ExternalSignature specifying SHA‑512 as the digest algorithm
            ExternalSignature externalSig = new ExternalSignature(cert, Aspose.Pdf.DigestHashAlgorithm.Sha512);

            // Sign the field using the configured signature object
            sigField.Sign(externalSig);

            // Save the signed PDF (lifecycle: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}