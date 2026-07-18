using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all fields and process only signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    // Extract the X.509 certificate object from the signature field
                    X509Certificate2 cert = sigField.ExtractCertificateObject();

                    if (cert == null)
                    {
                        Console.WriteLine($"No certificate found in signature field: {sigField.PartialName}");
                        continue;
                    }

                    Console.WriteLine($"Signature field: {sigField.PartialName}");
                    Console.WriteLine($"Subject: {cert.Subject}");
                    Console.WriteLine($"Issuer : {cert.Issuer}");
                    Console.WriteLine($"Valid From: {cert.NotBefore}");
                    Console.WriteLine($"Valid To  : {cert.NotAfter}");
                    Console.WriteLine();

                    // Build the certificate chain for the extracted certificate
                    using (X509Chain chain = new X509Chain())
                    {
                        // Build the chain using the default policy (revocation checks are optional)
                        chain.Build(cert);

                        // Iterate over each element in the chain
                        for (int i = 0; i < chain.ChainElements.Count; i++)
                        {
                            X509Certificate2 elementCert = chain.ChainElements[i].Certificate;
                            bool isExpired = elementCert.NotAfter < DateTime.UtcNow;

                            Console.WriteLine($"Chain element {i + 1}:");
                            Console.WriteLine($"  Subject: {elementCert.Subject}");
                            Console.WriteLine($"  Issuer : {elementCert.Issuer}");
                            Console.WriteLine($"  Valid To: {elementCert.NotAfter} {(isExpired ? "(EXPIRED)" : "(valid)")}");
                        }

                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
