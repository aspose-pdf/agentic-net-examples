using System;
using System.IO;
using System.Collections.Generic;
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

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document contains at least one signature field
            bool hasSignature = false;
            foreach (Field f in doc.Form?.Fields ?? Array.Empty<Field>())
            {
                if (f is SignatureField)
                {
                    hasSignature = true;
                    break;
                }
            }

            if (!hasSignature)
            {
                Console.WriteLine("No signature fields found in the document.");
                return;
            }

            // Prepare validation options (strict mode)
            ValidationOptions valOptions = new ValidationOptions
            {
                ValidationMode = ValidationMode.Strict,
                CheckCertificateChain = true
            };

            // Iterate over each signature field
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    // Verify the signature
                    bool isValid = sigField.Signature.Verify(valOptions, out ValidationResult valResult);
                    Console.WriteLine($"Signature field '{sigField.PartialName}': Valid = {isValid}");

                    // Extract the signing certificate
                    X509Certificate2 cert = sigField.ExtractCertificateObject();
                    if (cert == null)
                    {
                        Console.WriteLine("  No certificate attached to this signature.");
                        continue;
                    }

                    // Display basic certificate info
                    Console.WriteLine($"  Subject: {cert.Subject}");
                    Console.WriteLine($"  Issuer : {cert.Issuer}");

                    // Retrieve Subject Alternative Name (SAN) extension (OID 2.5.29.17)
                    const string sanOid = "2.5.29.17";
                    X509Extension sanExtension = cert.Extensions[sanOid];
                    if (sanExtension != null)
                    {
                        // Decode the SAN extension to a readable string
                        AsnEncodedData asnData = new AsnEncodedData(sanExtension.Oid, sanExtension.RawData);
                        string sanString = asnData.Format(true);
                        Console.WriteLine("  Subject Alternative Names:");
                        Console.WriteLine($"    {sanString.Trim()}");
                    }
                    else
                    {
                        Console.WriteLine("  No Subject Alternative Name extension found.");
                    }
                }
            }
        }
    }
}
