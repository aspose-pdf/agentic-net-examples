using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchEncryptAndSign
{
    static void Main()
    {
        // Input directory containing PDFs to process
        const string inputDir = @"C:\InputPdfs";
        // Output directory for encrypted and signed PDFs
        const string outputDir = @"C:\OutputPdfs";

        // Passwords for encryption
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Path to the PFX file used for digital signing and its password
        const string pfxPath = @"C:\Certificates\mycert.pfx";
        const string pfxPassword = "pfxPass";

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Process each PDF file in the input directory
        foreach (string pdfFile in Directory.GetFiles(inputDir, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfFile);
            string encryptedPath = Path.Combine(outputDir, $"{fileNameWithoutExt}_enc.pdf");
            string signedPath = Path.Combine(outputDir, $"{fileNameWithoutExt}_signed.pdf");

            // ---------- Encrypt the PDF ----------
            using (Document doc = new Document(pdfFile))
            {
                // Define permissions (example: allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt using AES-256 (preferred algorithm)
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(encryptedPath);
            }

            // ---------- Apply a digital signature ----------
            // Open the encrypted PDF using the user password
            using (Document signedDoc = new Document(encryptedPath, userPassword))
            {
                // Define a rectangle where the signature appearance will be placed
                // (left, bottom, right, top) in points
                Rectangle sigRect = new Rectangle(100, 100, 300, 150);

                // Create a signature field and add it to the document's form
                SignatureField sigField = new SignatureField(signedDoc, sigRect);
                sigField.PartialName = "Signature1"; // set field name
                signedDoc.Form.Add(sigField, 0); // Insert at the beginning of the fields collection

                // Create a PKCS7 signature using the PFX file
                PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
                {
                    Reason = "Document approved",
                    Location = "Office",
                    ContactInfo = "signer@example.com"
                };

                // Sign the document using the signature field
                sigField.Sign(pkcs7);

                // Save the signed PDF
                signedDoc.Save(signedPath);
            }

            Console.WriteLine($"Processed: {pdfFile}");
            Console.WriteLine($"  Encrypted -> {encryptedPath}");
            Console.WriteLine($"  Signed     -> {signedPath}");
        }

        Console.WriteLine("Batch encryption and signing completed.");
    }
}
