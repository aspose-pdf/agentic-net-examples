using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main()
    {
        // Paths to the original PDF (or any data file) and the detached PKCS#7 signature
        const string dataPath = "document.pdf";
        const string signaturePath = "signature.p7s";

        if (!File.Exists(dataPath))
        {
            Console.Error.WriteLine($"Data file not found: {dataPath}");
            return;
        }

        if (!File.Exists(signaturePath))
        {
            Console.Error.WriteLine($"Signature file not found: {signaturePath}");
            return;
        }

        try
        {
            // Read the original data and the detached signature
            byte[] dataBytes;
            byte[] signatureBytes;

            using (FileStream dataStream = File.OpenRead(dataPath))
            using (MemoryStream ms = new MemoryStream())
            {
                dataStream.CopyTo(ms);
                dataBytes = ms.ToArray();
            }

            using (FileStream sigStream = File.OpenRead(signaturePath))
            using (MemoryStream ms = new MemoryStream())
            {
                sigStream.CopyTo(ms);
                signatureBytes = ms.ToArray();
            }

            // Prepare a SignedCms object for a detached signature
            ContentInfo contentInfo = new ContentInfo(dataBytes);
            SignedCms signedCms = new SignedCms(contentInfo, detached: true);

            // Decode the PKCS#7 signature
            signedCms.Decode(signatureBytes);

            // Verify the signature (throws if invalid)
            signedCms.CheckSignature(verifySignatureOnly: true);
            Console.WriteLine("Signature is cryptographically valid.");

            // Retrieve the signing certificate (first signer)
            if (signedCms.SignerInfos.Count == 0)
            {
                Console.Error.WriteLine("No signer information found in the signature.");
                return;
            }

            SignerInfo signerInfo = signedCms.SignerInfos[0];
            X509Certificate2 signerCert = signerInfo.Certificate;

            if (signerCert == null)
            {
                Console.Error.WriteLine("Signer certificate could not be extracted.");
                return;
            }

            Console.WriteLine($"Signer Subject: {signerCert.Subject}");
            Console.WriteLine($"Signer Issuer : {signerCert.Issuer}");

            // Build and validate the certificate chain
            using (X509Chain chain = new X509Chain())
            {
                // Configure chain policy as needed (e.g., revocation checking)
                chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                chain.ChainPolicy.RevocationFlag = X509RevocationFlag.ExcludeRoot;
                chain.ChainPolicy.VerificationFlags = X509VerificationFlags.NoFlag;
                chain.ChainPolicy.VerificationTime = DateTime.UtcNow;

                bool isChainValid = chain.Build(signerCert);

                Console.WriteLine($"Certificate chain valid: {isChainValid}");

                if (!isChainValid)
                {
                    Console.WriteLine("Chain errors:");
                    foreach (X509ChainStatus status in chain.ChainStatus)
                    {
                        Console.WriteLine($" - {status.StatusInformation.Trim()}");
                    }
                }
                else
                {
                    // Optionally list each element in the chain
                    Console.WriteLine("Certificate chain:");
                    foreach (X509ChainElement element in chain.ChainElements)
                    {
                        Console.WriteLine($" * {element.Certificate.Subject}");
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
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}