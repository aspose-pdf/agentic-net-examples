using System;
using System.IO;
using System.Linq; // needed for Count() extension method
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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form with fields
            if (doc.Form == null || doc.Form.Fields == null || doc.Form.Fields.Count() == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Iterate over each field and process only signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    Console.WriteLine($"Signature field name: {sigField.PartialName}");

                    // Extract the X509 certificate object from the signature field
                    X509Certificate2 cert = sigField.ExtractCertificateObject();

                    if (cert == null)
                    {
                        Console.WriteLine("  No certificate attached to this signature.");
                        continue;
                    }

                    // Build the certificate chain using .NET's X509Chain
                    X509Chain chain = new X509Chain();
                    // Optional: adjust chain.ChainPolicy if needed (e.g., revocation checking)
                    bool chainBuilt = chain.Build(cert);

                    if (!chainBuilt)
                    {
                        Console.WriteLine("  Unable to build a valid certificate chain.");
                    }

                    // Iterate over each element in the chain
                    for (int i = 0; i < chain.ChainElements.Count; i++)
                    {
                        X509Certificate2 elementCert = chain.ChainElements[i].Certificate;
                        string subject = elementCert.Subject;
                        DateTime notAfter = elementCert.NotAfter;
                        bool isExpired = notAfter < DateTime.UtcNow;

                        Console.WriteLine($"  Chain element {i + 1}:");
                        Console.WriteLine($"    Subject      : {subject}");
                        Console.WriteLine($"    Expiration   : {notAfter:u}");
                        Console.WriteLine($"    Expired?     : {(isExpired ? "Yes" : "No")}");
                    }

                    // Additionally, check the leaf certificate's expiration directly
                    bool leafExpired = cert.NotAfter < DateTime.UtcNow;
                    Console.WriteLine($"  Leaf certificate expired: {(leafExpired ? "Yes" : "No")}");
                    Console.WriteLine();
                }
            }
        }
    }
}
