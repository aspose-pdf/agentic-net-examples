using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed.pdf";
        const string outputDir = "certificates";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            using (Document doc = new Document(inputPdf))
            {
                // Iterate over all form fields and pick the signature fields
                var signatureFields = doc?.Form?.Fields?.OfType<SignatureField>();
                if (signatureFields == null || !signatureFields.Any())
                {
                    Console.WriteLine("No signature fields found.");
                    return;
                }

                int sigIndex = 1;
                foreach (SignatureField sigField in signatureFields)
                {
                    // Extract the signing (leaf) certificate from the signature field
                    X509Certificate2 signingCert = sigField.ExtractCertificateObject();
                    if (signingCert != null)
                    {
                        SaveCertificate(signingCert, outputDir, $"sig{sigIndex}_signer.der");

                        // Build the full certificate chain using X509Chain
                        var chainBuilder = new X509Chain();
                        // Optional: you can adjust chain policy here if needed
                        chainBuilder.Build(signingCert);

                        for (int i = 0; i < chainBuilder.ChainElements.Count; i++)
                        {
                            // Skip the leaf certificate – it has already been saved
                            if (chainBuilder.ChainElements[i].Certificate.Thumbprint == signingCert.Thumbprint)
                                continue;

                            string fileName = $"sig{sigIndex}_chain_{i + 1}.der";
                            SaveCertificate(chainBuilder.ChainElements[i].Certificate, outputDir, fileName);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Signature field {sigIndex} does not contain a certificate.");
                    }

                    sigIndex++;
                }
            }

            Console.WriteLine($"Certificates have been saved to '{outputDir}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    static void SaveCertificate(X509Certificate2 cert, string directory, string fileName)
    {
        // Export the certificate in DER (binary) format
        byte[] derBytes = cert.Export(X509ContentType.Cert);
        string path = Path.Combine(directory, fileName);
        File.WriteAllBytes(path, derBytes);
    }
}
