using System;
using System.IO;
using System.Collections;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security;
using System.Security.Cryptography.X509Certificates;

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
            // Ensure the document contains a form
            if (doc.Form == null)
            {
                Console.WriteLine("The document does not contain any form fields.");
                return;
            }

            // Prepare validation options: check the certificate chain and use strict mode
            ValidationOptions valOptions = new ValidationOptions
            {
                CheckCertificateChain = true,
                ValidationMode = ValidationMode.Strict
            };

            // Iterate over each field and process only signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    // The Signature object associated with the field (could be PKCS7Detached, PKCS7, etc.)
                    Signature signature = sigField.Signature;

                    if (signature == null)
                    {
                        Console.WriteLine($"Signature field '{sigField.PartialName}' does not contain a signature.");
                        continue;
                    }

                    // Verify the signature using the validation options
                    bool isValid = signature.Verify(valOptions, out ValidationResult validationResult);

                    Console.WriteLine($"Signature Field: {sigField.PartialName}");
                    Console.WriteLine($"  Valid: {isValid}");
                    Console.WriteLine($"  Validation Status: {validationResult?.Status}");

                    // Retrieve an error message if the property exists (different API versions expose it differently)
                    if (validationResult != null)
                    {
                        var errProp = validationResult.GetType().GetProperty("ErrorMessage");
                        var errMsg = errProp?.GetValue(validationResult) as string;
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            Console.WriteLine($"  Error Message: {errMsg}");
                        }
                    }

                    // If the ValidationResult provides a certificate chain, display it (guarded for API versions where it may be absent)
                    if (validationResult != null)
                    {
                        // Some versions expose CertificateChain, others expose CertificateChainInfo.
                        var chainProp = validationResult.GetType().GetProperty("CertificateChain");
                        var chainInfoProp = validationResult.GetType().GetProperty("CertificateChainInfo");
                        var chain = chainProp?.GetValue(validationResult) as IEnumerable;
                        var chainInfo = chainInfoProp?.GetValue(validationResult) as IEnumerable;

                        var certificates = chain ?? chainInfo;
                        if (certificates != null)
                        {
                            Console.WriteLine("  Certificate Chain:");
                            foreach (var certObj in certificates)
                            {
                                if (certObj is X509Certificate2 cert)
                                {
                                    Console.WriteLine($"    Subject: {cert.Subject}");
                                    Console.WriteLine($"    Issuer : {cert.Issuer}");
                                    Console.WriteLine($"    Expiry : {cert.NotAfter}");
                                }
                            }
                        }
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}
