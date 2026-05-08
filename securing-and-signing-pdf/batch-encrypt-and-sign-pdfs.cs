using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchEncryptAndSign
{
    static void Main()
    {
        // Resolve folders relative to the current working directory – works on Windows, Linux and macOS
        string baseDir = Directory.GetCurrentDirectory();
        string inputFolder = Path.Combine(baseDir, "InputPdfs");
        string encryptedFolder = Path.Combine(baseDir, "EncryptedPdfs");
        string signedFolder = Path.Combine(baseDir, "SignedPdfs");

        // Digital certificate (PFX) used for signing – also resolved relative to the base directory
        string pfxPath = Path.Combine(baseDir, "Certificates", "signcert.pfx");
        const string pfxPassword = "certPassword";

        // Encryption passwords
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Ensure output directories exist
        Directory.CreateDirectory(encryptedFolder);
        Directory.CreateDirectory(signedFolder);

        // Verify that the input folder exists – if not, give a clear message
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string encryptedPath = Path.Combine(encryptedFolder, fileNameWithoutExt + "_enc.pdf");
            string signedPath = Path.Combine(signedFolder, fileNameWithoutExt + "_signed.pdf");

            // ---------- Encrypt ----------
            using (Document doc = new Document(pdfPath))
            {
                // Set desired permissions (example: allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt using AES-256 algorithm
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save encrypted PDF
                doc.Save(encryptedPath);
            }

            // ---------- Sign ----------
            // Open the encrypted PDF using the user password
            using (Document signedDoc = new Document(encryptedPath, userPassword))
            {
                // Create a signature field on the first page (position can be adjusted as needed)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                SignatureField sigField = new SignatureField(signedDoc.Pages[1], rect);
                signedDoc.Form.Add(sigField);

                // Initialize the concrete PKCS7 signature object with the certificate
                PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword);
                // Optional: set additional signature properties
                pkcs7.Reason = "Document approved";
                pkcs7.Location = "Office";
                pkcs7.ContactInfo = "contact@example.com";

                // Apply the digital signature to the field
                sigField.Sign(pkcs7);

                // Save the signed PDF
                signedDoc.Save(signedPath);
            }

            Console.WriteLine($"Processed: {pdfPath}");
            Console.WriteLine($"  Encrypted -> {encryptedPath}");
            Console.WriteLine($"  Signed     -> {signedPath}");
        }
    }
}
