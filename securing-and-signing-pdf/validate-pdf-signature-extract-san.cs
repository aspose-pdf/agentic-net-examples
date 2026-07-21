using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

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

        // Load the PDF (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all fields and filter for signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    // -----------------------------------------------------------------
                    // 1. Validate the signature (core API does not expose a direct Verify method).
                    //    We try to obtain the raw PKCS#7 data via reflection and verify it
                    //    using System.Security.Cryptography.Pkcs.SignedCms.
                    // -----------------------------------------------------------------
                    bool isValid = false;
                    string validationMessage;

                    // Attempt to read the raw signature bytes (property name may vary by version)
                    byte[] pkcs7Data = null;
                    var sigObj = sigField.Signature;
                    var prop = sigObj.GetType().GetProperty("SignatureData") ??
                               sigObj.GetType().GetProperty("SignatureBytes");
                    if (prop != null)
                    {
                        pkcs7Data = prop.GetValue(sigObj) as byte[];
                    }

                    if (pkcs7Data != null && pkcs7Data.Length > 0)
                    {
                        try
                        {
                            SignedCms cms = new SignedCms();
                            cms.Decode(pkcs7Data);
                            cms.CheckSignature(true); // throws if invalid
                            isValid = true;
                            validationMessage = "Signature is valid.";
                        }
                        catch (Exception ex)
                        {
                            isValid = false;
                            validationMessage = $"Signature validation failed: {ex.Message}";
                        }
                    }
                    else
                    {
                        validationMessage = "Signature data not available for verification.";
                    }

                    Console.WriteLine($"Signature field '{sigField.PartialName}': Valid = {isValid}");
                    Console.WriteLine($"  Validation result: {validationMessage}");

                    // -----------------------------------------------------------------
                    // 2. Extract the signing certificate
                    // -----------------------------------------------------------------
                    X509Certificate2 cert = sigField.ExtractCertificateObject();
                    if (cert == null)
                    {
                        Console.WriteLine("  No certificate found in the signature.");
                        continue;
                    }

                    Console.WriteLine($"  Certificate Subject: {cert.Subject}");

                    // -----------------------------------------------------------------
                    // 3. Retrieve Subject Alternative Name (SAN) entries
                    // -----------------------------------------------------------------
                    List<string> sanEntries = new List<string>();
                    foreach (X509Extension ext in cert.Extensions)
                    {
                        // OID 2.5.29.17 corresponds to Subject Alternative Name
                        if (ext.Oid?.Value == "2.5.29.17")
                        {
                            AsnEncodedData asnData = new AsnEncodedData(ext.Oid, ext.RawData);
                            string formatted = asnData.Format(true);
                            sanEntries.Add(formatted);
                        }
                    }

                    if (sanEntries.Count == 0)
                    {
                        Console.WriteLine("  No Subject Alternative Name entries found.");
                    }
                    else
                    {
                        Console.WriteLine("  Subject Alternative Name entries:");
                        foreach (string entry in sanEntries)
                        {
                            Console.WriteLine($"    {entry}");
                        }
                    }
                }
            }
        }
    }
}
