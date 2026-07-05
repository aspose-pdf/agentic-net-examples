using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed_document.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all form fields and find signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    Console.WriteLine($"Processing signature field: {sigField.PartialName}");

                    // Verify the signature (basic verification)
                    bool isValid = sigField.Signature?.Verify() ?? false;
                    Console.WriteLine($"Signature valid: {isValid}");

                    // Extract the signing certificate
                    X509Certificate2 cert = sigField.ExtractCertificateObject();
                    if (cert == null)
                    {
                        Console.WriteLine("No certificate found in the signature.");
                        continue;
                    }

                    Console.WriteLine($"Certificate Subject: {cert.Subject}");

                    // Retrieve Subject Alternative Name (SAN) extension (OID 2.5.29.17)
                    const string sanOid = "2.5.29.17";
                    foreach (X509Extension ext in cert.Extensions)
                    {
                        if (ext.Oid?.Value == sanOid)
                        {
                            AsnEncodedData asnData = new AsnEncodedData(ext.Oid, ext.RawData);
                            string sanString = asnData.Format(true);
                            Console.WriteLine("Subject Alternative Name(s):");
                            Console.WriteLine(sanString);
                        }
                    }
                }
            }
        }
    }
}
