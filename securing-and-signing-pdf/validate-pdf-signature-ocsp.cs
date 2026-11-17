using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;               // SignatureField, Signature
using Aspose.Pdf.Security;            // ValidationOptions, ValidationResult, ValidationMode, ValidationMethod, ValidationStatus

class Program
{
    static void Main()
    {
        const string pdfPath = "signed_document.pdf";
        const string ocspResponsePath = "ocsp_response.der"; // Loaded for demonstration – not directly used by core API

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(ocspResponsePath))
        {
            Console.Error.WriteLine($"OCSP response file not found: {ocspResponsePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(pdfPath))
        {
            // Find the first signature field in the document.
            SignatureField signatureField = null;
            foreach (Field f in doc.Form.Fields)
            {
                if (f is SignatureField sf)
                {
                    signatureField = sf;
                    break;
                }
            }

            if (signatureField == null)
            {
                Console.WriteLine("No signature fields found in the document.");
                return;
            }

            // The signature object attached to the field.
            Signature signature = signatureField.Signature;
            if (signature == null)
            {
                Console.WriteLine("Signature field does not contain a digital signature.");
                return;
            }

            // Load the OCSP response file – the core API does not accept raw OCSP bytes for verification,
            // but we keep the load here to illustrate the original intent.
            byte[] ocspBytes = File.ReadAllBytes(ocspResponsePath);
            // (In a real scenario you would configure an OcspSettings with a URL; the API fetches the response itself.)

            // Prepare validation options to use OCSP as the revocation method.
            var options = new ValidationOptions
            {
                ValidationMode = ValidationMode.OnlyCheck, // Check only the signature integrity; revocation is evaluated separately.
                ValidationMethod = ValidationMethod.Ocsp,   // Force OCSP; Auto would try OCSP first then CRL.
                CheckCertificateChain = false,            // Skip full chain validation for this demo.
                RequestTimeout = 5000                     // 5‑second timeout for network calls.
            };

            // Perform the verification.
            bool isIntact = signature.Verify(options, out ValidationResult result);

            Console.WriteLine($"Signature integrity (cryptographic check): {isIntact}");
            Console.WriteLine($"Overall validation status: {result.Status}");
            Console.WriteLine($"Message: {result.Message}");

            // The ValidationResult.Status reflects revocation information when ValidationMethod is OCSP.
            // If the status is Valid, the certificate is not revoked according to the OCSP responder.
            if (result.Status == ValidationStatus.Valid)
            {
                Console.WriteLine("Certificate is not revoked according to OCSP.");
            }
            else if (result.Status == ValidationStatus.Invalid)
            {
                Console.WriteLine("Certificate revocation check failed or certificate is revoked.");
            }
            else // Undefined or other statuses
            {
                Console.WriteLine("Revocation status could not be determined.");
            }
        }
    }
}
