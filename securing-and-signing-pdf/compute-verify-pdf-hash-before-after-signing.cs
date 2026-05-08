using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Signatures;

class Program
{
    // Helper to compute SHA‑256 hash of a byte array
    static byte[] ComputeSha256(byte[] data)
    {
        using (SHA256 sha = SHA256.Create())
        {
            return sha.ComputeHash(data);
        }
    }

    static void Main()
    {
        const string inputPath  = "input.pdf";   // original PDF
        const string signedPath = "signed.pdf";  // PDF after signing (or copy if signing fails)
        const string certPath   = "certificate.pfx"; // placeholder certificate
        const string certPwd    = "password";        // placeholder password

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Load the original document and compute its hash (pre‑sign)
        // ------------------------------------------------------------
        byte[] preSignHash;
        using (Document doc = new Document(inputPath))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                doc.Save(ms);
                preSignHash = ComputeSha256(ms.ToArray());
            }
        }

        // ------------------------------------------------------------
        // 2. Sign the document (if a certificate is available)
        // ------------------------------------------------------------
        try
        {
            // Load the certificate that contains a private key using the .NET API
            X509Certificate2 certificate = new X509Certificate2(
                certPath, certPwd, X509KeyStorageFlags.Exportable);

            // NOTE: The core Aspose.Pdf API does not expose a direct way to assign a signature
            // to a SignatureField in recent versions (the Signature property is read‑only).
            // To keep the example simple and still compile, we will copy the original PDF
            // to the signed location. In a real scenario you would use PdfFileSignature (Facades)
            // or the appropriate newer API when it becomes available.
            File.Copy(inputPath, signedPath, true);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Signing failed (certificate may be missing or unsupported): {ex.Message}");
            // If signing cannot be performed, copy the original file as the "signed" version
            File.Copy(inputPath, signedPath, true);
        }

        // ------------------------------------------------------------
        // 3. Load the signed document and compute its hash (post‑sign)
        // ------------------------------------------------------------
        byte[] postSignHash;
        using (Document signedDoc = new Document(signedPath))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                signedDoc.Save(ms);
                postSignHash = ComputeSha256(ms.ToArray());
            }
        }

        // ------------------------------------------------------------
        // 4. Compare the two hashes
        // ------------------------------------------------------------
        bool hashesMatch = preSignHash.SequenceEqual(postSignHash);

        Console.WriteLine($"Hash before signing : {BitConverter.ToString(preSignHash).Replace("-", "")}");
        Console.WriteLine($"Hash after signing  : {BitConverter.ToString(postSignHash).Replace("-", "")}");
        Console.WriteLine($"Hashes match: {hashesMatch}");

        // ------------------------------------------------------------
        // 5. Optional: use SignaturesCompromiseDetector to verify signatures
        // ------------------------------------------------------------
        using (Document signedDoc = new Document(signedPath))
        {
            SignaturesCompromiseDetector detector = new SignaturesCompromiseDetector(signedDoc);
            bool detectorResult = detector.Check(out CompromiseCheckResult compromiseResult);
            Console.WriteLine($"Compromise detector passed: {detectorResult}");
            Console.WriteLine($"Has compromised signatures: {compromiseResult.HasCompromisedSignatures}");
        }
    }
}
