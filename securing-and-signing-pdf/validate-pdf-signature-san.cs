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

        // Load the PDF document (lifecycle rule: use using block)
        using (Document doc = new Document(inputPdf))
        {
            // Prepare validation options (full namespace to avoid invalid using)
            ValidationOptions validationOptions = new ValidationOptions
            {
                // Strict mode ensures the result influences the verification outcome
                ValidationMode = ValidationMode.Strict,
                // Optional: check the certificate chain
                CheckCertificateChain = true
            };

            // Iterate over all fields and filter signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    // Verify the signature using the prepared options
                    bool isValid = sigField.Signature.Verify(validationOptions, out var validationResult);

                    Console.WriteLine($"Signature field '{sigField.PartialName}': Valid = {isValid}");
                    Console.WriteLine($"  Validation result: {validationResult?.Status}");

                    // Extract the signing certificate
                    X509Certificate2 cert = sigField.ExtractCertificateObject();
                    if (cert == null)
                    {
                        Console.WriteLine("  No certificate found in this signature.");
                        continue;
                    }

                    // Retrieve Subject Alternative Name (SAN) entries
                    List<string> sanEntries = GetSubjectAlternativeNames(cert);

                    if (sanEntries.Count == 0)
                    {
                        Console.WriteLine("  No Subject Alternative Name entries found.");
                    }
                    else
                    {
                        Console.WriteLine("  Subject Alternative Name entries:");
                        foreach (string san in sanEntries)
                        {
                            Console.WriteLine($"    - {san}");
                        }
                    }
                }
            }
        }
    }

    // Helper method to extract SAN entries from a certificate
    private static List<string> GetSubjectAlternativeNames(X509Certificate2 certificate)
    {
        var sanList = new List<string>();

        foreach (X509Extension ext in certificate.Extensions)
        {
            // OID 2.5.29.17 corresponds to Subject Alternative Name
            if (ext.Oid?.Value == "2.5.29.17")
            {
                // Decode the raw data as a string (RFC 5280 format)
                // The AsnEncodedData.Format(true) returns a readable multi‑line string
                string formatted = new AsnEncodedData(ext.Oid, ext.RawData).Format(true);
                // Split lines and trim
                foreach (string line in formatted.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    sanList.Add(line.Trim());
                }
            }
        }

        return sanList;
    }
}
