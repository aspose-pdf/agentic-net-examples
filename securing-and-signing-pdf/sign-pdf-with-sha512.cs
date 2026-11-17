using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed_output.pdf";
        const string certPath   = "certificate.pfx";
        const string certPass   = "certPassword";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the source PDF into an expandable memory stream (required for signing)
        byte[] sourceBytes = File.ReadAllBytes(inputPdf);
        using (var expandableStream = new MemoryStream())
        {
            // Copy bytes into a stream that can grow – a plain MemoryStream(byte[]) is fixed‑size.
            expandableStream.Write(sourceBytes, 0, sourceBytes.Length);
            expandableStream.Position = 0;

            // Open the document from the expandable stream
            using (var doc = new Document(expandableStream))
            {
                // Create a signature field on the first page (coordinates are in points)
                var sigField = new SignatureField(doc.Pages[1], new Rectangle(100, 100, 300, 150))
                {
                    PartialName = "Signature1"
                };
                doc.Form.Add(sigField, 1);

                // Prepare PKCS7 signature object
                var pkcs7 = new PKCS7(certPath, certPass)
                {
                    Reason      = "Signed by Example Corp",
                    Location    = "Head Office",
                    ContactInfo = "security@example.com",
                    // Optional visual customisation
                    CustomAppearance = new SignatureCustomAppearance
                    {
                        FontSize = 9,
                        ShowContactInfo = false
                    }
                };

                // Configure SHA‑512 as the digest algorithm via TimestampSettings
                // (PKCS7 itself does not expose a DigestAlgorithm property).
                pkcs7.TimestampSettings = new TimestampSettings(
                    "https://freetsa.org/tsr", // TSA URL – replace with your own if needed
                    "",                         // username (empty if not required)
                    Aspose.Pdf.DigestHashAlgorithm.Sha512);

                // Sign the field – this writes the signed PDF back into the source stream.
                sigField.Sign(pkcs7);

                // Do NOT call doc.Save() after signing – the stream already contains the signed PDF.
            }

            // Write the signed PDF bytes to the output file.
            File.WriteAllBytes(outputPdf, expandableStream.ToArray());
        }

        Console.WriteLine($"Document signed successfully and saved to '{outputPdf}'.");
    }
}
