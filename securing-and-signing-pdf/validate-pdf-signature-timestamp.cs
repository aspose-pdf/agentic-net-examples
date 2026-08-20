using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Locate the first signature field in the document
            SignatureField sigField = null;
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField s)
                {
                    sigField = s;
                    break;
                }
            }

            if (sigField == null)
            {
                Console.WriteLine("No signature fields found in the document.");
                return;
            }

            // Retrieve the signature object associated with the field
            Signature signature = sigField.Signature;

            // Configure validation options (strict mode, check certificate chain)
            ValidationOptions valOptions = new ValidationOptions();
            valOptions.ValidationMode = ValidationMode.Strict;
            valOptions.CheckCertificateChain = true;

            // Perform signature verification
            bool isValid = signature.Verify(valOptions, out ValidationResult valResult);
            Console.WriteLine($"Signature verification result: {isValid}");

            // Extract signing timestamp
            DateTime signingTime = signature.Date;

            // Attempt to obtain the signing certificate (if available)
            X509Certificate2 cert = null;
            // Aspose.Pdf does not expose the certificate directly via the Signature base class.
            // If a concrete signature type (PKCS7, PKCS1, ExternalSignature) provides a certificate
            // property, it can be accessed here. For now we keep it null and inform the user.

            if (cert != null)
            {
                // Verify that the signing timestamp falls within the certificate's validity period
                bool withinPeriod = signingTime >= cert.NotBefore && signingTime <= cert.NotAfter;
                Console.WriteLine($"Signing timestamp: {signingTime}");
                Console.WriteLine($"Certificate validity period: {cert.NotBefore} – {cert.NotAfter}");
                Console.WriteLine($"Timestamp within certificate validity: {withinPeriod}");
            }
            else
            {
                Console.WriteLine("Certificate information is not available for this signature.");
                Console.WriteLine($"Signing timestamp: {signingTime}");
            }
        }
    }
}
