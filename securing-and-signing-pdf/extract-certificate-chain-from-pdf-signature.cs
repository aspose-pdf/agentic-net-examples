using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class ExtractCertificateChain
{
    static void Main()
    {
        const string inputPdf  = "signed_document.pdf";
        const string outputDir = "certificates";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all form fields in the document
            foreach (var field in doc.Form)
            {
                // Process only signature fields
                if (field is SignatureField signatureField)
                {
                    // Extract the X509 certificate object from the signature
                    X509Certificate2 cert = signatureField.ExtractCertificateObject();

                    if (cert == null)
                    {
                        Console.WriteLine("No certificate found in this signature field.");
                        continue;
                    }

                    // Build the certificate chain using .NET's X509Chain
                    X509Chain chain = new X509Chain();
                    chain.Build(cert);

                    // Save each certificate in the chain as a DER file
                    for (int i = 0; i < chain.ChainElements.Count; i++)
                    {
                        X509Certificate2 chainCert = chain.ChainElements[i].Certificate;
                        string outPath = Path.Combine(outputDir, $"cert_{i}.der");

                        // Export the certificate in DER (binary) format and write to file
                        byte[] derBytes = chainCert.Export(X509ContentType.Cert);
                        File.WriteAllBytes(outPath, derBytes);

                        Console.WriteLine($"Saved certificate {i} to '{outPath}'.");
                    }
                }
            }
        }

        Console.WriteLine("Certificate extraction completed.");
    }
}