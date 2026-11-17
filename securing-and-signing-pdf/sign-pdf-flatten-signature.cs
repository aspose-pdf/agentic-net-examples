using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_flattened.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the PDF into an expandable memory stream (required for signing)
        byte[] pdfBytes = File.ReadAllBytes(inputPath);
        using (var expandableStream = new MemoryStream())
        {
            expandableStream.Write(pdfBytes, 0, pdfBytes.Length);
            expandableStream.Position = 0;

            // Load the document from the expandable stream
            using (var doc = new Document(expandableStream))
            {
                // Create a signature field on the first page
                var rect = new Rectangle(100, 600, 300, 650);
                var signatureField = new SignatureField(doc.Pages[1], rect)
                {
                    PartialName = "Signature1"
                };
                // Add the field to the document's form collection
                doc.Form.Add(signatureField, 1);

                // Prepare PKCS#7 signature using the provided certificate
                var pkcs7 = new PKCS7(certPath, certPassword)
                {
                    Reason = "Document approved",
                    Location = "Head Office",
                    ContactInfo = "contact@example.com",
                    // Optional: customize appearance (font size, etc.)
                    CustomAppearance = new SignatureCustomAppearance { FontSize = 8, ShowContactInfo = false }
                };

                // Sign the field – this writes the signed PDF back into the source stream
                signatureField.Sign(pkcs7);

                // Flatten the signature appearance so it cannot be edited visually
                signatureField.Flatten();

                // No doc.Save() – the stream already contains the signed PDF
            }

            // Write the signed, flattened PDF to the output file
            expandableStream.Position = 0;
            File.WriteAllBytes(outputPath, expandableStream.ToArray());
        }

        Console.WriteLine($"Signed and flattened PDF saved to '{outputPath}'.");
    }
}
