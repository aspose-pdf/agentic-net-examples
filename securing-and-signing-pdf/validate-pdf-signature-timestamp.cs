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
            bool anySignature = false;

            // Iterate over all form fields and process signature fields
            foreach (Field field in doc.Form)
            {
                if (field is SignatureField sigField)
                {
                    anySignature = true;

                    // The embedded signature object
                    Signature signature = sigField.Signature;

                    // Set up validation options (strict mode)
                    ValidationOptions options = new ValidationOptions
                    {
                        ValidationMode = ValidationMode.Strict,
                        CheckCertificateChain = true
                    };

                    // Perform verification
                    ValidationResult validationResult;
                    bool isSignatureValid = signature.Verify(options, out validationResult);

                    Console.WriteLine($"Signature field '{sigField.PartialName}':");
                    Console.WriteLine($"  Verification result: {isSignatureValid}");
                    Console.WriteLine($"  Validation status: {validationResult?.Status}");

                    // Extract the embedded certificate
                    using (Stream certStream = sigField.ExtractCertificate())
                    {
                        // Read the stream into a byte array
                        using (MemoryStream ms = new MemoryStream())
                        {
                            certStream.CopyTo(ms);
                            byte[] certBytes = ms.ToArray();

                            // Load the certificate
                            X509Certificate2 cert = new X509Certificate2(certBytes);

                            // Signing timestamp from the signature
                            DateTime signingTime = signature.Date;

                            // Check if the signing time is within the certificate's validity period
                            bool withinValidity = signingTime >= cert.NotBefore && signingTime <= cert.NotAfter;

                            Console.WriteLine($"  Signing time: {signingTime:u}");
                            Console.WriteLine($"  Certificate valid from {cert.NotBefore:u} to {cert.NotAfter:u}");
                            Console.WriteLine($"  Timestamp within certificate validity: {withinValidity}");
                        }
                    }

                    Console.WriteLine();
                }
            }

            if (!anySignature)
            {
                Console.WriteLine("No digital signatures were found in the document.");
            }
        }
    }
}