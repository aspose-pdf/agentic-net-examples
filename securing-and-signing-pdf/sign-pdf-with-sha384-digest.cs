using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF containing a signature field (e.g., named "Signature1")
        const string inputPdfPath  = "input.pdf";
        // Output PDF with the applied digital signature
        const string outputPdfPath = "signed_output.pdf";
        // Path to the PFX (PKCS#12) certificate file and its password
        const string pfxPath       = "certificate.pfx";
        const string pfxPassword   = "pfxPassword";

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document (wrapped in a using block for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Retrieve the signature field by its name.
            // Adjust the field name if your PDF uses a different identifier.
            SignatureField signatureField = pdfDoc.Form["Signature1"] as SignatureField;
            if (signatureField == null)
            {
                Console.Error.WriteLine("Signature field 'Signature1' not found in the document.");
                return;
            }

            // Load the certificate (contains the private key) from the PFX file.
            X509Certificate2 certificate = new X509Certificate2(pfxPath, pfxPassword);

            // Create an ExternalSignature specifying SHA‑384 as the digest algorithm.
            // The constructor selects the appropriate algorithm based on the certificate
            // and the provided DigestHashAlgorithm value.
            ExternalSignature externalSignature = new ExternalSignature(certificate, DigestHashAlgorithm.Sha384);

            // Apply the signature to the field.
            signatureField.Sign(externalSignature);

            // Save the signed PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Document signed successfully and saved to '{outputPdfPath}'.");
    }
}