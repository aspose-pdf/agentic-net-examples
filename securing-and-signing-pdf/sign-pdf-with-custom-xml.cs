using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Added for Border class

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_output.pdf";
        const string pfxPath        = "certificate.pfx";
        const string pfxPassword    = "pfxPassword";
        const string customXmlPath  = "customSignatureData.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }
        if (!File.Exists(customXmlPath))
        {
            Console.Error.WriteLine($"Custom XML file not found: {customXmlPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Define the rectangle where the signature field will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Add a signature field to the first page
            SignatureField sigField = new SignatureField(pdfDoc.Pages[1], sigRect)
            {
                Name = "Signature1",
                // Optional visual properties
                Color = Aspose.Pdf.Color.LightGray
            };
            // Border must be set after the annotation instance is created
            sigField.Border = new Border(sigField) { Width = 1 };
            pdfDoc.Pages[1].Annotations.Add(sigField);

            // Load the certificate (PFX) into a stream – the stream is required by PKCS7 ctor
            using (FileStream pfxStream = File.OpenRead(pfxPath))
            {
                // Create a PKCS7 signature object using the certificate stream
                PKCS7 pkcs7Signature = new PKCS7(pfxStream, pfxPassword)
                {
                    Reason   = "Document approved",
                    Location = "Head Office",
                    Date     = DateTime.UtcNow,
                    // Embed custom XML data via the CustomSignHash delegate
                    CustomSignHash = (hash, digestHashAlgorithm) =>
                    {
                        // Load custom XML that must be part of the signature (not used directly in this demo)
                        byte[] xmlBytes = File.ReadAllBytes(customXmlPath);
                        // In a real scenario you would incorporate xmlBytes into the CMS structure.
                        // Here we simply sign the provided hash using the certificate's private key.
                        using (X509Certificate2 cert = new X509Certificate2(pfxPath, pfxPassword))
                        {
                            using (RSA rsa = cert.GetRSAPrivateKey())
                            {
                                // Sign the hash using SHA‑256 and PKCS#1 v1.5 padding
                                return rsa.SignHash(hash, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                            }
                        }
                    }
                };

                // Sign the document using the signature field
                sigField.Sign(pkcs7Signature);
            }

            // Save the signed PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed and saved to '{outputPdfPath}'.");
    }
}
