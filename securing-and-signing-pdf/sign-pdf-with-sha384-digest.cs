using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Forms; // for SignatureField
using Aspose.Pdf;      // core API
using Aspose.Pdf.Forms; // signature related types
using Aspose.Pdf;      // ensure core namespace is included
using Aspose.Pdf;      // redundant but harmless
using Aspose.Pdf;      // keep core namespace

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF (must contain a blank signature field)
        const string outputPdf = "signed_output.pdf"; // destination PDF
        const string pfxPath = "certificate.pfx";    // PKCS#12 certificate file
        const string pfxPassword = "password";       // certificate password
        const string signatureFieldName = "Signature1"; // name of the blank signature field

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
            using (Document doc = new Document(inputPdf))
            {
                // Load the certificate (private key required for signing)
                X509Certificate2 cert = new X509Certificate2(pfxPath, pfxPassword);

                // Create an ExternalSignature that uses SHA‑384 as the digest algorithm
                ExternalSignature externalSig = new ExternalSignature(cert, DigestHashAlgorithm.Sha384);

                // Retrieve the blank signature field by name
                SignatureField sigField = doc.Form[signatureFieldName] as SignatureField;
                if (sigField == null)
                {
                    Console.Error.WriteLine($"Signature field '{signatureFieldName}' not found.");
                    return;
                }

                // Sign the field using the prepared ExternalSignature
                sigField.Sign(externalSig);

                // Save the signed PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF signed successfully. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during signing: {ex.Message}");
        }
    }
}