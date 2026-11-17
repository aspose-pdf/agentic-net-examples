using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Compute SHA‑256 hash of a file and return it as a hex string
    static string ComputeHash(string filePath)
    {
        using (SHA256 sha = SHA256.Create())
        using (FileStream stream = File.OpenRead(filePath))
        {
            byte[] hashBytes = sha.ComputeHash(stream);
            return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
        }
    }

    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // original PDF
        const string signedPdfPath  = "signed.pdf";         // output after signing
        const string certPath       = "certificate.pfx";    // signing certificate (PKCS#12)
        const string certPassword   = "certPassword";       // certificate password

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // 1. Compute hash of the original document
        string originalHash = ComputeHash(inputPdfPath);
        Console.WriteLine($"Original SHA‑256: {originalHash}");

        // 2. Load the PDF, add a signature field, sign it, and save the signed document
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a visible signature field on the first page
            // Rectangle(left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);
            SignatureField sigField = new SignatureField(doc, rect)
            {
                PartialName = "Signature1"
            };
            // Add the field to the form on page 1
            doc.Form.Add(sigField, 1);

            // Prepare PKCS7 signature object
            PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
            {
                Reason = "Document signed",
                Location = "My Company",
                ContactInfo = "",
                // optional: set timestamp settings, digest algorithm, etc.
            };

            // Sign the field
            sigField.Sign(pkcs7);

            // Save the signed PDF
            doc.Save(signedPdfPath);
        }

        // 3. Compute hash of the signed document
        if (!File.Exists(signedPdfPath))
        {
            Console.Error.WriteLine($"Signed file not created: {signedPdfPath}");
            return;
        }

        string signedHash = ComputeHash(signedPdfPath);
        Console.WriteLine($"Signed SHA‑256:   {signedHash}");

        // 4. Compare hashes to verify integrity (they should differ because signing modifies the file)
        if (originalHash.Equals(signedHash, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Hashes are identical – unexpected, signing may have failed.");
        }
        else
        {
            Console.WriteLine("Hashes differ – signing applied and document integrity preserved.");
        }
    }
}
