using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string UserPassword = "user123";
        const string OwnerPassword = "owner123";
        const string WrongPassword = "wrongOwner";

        // Create a simple one‑page PDF to work with
        string originalPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save(originalPdfPath);
        }

        // Encrypt the PDF using the correct passwords
        string encryptedPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
        using (Document doc = new Document(originalPdfPath))
        {
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(UserPassword, OwnerPassword, perms, CryptoAlgorithm.AESx256);
            doc.Save(encryptedPdfPath);
        }

        // Verify that opening with an incorrect password throws InvalidPasswordException
        bool threw = false;
        try
        {
            using (Document doc = new Document(encryptedPdfPath, WrongPassword))
            {
                // If the document opens, attempt to decrypt (should not be reached)
                doc.Decrypt();
            }
        }
        catch (InvalidPasswordException)
        {
            threw = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAIL unexpected exception: {ex.GetType().Name} - {ex.Message}");
        }

        Console.WriteLine(threw ?
            "PASS Decrypt_WithIncorrectOwnerPassword_ShouldThrowInvalidPasswordException" :
            "FAIL Decrypt_WithIncorrectOwnerPassword_ShouldThrowInvalidPasswordException");

        // Clean up temporary files
        try { if (File.Exists(originalPdfPath)) File.Delete(originalPdfPath); } catch { }
        try { if (File.Exists(encryptedPdfPath)) File.Delete(encryptedPdfPath); } catch { }
    }
}