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

        // Open the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all form fields and process only signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    // Retrieve the embedded signature object
                    Signature signature = sigField.Signature;

                    // Prepare validation options (strict mode)
                    ValidationOptions valOptions = new ValidationOptions
                    {
                        ValidationMode = ValidationMode.Strict,
                        CheckCertificateChain = true
                    };

                    // Verify the signature and obtain detailed validation result
                    bool isSignatureValid = signature.Verify(valOptions, out ValidationResult valResult);

                    // Extract the signing certificate (if present)
                    X509Certificate2 cert = sigField.ExtractCertificateObject();

                    // Retrieve the signing timestamp
                    DateTime signingTime = signature.Date;

                    // Check that the signing time falls within the certificate's validity period
                    bool isTimestampWithinCertValidity = false;
                    if (cert != null)
                    {
                        isTimestampWithinCertValidity = signingTime >= cert.NotBefore && signingTime <= cert.NotAfter;
                    }

                    // Output the verification details
                    Console.WriteLine($"Signature Field: {sigField.PartialName}");
                    Console.WriteLine($"  Signature valid (basic): {isSignatureValid}");
                    // ValidationResult does not expose an ErrorMessage property in recent Aspose.Pdf versions.
                    // Use ToString() (or other available members) to convey validation information.
                    Console.WriteLine($"  Validation result: {valResult?.ToString() ?? "None"}");
                    Console.WriteLine($"  Signing time: {signingTime:u}");
                    if (cert != null)
                    {
                        Console.WriteLine($"  Certificate subject: {cert.Subject}");
                        Console.WriteLine($"  Certificate validity: {cert.NotBefore:u} – {cert.NotAfter:u}");
                        Console.WriteLine($"  Timestamp within certificate validity: {isTimestampWithinCertValidity}");
                    }
                    else
                    {
                        Console.WriteLine("  No certificate embedded in the signature.");
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}
