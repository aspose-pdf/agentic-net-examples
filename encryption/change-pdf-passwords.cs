using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths
        const string originalPdfPath = "original.pdf";
        const string encryptedPdfPath = "encrypted.pdf";
        const string reencryptedPdfPath = "reencrypted.pdf";

        // Initial passwords
        const string initialUserPassword = "user123";
        const string initialOwnerPassword = "owner123";

        // New passwords
        const string newUserPassword = "newUser456";
        const string newOwnerPassword = "newOwner456";

        // Ensure the source PDF exists
        if (!File.Exists(originalPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {originalPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Encrypt the original PDF with the initial passwords
        // -----------------------------------------------------------------
        try
        {
            using (Document doc = new Document(originalPdfPath))
            {
                // Define permissions (example: allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt using AES-256
                doc.Encrypt(initialUserPassword,
                            initialOwnerPassword,
                            perms,
                            CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(encryptedPdfPath);
            }

            Console.WriteLine($"Encrypted PDF saved to '{encryptedPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Encryption failed: {ex.Message}");
            return;
        }

        // -----------------------------------------------------------------
        // 2. Open the encrypted PDF with the owner password and change passwords
        // -----------------------------------------------------------------
        try
        {
            // Open using the owner password (owner password can open the document regardless of restrictions)
            using (Document encryptedDoc = new Document(encryptedPdfPath, initialOwnerPassword))
            {
                // Re‑encrypt the document with the new passwords. The same permissions and algorithm are used as in step 1.
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                encryptedDoc.Encrypt(newUserPassword,
                                      newOwnerPassword,
                                      perms,
                                      CryptoAlgorithm.AESx256);

                // Save the PDF with updated passwords
                encryptedDoc.Save(reencryptedPdfPath);
            }

            Console.WriteLine($"PDF with updated passwords saved to '{reencryptedPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Password change failed: {ex.Message}");
        }
    }
}
