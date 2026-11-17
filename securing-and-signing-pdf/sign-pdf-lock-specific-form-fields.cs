using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_locked.pdf";
        const string certPath = "cert.pfx";
        const string certPassword = "password";

        if (!File.Exists(inputPath) || !File.Exists(certPath))
        {
            Console.Error.WriteLine("Input PDF or certificate file not found.");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle for the visible signature (Aspose.Pdf.Rectangle)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 200, 150);

            // Create a signature field on the first page and add it to the form
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect)
            {
                PartialName = "Signature1"
            };
            doc.Form.Add(sigField, 1);

            // Configure the PKCS#7 digital signature
            PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
            {
                Reason = "Document approved",
                Location = "Head Office",
                ContactInfo = "contact@example.com"
            };

            // Sign the field (the field's Signature property is read‑only and is populated by this call)
            sigField.Sign(pkcs7);

            // Lock the desired form fields after signing
            string[] fieldsToLock = { "Name", "Date", "Signature1" };
            foreach (string fieldName in fieldsToLock)
            {
                var field = doc.Form[fieldName];
                if (field != null)
                {
                    field.ReadOnly = true; // Prevent further edits
                }
            }

            // Save the signed PDF with the locked fields
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF with locked fields saved to '{outputPath}'.");
    }
}
