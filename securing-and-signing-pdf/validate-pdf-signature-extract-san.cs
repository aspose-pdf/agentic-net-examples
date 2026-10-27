using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

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
            // Ensure the document contains a form with fields
            if (doc.Form == null || doc.Form.Fields == null)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            bool signatureFound = false;

            // Iterate over all form fields to locate signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    signatureFound = true;

                    // Validate the digital signature (method name may vary by version)
                    bool isValid = false;
                    try
                    {
                        // Prefer ValidateSignature if available, otherwise fall back to VerifySignature via reflection
                        var validateMethod = sigField.GetType().GetMethod("ValidateSignature");
                        if (validateMethod != null)
                        {
                            isValid = (bool)validateMethod.Invoke(sigField, null);
                        }
                        else
                        {
                            var verifyMethod = sigField.GetType().GetMethod("VerifySignature");
                            if (verifyMethod != null)
                            {
                                isValid = (bool)verifyMethod.Invoke(sigField, null);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Signature validation failed with exception: {ex.Message}");
                    }
                    Console.WriteLine($"Signature field '{sigField.PartialName}' validation result: {isValid}");

                    // Retrieve the signing certificate
                    X509Certificate2 cert = null;
                    try
                    {
                        // ExtractCertificateObject is the supported way to obtain the certificate
                        cert = sigField.ExtractCertificateObject();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to extract certificate: {ex.Message}");
                    }

                    if (cert == null)
                    {
                        Console.WriteLine("No certificate associated with the signature.");
                        continue;
                    }

                    // Extract Subject Alternative Name (SAN) entries
                    List<string> sanEntries = new List<string>();
                    foreach (X509Extension ext in cert.Extensions)
                    {
                        // OID for Subject Alternative Name is 2.5.29.17
                        if (ext.Oid?.Value == "2.5.29.17")
                        {
                            // Decode the extension data to a readable string
                            AsnEncodedData asnData = new AsnEncodedData(ext.Oid, ext.RawData);
                            string formatted = asnData.Format(true);
                            // The formatted string may contain multiple entries separated by line breaks
                            foreach (string line in formatted.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                sanEntries.Add(line.Trim());
                            }
                        }
                    }

                    // Output SAN entries
                    if (sanEntries.Count > 0)
                    {
                        Console.WriteLine("Subject Alternative Name entries:");
                        foreach (string san in sanEntries)
                        {
                            Console.WriteLine($"  {san}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Subject Alternative Name entries found in the certificate.");
                    }
                }
            }

            if (!signatureFound)
            {
                Console.WriteLine("No signature fields were found in the document.");
            }
        }
    }
}
