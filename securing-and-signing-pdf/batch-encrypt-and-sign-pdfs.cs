using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class BatchEncryptAndSign
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\InputPdfs";
        // Folder where final signed PDFs will be saved
        const string outputFolder = @"C:\SignedPdfs";

        // Encryption passwords
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Digital signature certificate (PFX) and its password
        const string pfxPath = @"C:\Certificates\mycert.pfx";
        const string pfxPassword = "certPass";

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string sourcePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = System.IO.Path.GetFileNameWithoutExtension(sourcePath);
            string encryptedPath = System.IO.Path.Combine(outputFolder, $"{fileName}_encrypted.pdf");
            string signedPath = System.IO.Path.Combine(outputFolder, $"{fileName}_signed.pdf");

            // ---------- Encrypt the PDF ----------
            using (Document doc = new Document(sourcePath))
            {
                // Set desired permissions (example: allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt using AES-256 (preferred algorithm)
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted version
                doc.Save(encryptedPath);
            }

            // ---------- Sign the encrypted PDF ----------
            // Open the encrypted PDF with the user password
            using (Document encDoc = new Document(encryptedPath, userPassword))
            {
                // Ensure the document has at least one page
                if (encDoc.Pages.Count == 0)
                    throw new InvalidOperationException("Document contains no pages.");

                // Choose the first page for the signature field
                Page page = encDoc.Pages[1];

                // Define the rectangle where the signature will appear
                // Fully qualified to avoid ambiguity with Aspose.Pdf.Drawing.Rectangle
                Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create a signature field on the chosen page
                SignatureField signatureField = new SignatureField(page, sigRect);
                page.Annotations.Add(signatureField);

                // Create a concrete PKCS#7 signature object using the PFX file
                PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
                {
                    Reason = "Document signed",
                    Location = "Office",
                    ContactInfo = "admin@example.com"
                };

                // Apply the digital signature to the field
                signatureField.Sign(pkcs7);

                // Save the final signed PDF
                encDoc.Save(signedPath);
            }

            // Optionally delete the intermediate encrypted file
            try { File.Delete(encryptedPath); } catch { /* ignore cleanup errors */ }

            Console.WriteLine($"Processed '{sourcePath}' -> '{signedPath}'");
        }
    }
}
