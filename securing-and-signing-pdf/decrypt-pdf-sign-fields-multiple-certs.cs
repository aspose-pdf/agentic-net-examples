using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string outputPath = "re_signed.pdf";
        const string userPassword = "user123";

        // Certificate files and passwords – one per signature field (cycle if needed)
        string[] certPaths = { "cert1.pfx", "cert2.pfx" };
        string[] certPasswords = { "pass1", "pass2" };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the encrypted PDF with the user password
        using (Document doc = new Document(inputPath, userPassword))
        {
            // Decrypt the document (no parameters required)
            doc.Decrypt();

            // Ensure the document contains form fields
            if (doc.Form == null || doc.Form.Fields == null)
            {
                Console.WriteLine("No form fields found.");
                doc.Save(outputPath);
                return;
            }

            // Gather all signature fields in the document
            List<SignatureField> signatureFields = new List<SignatureField>();
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    signatureFields.Add(sigField);
                }
            }

            // Re‑sign each signature field with a different certificate
            for (int i = 0; i < signatureFields.Count; i++)
            {
                SignatureField sigField = signatureFields[i];

                // Select certificate (cycle if fewer certificates than fields)
                string certPath = certPaths[i % certPaths.Length];
                string certPassword = certPasswords[i % certPasswords.Length];

                if (!File.Exists(certPath))
                {
                    Console.Error.WriteLine($"Certificate not found: {certPath}");
                    continue;
                }

                // Create a PKCS#7 digital signature
                PKCS7 pkcs7 = new PKCS7(certPath, certPassword);

                // Optional visual appearance – set via PKCS7.CustomAppearance
                SignatureCustomAppearance appearance = new SignatureCustomAppearance
                {
                    ShowContactInfo = true,
                    ShowLocation = true,
                    ShowReason = true,
                    ContactInfoLabel = "Contact:",
                    LocationLabel = "Location:",
                    ReasonLabel = "Reason:"
                };
                pkcs7.CustomAppearance = appearance;

                // Optional metadata (reason, location, contact info)
                pkcs7.Reason = "Document re‑signed";
                pkcs7.Location = "Office";
                pkcs7.ContactInfo = "signer@example.com";

                // Apply the signature to the field
                sigField.Sign(pkcs7);
            }

            // Save the re‑signed PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Re‑signed PDF saved to '{outputPath}'.");
    }
}
