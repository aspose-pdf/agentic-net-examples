using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed.pdf";
        const string outputDir = "certs";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdf))
        {
            // Check for signature fields
            if (doc.Form == null || doc.Form.Fields == null)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            int signatureIndex = 1;

            // Iterate over each field and process only signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    // Extract the leaf certificate as an X509Certificate2 object
                    X509Certificate2 leafCert = sigField.ExtractCertificateObject();

                    if (leafCert == null)
                    {
                        Console.WriteLine($"Signature {signatureIndex}: no certificate extracted.");
                        signatureIndex++;
                        continue;
                    }

                    // Build the certificate chain for the extracted leaf certificate
                    X509Chain chain = new X509Chain();
                    chain.Build(leafCert);

                    int certIndex = 0;

                    // Save each certificate in the chain as a DER file
                    foreach (X509ChainElement element in chain.ChainElements)
                    {
                        byte[] derBytes = element.Certificate.Export(X509ContentType.Cert);
                        string fileName = $"sig{signatureIndex}_cert{certIndex}.der";
                        string filePath = Path.Combine(outputDir, fileName);
                        File.WriteAllBytes(filePath, derBytes);
                        Console.WriteLine($"Saved certificate: {filePath}");
                        certIndex++;
                    }

                    signatureIndex++;
                }
            }
        }
    }
}
