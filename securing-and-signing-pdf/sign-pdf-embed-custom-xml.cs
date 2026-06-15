using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";
        const string customXmlPath = "customSignature.xml";

        if (!File.Exists(inputPdfPath) ||
            !File.Exists(certPath) ||
            !File.Exists(customXmlPath))
        {
            Console.Error.WriteLine("One or more required files are missing.");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdfPath))
        {
            // Define the rectangle for the signature field (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field on the first page
            SignatureField signatureField = new SignatureField(doc, rect)
            {
                PartialName = "Signature1"
            };
            doc.Pages[1].Annotations.Add(signatureField);

            // Load custom XML data that must be embedded in the signature
            byte[] customXml = File.ReadAllBytes(customXmlPath);

            // Create a PKCS#7 signature object using the certificate
            PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
            {
                Reason = "Compliance with external standard",
                Location = "Company HQ",
                Date = DateTime.UtcNow
            };

            // Embed custom XML data via the CustomSignHash delegate.
            // The delegate receives the document hash and the digest algorithm and must return the signature bytes.
            pkcs7.CustomSignHash = (byte[] hash, DigestHashAlgorithm alg) =>
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(hash, 0, hash.Length);
                    ms.Write(customXml, 0, customXml.Length);
                    return ms.ToArray();
                }
            };

            // Sign the document using the signature field
            signatureField.Sign(pkcs7);

            // Save the signed PDF (lifecycle rule: use Save)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}
