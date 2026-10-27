using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

// Helper class that logs encryption and decryption actions
static class PdfSecurityLogger
{
    public static void EncryptWithLogging(Document doc, string userPassword, string ownerPassword,
                                          Permissions permissions, CryptoAlgorithm algorithm)
    {
        Console.WriteLine("[LOG] Encrypting document:");
        Console.WriteLine($"      User password:   {userPassword}");
        Console.WriteLine($"      Owner password:  {ownerPassword}");
        Console.WriteLine($"      Permissions:     {permissions}");
        Console.WriteLine($"      Algorithm:       {algorithm}");
        // Perform the actual encryption
        doc.Encrypt(userPassword, ownerPassword, permissions, algorithm);
    }

    public static void DecryptWithLogging(Document doc, string userPassword)
    {
        Console.WriteLine($"[LOG] Decrypt attempt with password: {userPassword}");
        // Perform the actual decryption (no parameters required after opening with password)
        doc.Decrypt();
    }
}

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";
        const string encryptedPdf  = "encrypted.pdf";
        const string decryptedPdf  = "decrypted.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // ---------- Encrypt the PDF with logging ----------
        try
        {
            using (Document doc = new Document(inputPdf))
            {
                // Define permissions (example: allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt using AES-256 while logging the operation
                PdfSecurityLogger.EncryptWithLogging(doc, userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(encryptedPdf);
            }

            Console.WriteLine($"Encrypted PDF saved to '{encryptedPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Encryption error: {ex.Message}");
            return;
        }

        // ---------- Decrypt (open) the encrypted PDF with logging ----------
        try
        {
            // Open the encrypted PDF providing the user password
            using (Document encDoc = new Document(encryptedPdf, userPassword))
            {
                // Log the decrypt attempt and perform decryption
                PdfSecurityLogger.DecryptWithLogging(encDoc, userPassword);

                // Save the decrypted version
                encDoc.Save(decryptedPdf);
            }

            Console.WriteLine($"Decrypted PDF saved to '{decryptedPdf}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Decryption error: {ex.Message}");
        }
    }
}
