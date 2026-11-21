using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "secured_signed.pdf";
        const string certificatePath = "cert.pfx";
        const string certificatePassword = "certPass";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Verify required files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // -------------------------------------------------
            // Add an invisible signature field on the first page
            // -------------------------------------------------
            // Rectangle with zero size makes the field invisible
            Rectangle rect = new Rectangle(0, 0, 0, 0);
            SignatureField sigField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "Signature1"
            };
            // Add the field to the document's form collection
            doc.Form.Add(sigField, 1);

            // -------------------------------------------------
            // Create a PKCS7 (certificate‑based) signature
            // -------------------------------------------------
            PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePassword)
            {
                // Suppress the default visible text fields
                Reason = string.Empty,
                Location = string.Empty,
                ContactInfo = string.Empty
            };

            // Sign the invisible field with the PKCS7 signature
            sigField.Sign(pkcs7);

            // -------------------------------------------------
            // Encrypt the PDF with password protection (AES‑256)
            // -------------------------------------------------
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the secured and signed PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF has been signed and encrypted successfully: '{outputPath}'");
    }
}
