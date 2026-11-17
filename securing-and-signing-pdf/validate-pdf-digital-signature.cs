using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main()
    {
        const string pdfPath = "document.pdf";
        const string signaturePath = "signature.p7s";

        if (!File.Exists(pdfPath) || !File.Exists(signaturePath))
        {
            Console.Error.WriteLine("Required files not found.");
            return;
        }

        try
        {
            // Read the PDF (signed data) and the detached PKCS#7 signature.
            byte[] pdfBytes = File.ReadAllBytes(pdfPath);
            byte[] sigBytes = File.ReadAllBytes(signaturePath);

            // Create a SignedCms object for detached content.
            ContentInfo contentInfo = new ContentInfo(pdfBytes);
            SignedCms signedCms = new SignedCms(contentInfo, true); // true => detached
            signedCms.Decode(sigBytes);

            // Validate the signature and the certificate chain.
            // The 'true' argument forces chain verification.
            signedCms.CheckSignature(true);
            Console.WriteLine("Signature is valid and the certificate chain is trusted.");

            // Optional: detailed chain inspection for each signer.
            foreach (SignerInfo signer in signedCms.SignerInfos)
            {
                X509Certificate2 cert = signer.Certificate;
                using (X509Chain chain = new X509Chain())
                {
                    chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                    chain.ChainPolicy.RevocationFlag = X509RevocationFlag.ExcludeRoot;
                    chain.Build(cert);

                    Console.WriteLine($"Signer: {cert.Subject}");
                    Console.WriteLine($"Chain status count: {chain.ChainStatus.Length}");
                    foreach (X509ChainStatus status in chain.ChainStatus)
                    {
                        Console.WriteLine($" - {status.StatusInformation.Trim()}");
                    }
                }
            }
        }
        catch (CryptographicException ex)
        {
            Console.Error.WriteLine($"Signature validation failed: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
