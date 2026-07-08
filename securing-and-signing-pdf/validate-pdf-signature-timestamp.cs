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
        const string inputPdf = "signed_document.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all form fields and process signature fields
            foreach (var field in doc.Form)
            {
                if (field is SignatureField sigField)
                {
                    // The underlying signature object
                    Aspose.Pdf.Forms.Signature signature = sigField.Signature;

                    // Set up validation options (strict mode, check certificate chain)
                    ValidationOptions valOptions = new ValidationOptions
                    {
                        ValidationMode = ValidationMode.Strict,
                        CheckCertificateChain = true
                    };

                    // Verify the signature and obtain detailed validation result
                    bool isSignatureValid = signature.Verify(valOptions, out ValidationResult valResult);

                    // Retrieve signing time from the signature
                    DateTime signingTime = signature.Date;

                    // Attempt to get the signer's certificate from the validation result
                    X509Certificate2 signerCert = null;
                    try
                    {
                        // ValidationResult provides the signer's certificate (if available)
                        signerCert = (X509Certificate2)valResult.GetType()
                            .GetProperty("SignerCertificate")?.GetValue(valResult);
                    }
                    catch
                    {
                        // If the property is not present, signerCert remains null
                    }

                    // Determine if the signing timestamp falls within the certificate's validity period
                    bool isTimestampWithinCertValidity = false;
                    if (signerCert != null)
                    {
                        isTimestampWithinCertValidity =
                            signingTime >= signerCert.NotBefore && signingTime <= signerCert.NotAfter;
                    }

                    // Output the verification results
                    Console.WriteLine($"Signature field: {sigField.FullName}");
                    Console.WriteLine($"  Signature valid: {isSignatureValid}");
                    Console.WriteLine($"  Signing time   : {signingTime:u}");
                    if (signerCert != null)
                    {
                        Console.WriteLine($"  Certificate    : {signerCert.Subject}");
                        Console.WriteLine($"  Cert valid from: {signerCert.NotBefore:u} to {signerCert.NotAfter:u}");
                        Console.WriteLine($"  Timestamp within certificate validity: {isTimestampWithinCertValidity}");
                    }
                    else
                    {
                        Console.WriteLine("  Certificate information not available.");
                    }
                }
            }
        }
    }
}