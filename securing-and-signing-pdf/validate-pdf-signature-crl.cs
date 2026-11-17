using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "signed_document.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Ensure the document contains a form (signature fields are stored there)
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("No form fields found in the PDF.");
                return;
            }

            // Iterate over all form fields and process those that are signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField signatureField)
                {
                    // Extract the signing certificate from the signature field
                    X509Certificate2 signingCert = signatureField.ExtractCertificateObject();

                    if (signingCert == null)
                    {
                        Console.WriteLine($"Signature '{signatureField.PartialName}' does not contain a certificate.");
                        continue;
                    }

                    // Build a certificate chain with online CRL checking enabled
                    using (X509Chain chain = new X509Chain())
                    {
                        chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;      // Use CRL distribution points
                        chain.ChainPolicy.RevocationFlag = X509RevocationFlag.ExcludeRoot; // Check all certificates except the root
                        chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 0, 30);    // 30‑second timeout for CRL retrieval
                        chain.ChainPolicy.VerificationFlags = X509VerificationFlags.NoFlag;

                        bool chainBuilt = chain.Build(signingCert);

                        // Evaluate the chain status for revocation information
                        bool isRevoked = false;
                        foreach (X509ChainStatus status in chain.ChainStatus)
                        {
                            if (status.Status == X509ChainStatusFlags.Revoked ||
                                status.Status == X509ChainStatusFlags.RevocationStatusUnknown)
                            {
                                isRevoked = true;
                                Console.WriteLine($"Revocation check failed for '{signatureField.PartialName}': {status.StatusInformation.Trim()}");
                            }
                        }

                        if (chainBuilt && !isRevoked)
                        {
                            Console.WriteLine($"Signature '{signatureField.PartialName}' is valid and not revoked.");
                        }
                        else if (!chainBuilt)
                        {
                            Console.WriteLine($"Signature '{signatureField.PartialName}' failed chain building.");
                            foreach (X509ChainStatus status in chain.ChainStatus)
                            {
                                Console.WriteLine($"  - {status.Status}: {status.StatusInformation.Trim()}");
                            }
                        }
                    }
                }
            }
        }
    }
}
