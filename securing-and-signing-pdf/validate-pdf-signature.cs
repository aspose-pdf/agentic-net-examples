using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class PdfSignatureValidator
{
    static void Main()
    {
        const string pdfPath = "signed_document.pdf";
        const string rootCaPath = "trusted_root_ca.cer";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(rootCaPath))
        {
            Console.Error.WriteLine($"Root CA certificate not found: {rootCaPath}");
            return;
        }

        // Load the trusted root CA certificate using the byte[] overload (avoids the obsolete string ctor)
        X509Certificate2 rootCa = new X509Certificate2(File.ReadAllBytes(rootCaPath));
        // Prepare a custom trust store that contains only the supplied root CA
        var trustedRootStore = new X509Chain();
        trustedRootStore.ChainPolicy.TrustMode = X509ChainTrustMode.CustomRootTrust;
        trustedRootStore.ChainPolicy.CustomTrustStore.Add(rootCa);
        trustedRootStore.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;

        // Open the PDF document
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Retrieve all signature fields from the form (Form.SignatureFields does not exist)
            var signatureFields = pdfDoc.Form?.Fields?.OfType<SignatureField>()?.ToList();

            if (signatureFields == null || signatureFields.Count == 0)
            {
                Console.WriteLine("No digital signatures found in the PDF.");
                return;
            }

            foreach (SignatureField sigField in signatureFields)
            {
                Console.WriteLine($"Signature Name: {sigField.PartialName}");

                // If the field has not been signed yet, Signature will be null
                if (sigField.Signature == null)
                {
                    Console.WriteLine("  Not signed.");
                    continue;
                }

                // Extract the signing certificate from the signature field
                using (Stream certStream = sigField.ExtractCertificate())
                {
                    if (certStream == null || certStream.Length == 0)
                    {
                        Console.WriteLine("  Unable to extract certificate from signature.");
                        continue;
                    }

                    // Load the certificate (byte[] overload – not obsolete)
                    byte[] certBytes;
                    using (var ms = new MemoryStream())
                    {
                        certStream.CopyTo(ms);
                        certBytes = ms.ToArray();
                    }
                    X509Certificate2 signingCert = new X509Certificate2(certBytes);

                    // Build a chain using the custom trust store (only the supplied root CA)
                    using (var chain = new X509Chain())
                    {
                        chain.ChainPolicy.TrustMode = X509ChainTrustMode.CustomRootTrust;
                        chain.ChainPolicy.CustomTrustStore.Add(rootCa);
                        chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;

                        bool isValid = chain.Build(signingCert);
                        Console.WriteLine($"  Valid: {isValid}");

                        if (!isValid)
                        {
                            foreach (X509ChainStatus status in chain.ChainStatus)
                            {
                                Console.WriteLine($"  Error: {status.StatusInformation.Trim()} (Status: {status.Status})");
                            }
                        }
                        else
                        {
                            Console.WriteLine("  Signature is trusted and has a valid certificate chain.");
                        }
                    }
                }
            }
        }
    }
}
