using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main()
    {
        const string pdfPath = "signed.pdf";
        const string outputDir = "certs";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document using the core API
        Document pdfDoc = new Document(pdfPath);

        // Form fields are stored in an array; use Length to check for emptiness
        if (pdfDoc.Form == null || pdfDoc.Form.Fields == null || pdfDoc.Form.Fields.Length == 0)
        {
            Console.WriteLine("No form fields found in the PDF.");
            return;
        }

        bool signatureFound = false;

        // Iterate over all form fields and process the first SignatureField encountered
        foreach (Field field in pdfDoc.Form.Fields)
        {
            if (field is SignatureField sigField)
            {
                signatureFound = true;

                // Extract the signing certificate from the signature field
                X509Certificate2 signingCert = sigField.ExtractCertificateObject();
                if (signingCert == null)
                {
                    Console.WriteLine("Signature field does not contain a certificate.");
                    continue;
                }

                // Build the certificate chain using the system's chain engine
                var chain = new X509Chain();
                chain.Build(signingCert);

                if (chain.ChainElements.Count == 0)
                {
                    Console.WriteLine("Unable to build a certificate chain for the signature.");
                    continue;
                }

                // Save each certificate in the chain as a DER file
                for (int i = 0; i < chain.ChainElements.Count; i++)
                {
                    var cert = chain.ChainElements[i].Certificate;
                    string certPath = Path.Combine(outputDir, $"cert_{i + 1}.der");
                    byte[] derBytes = cert.Export(X509ContentType.Cert);
                    File.WriteAllBytes(certPath, derBytes);
                    Console.WriteLine($"Certificate {i + 1} saved to: {certPath}");
                }

                // If only the first signature is required, uncomment the next line
                // break;
            }
        }

        if (!signatureFound)
        {
            Console.WriteLine("No signature fields found in the PDF.");
        }
    }
}
