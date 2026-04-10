using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Signatures;

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
            bool anySignature = false;

            // Iterate over all form fields and look for signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    anySignature = true;
                    Console.WriteLine($"Signature field: {sigField.PartialName}");

                    // Verify the signature using the core Signature API
                    Signature signatureObject = sigField.Signature;
                    bool isValid = signatureObject.Verify();
                    Console.WriteLine($"Signature valid: {isValid}");

                    // Extract the embedded X.509 certificate
                    using (Stream certStream = sigField.ExtractCertificate())
                    {
                        // Read the stream into a byte array
                        byte[] certBytes = new byte[certStream.Length];
                        certStream.Read(certBytes, 0, certBytes.Length);

                        // Load the certificate for analysis
                        X509Certificate2 cert = new X509Certificate2(certBytes);

                        Console.WriteLine("Certificate details:");
                        Console.WriteLine($"  Subject   : {cert.Subject}");
                        Console.WriteLine($"  Issuer    : {cert.Issuer}");
                        Console.WriteLine($"  NotBefore : {cert.NotBefore}");
                        Console.WriteLine($"  NotAfter  : {cert.NotAfter}");
                        Console.WriteLine($"  Thumbprint: {cert.Thumbprint}");
                    }
                }
            }

            if (!anySignature)
            {
                Console.WriteLine("No digital signatures found in the document.");
            }

            // Check for compromised signatures using the detector
            SignaturesCompromiseDetector detector = new SignaturesCompromiseDetector(doc);
            bool notCompromised = detector.Check(out CompromiseCheckResult compromiseResult);

            Console.WriteLine($"Compromise check passed: {notCompromised}");
            Console.WriteLine($"Has compromised signatures: {compromiseResult.HasCompromisedSignatures}");
        }
    }
}