using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        // Paths for the original, encrypted, and decrypted PDFs
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted.pdf";
        const string decryptedPath = "decrypted.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the original PDF document
            Document doc = new Document(inputPath);

            // Define passwords
            const string userPassword = "user123";
            const string ownerPassword = "owner123";

            // Encrypt the document with user/owner passwords and desired permissions
            doc.Encrypt(userPassword, ownerPassword,
                Permissions.PrintDocument | Permissions.ModifyContent,
                CryptoAlgorithm.RC4x128);

            // Save the encrypted PDF
            doc.Save(encryptedPath);
            Console.WriteLine($"Encrypted PDF saved to: {encryptedPath}");

            // Load the encrypted PDF using the user password
            Document encryptedDoc = new Document(encryptedPath, userPassword);

            // Remove encryption (decrypt)
            encryptedDoc.Decrypt();

            // Save the decrypted PDF
            encryptedDoc.Save(decryptedPath);
            Console.WriteLine($"Decrypted PDF saved to: {decryptedPath}");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}