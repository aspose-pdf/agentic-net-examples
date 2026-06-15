using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // SignatureField, ExternalSignature, DigestHashAlgorithm, etc.

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // ------------------------------------------------------------
            // 1. Obtain the signing certificate from the smart card store.
            //    The certificate must have an associated private key.
            //    This example picks the first suitable certificate.
            // ------------------------------------------------------------
            X509Certificate2 signingCert = GetSmartCardCertificate();
            if (signingCert == null)
            {
                Console.Error.WriteLine("No suitable certificate with a private key was found on the smart card.");
                return;
            }

            // ------------------------------------------------------------
            // 2. Create an ExternalSignature that uses the certificate.
            //    Set a custom hash delegate to avoid UI PIN prompts.
            // ------------------------------------------------------------
            ExternalSignature externalSig = new ExternalSignature(signingCert);

            // The delegate receives the hash bytes and the hash algorithm identifier.
            externalSig.CustomSignHash = (byte[] hash, DigestHashAlgorithm digestAlg) =>
            {
                using (RSA rsa = signingCert.GetRSAPrivateKey())
                {
                    // Map Aspose's DigestHashAlgorithm to .NET's HashAlgorithmName
                    HashAlgorithmName hashAlg = digestAlg switch
                    {
                        DigestHashAlgorithm.Sha1   => HashAlgorithmName.SHA1,
                        DigestHashAlgorithm.Sha256 => HashAlgorithmName.SHA256,
                        DigestHashAlgorithm.Sha384 => HashAlgorithmName.SHA384,
                        DigestHashAlgorithm.Sha512 => HashAlgorithmName.SHA512,
                        _ => HashAlgorithmName.SHA256 // fallback
                    };

                    // Sign the pre‑computed hash using the smart‑card private key.
                    return rsa.SignHash(hash, hashAlg, RSASignaturePadding.Pkcs1);
                }
            };

            // ------------------------------------------------------------
            // 3. Ensure a signature field exists on the first page.
            //    If it does not exist, create one at a desired location.
            // ------------------------------------------------------------
            SignatureField sigField = pdfDoc.Form["Signature1"] as SignatureField;
            if (sigField == null)
            {
                // Define the rectangle where the signature will appear.
                // Fully qualified to avoid ambiguity with System.Drawing.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                sigField = new SignatureField(pdfDoc.Pages[1], rect)
                {
                    PartialName = "Signature1"
                };
                pdfDoc.Form.Add(sigField);
            }

            // ------------------------------------------------------------
            // 4. Sign the document using the external signature.
            // ------------------------------------------------------------
            sigField.Sign(externalSig);

            // ------------------------------------------------------------
            // 5. Save the signed PDF.
            // ------------------------------------------------------------
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }

    // Helper method to retrieve a certificate with a private key from the smart‑card store.
    private static X509Certificate2 GetSmartCardCertificate()
    {
        // Smart cards are usually accessed via the CurrentUser\My store.
        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly);
            // Filter certificates that have a private key and are not expired.
            var cert = store.Certificates
                .OfType<X509Certificate2>()
                .FirstOrDefault(c => c.HasPrivateKey && c.NotAfter > DateTime.Now);
            store.Close();
            return cert;
        }
    }
}
