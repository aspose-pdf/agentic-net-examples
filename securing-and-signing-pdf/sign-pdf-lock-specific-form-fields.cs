using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string signedPath = "signed.pdf";
        const string outputPath = "final.pdf";
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

        // ------------------------------------------------------------
        // 1. Sign the PDF using core Aspose.Pdf APIs (no Facades).
        // ------------------------------------------------------------
        Document docToSign = new Document(inputPath);

        // Create a visible signature field (position can be adjusted as needed).
        // Rectangle(xLL, yLL, xUR, yUR) – lower‑left and upper‑right coordinates.
        var sigRect = new Rectangle(100, 100, 300, 200);
        SignatureField sigField = new SignatureField(docToSign, sigRect)
        {
            Name = "Signature1"
        };
        docToSign.Form.Add(sigField);

        // Load the certificate and create a PKCS7 signature object.
        PKCS7 pkcs7 = new PKCS7(certPath, certPassword);

        // Apply the digital signature to the field.
        sigField.Sign(pkcs7);

        // Save the signed PDF.
        docToSign.Save(signedPath);

        // ------------------------------------------------------------
        // 2. Lock specific form fields after signing.
        // ------------------------------------------------------------
        using (Document signedDoc = new Document(signedPath))
        {
            string[] fieldsToLock = { "Name", "Date" };

            foreach (string fieldName in fieldsToLock)
            {
                var field = signedDoc.Form[fieldName];
                if (field != null)
                {
                    field.ReadOnly = true; // Prevent further edits.
                }
                else
                {
                    Console.WriteLine($"Field '{fieldName}' not found in the document.");
                }
            }

            signedDoc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}
