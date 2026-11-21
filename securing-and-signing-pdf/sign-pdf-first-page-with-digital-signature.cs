using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed.pdf";
        const string certPath = "cert.pfx";
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

        // Load PDF bytes into an expandable memory stream (fixed‑size streams cause NotSupportedException)
        byte[] pdfBytes = File.ReadAllBytes(inputPath);
        using (var expandableStream = new MemoryStream())
        {
            expandableStream.Write(pdfBytes, 0, pdfBytes.Length);
            expandableStream.Position = 0;

            // Load the document from the expandable stream
            using (var doc = new Document(expandableStream))
            {
                // Create a visible signature field on the first page
                var rect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);
                var sigField = new SignatureField(doc.Pages[1], rect) { PartialName = "Signature1" };
                doc.Form.Add(sigField, 1);

                // Configure PKCS7 signature (core API, no Facades)
                var pkcs7 = new PKCS7(certPath, certPassword)
                {
                    Reason = "Document approval",
                    Location = "New York",
                    ContactInfo = "John Doe",
                    CustomAppearance = new SignatureCustomAppearance
                    {
                        ShowContactInfo = true,
                        FontSize = 8
                    }
                };

                // Sign the field – this writes the signed PDF back into the source stream
                sigField.Sign(pkcs7);
            }

            // The stream now contains the signed PDF; write it out (do NOT call doc.Save())
            File.WriteAllBytes(outputPath, expandableStream.ToArray());
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
