using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "signed.pdf";
        const string caPath = "rootCA.cer";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(caPath))
        {
            Console.Error.WriteLine($"Root CA file not found: {caPath}");
            return;
        }

        // Load the trusted root certificate using the recommended loader to avoid the obsolete constructor warning
        X509Certificate2 rootCert = X509CertificateLoader.LoadCertificateFromFile(caPath);
        X509Certificate2Collection trustedRoots = new X509Certificate2Collection { rootCert };

        // Load the PDF document
        Document doc = new Document(pdfPath);

        bool anySignature = false;
        foreach (var field in doc.Form.Fields)
        {
            if (field is SignatureField sigField)
            {
                anySignature = true;
                Console.WriteLine($"Processing signature field: '{sigField.PartialName}'");

                // Extract the certificate embedded in the signature (if any)
                X509Certificate2? embeddedCert = null;
                try
                {
                    using (Stream certStream = sigField.ExtractCertificate())
                    {
                        if (certStream != null && certStream.Length > 0)
                        {
                            // Load the certificate from the extracted stream
                            byte[] raw = new byte[certStream.Length];
                            certStream.Read(raw, 0, raw.Length);
                            embeddedCert = new X509Certificate2(raw);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"- Unable to extract certificate from signature: {ex.Message}");
                }

                if (embeddedCert == null)
                {
                    Console.WriteLine("- No certificate found in the signature; cannot validate trust chain.");
                    continue;
                }

                // Build a chain using the trusted root collection
                using (X509Chain chain = new X509Chain())
                {
                    // Add the trusted root(s) to the extra store
                    foreach (var root in trustedRoots)
                        chain.ChainPolicy.ExtraStore.Add(root);

                    // Configure chain policy – we want to trust only the supplied roots
                    chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority;
                    chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck; // optional: skip OCSP/CRL checks for this example
                    chain.ChainPolicy.TrustMode = X509ChainTrustMode.CustomRootTrust;

                    bool chainIsValid = chain.Build(embeddedCert);
                    if (chainIsValid)
                    {
                        Console.WriteLine($"Signature '{sigField.PartialName}' is cryptographically valid and chains to a trusted root.");
                    }
                    else
                    {
                        Console.WriteLine($"Signature '{sigField.PartialName}' validation failed. Chain status:");
                        foreach (X509ChainStatus status in chain.ChainStatus)
                        {
                            Console.WriteLine($"- {status.StatusInformation.Trim()} (Status: {status.Status})");
                        }
                    }
                }
            }
        }

        if (!anySignature)
        {
            Console.WriteLine("No signature fields found in the PDF.");
        }
    }
}
