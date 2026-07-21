using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class ExtractCertificateChain
{
    static void Main()
    {
        const string inputPdf = "signed.pdf";               // source PDF with digital signatures
        const string outputDir = "Certificates";            // folder to store DER files

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            int signatureIndex = 0;
            // Iterate over all form fields and process only SignatureField instances
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    signatureIndex++;
                    // Extract the signing certificate as an X509Certificate2 object
                    X509Certificate2 signingCert = sigField.ExtractCertificateObject();
                    if (signingCert == null)
                    {
                        Console.WriteLine($"Signature field {signatureIndex} does not contain a certificate.");
                        continue;
                    }

                    // Build the certificate chain for the signing certificate
                    using (X509Chain chain = new X509Chain())
                    {
                        // Build the chain; if it fails, we still get whatever elements were found
                        chain.Build(signingCert);

                        for (int j = 0; j < chain.ChainElements.Count; j++)
                        {
                            X509Certificate2 cert = chain.ChainElements[j].Certificate;

                            // Export the certificate in DER format (X509ContentType.Cert)
                            byte[] derBytes = cert.Export(X509ContentType.Cert);

                            // Create a file name that identifies the signature and the chain position
                            string fileName = $"signature{signatureIndex}_cert{j + 1}.der";
                            string outPath = Path.Combine(outputDir, fileName);

                            // Write the DER bytes to disk
                            File.WriteAllBytes(outPath, derBytes);
                            Console.WriteLine($"Saved: {outPath}");
                        }
                    }
                }
            }
        }

        Console.WriteLine("Certificate extraction completed.");
    }
}
