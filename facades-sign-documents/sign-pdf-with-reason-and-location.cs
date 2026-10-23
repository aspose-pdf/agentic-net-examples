using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // needed for PKCS7 and signature appearance

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed.pdf";
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

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Create a visible signature field (position can be adjusted as needed)
        // Rectangle(xLL, yLL, xUR, yUR) – lower‑left and upper‑right coordinates
        SignatureField sigField = new SignatureField(doc, new Aspose.Pdf.Rectangle(100, 100, 300, 200))
        {
            Name = "Signature1"
        };
        doc.Form.Add(sigField);

        // Configure PKCS7 signature with reason and location
        PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
        {
            Reason = "Approved for release",
            Location = "New York"
        };

        // Sign the field
        sigField.Sign(pkcs7);

        // Save the signed PDF
        doc.Save(outputPath);

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
