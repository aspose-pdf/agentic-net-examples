using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed_document.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (no custom load options needed for a standard PDF)
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document contains at least one signature field
            if (doc.Form == null || doc.Form.Fields == null || !doc.Form.Fields.Any(f => f is SignatureField))
            {
                Console.WriteLine("No signature fields found in the document.");
                return;
            }

            // Iterate over all fields and process only signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is not SignatureField sigField)
                    continue;

                // The underlying signature object
                Signature signature = sigField.Signature;
                if (signature == null)
                {
                    Console.WriteLine($"Signature field '{sigField.PartialName}' does not contain a signature.");
                    continue;
                }

                // Set up validation options (strict mode)
                ValidationOptions options = new ValidationOptions
                {
                    ValidationMode = ValidationMode.Strict,
                    CheckCertificateChain = true
                };

                // Perform verification
                bool isValid = signature.Verify(options, out ValidationResult validationResult);
                Console.WriteLine($"Signature field '{sigField.PartialName}': Valid = {isValid}");

                // Extract the signing certificate
                X509Certificate2 cert = sigField.ExtractCertificateObject();
                if (cert == null)
                {
                    Console.WriteLine("No signing certificate found.");
                    continue;
                }

                Console.WriteLine($"Certificate Subject: {cert.Subject}");
                Console.WriteLine($"Certificate Issuer : {cert.Issuer}");

                // Retrieve Subject Alternative Name (SAN) extension (OID 2.5.29.17)
                X509Extension sanExtension = cert.Extensions["2.5.29.17"];
                if (sanExtension != null)
                {
                    // Format the raw SAN data into a readable string
                    string san = new AsnEncodedData(sanExtension.Oid, sanExtension.RawData).Format(true);
                    Console.WriteLine("Subject Alternative Name (SAN):");
                    Console.WriteLine(san);
                }
                else
                {
                    Console.WriteLine("Subject Alternative Name (SAN) extension not present.");
                }

                // Optionally, you can also inspect the validation result details
                Console.WriteLine($"Validation Result: {validationResult.Status}");
                // Note: ValidationResult does not expose RevocationInfo in the core API.

                Console.WriteLine(new string('-', 50));
            }
        }
    }
}
