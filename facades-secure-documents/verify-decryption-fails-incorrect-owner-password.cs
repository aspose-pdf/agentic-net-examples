using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class DecryptionFailureTest
{
    static void Main()
    {
        // Prepare temporary file paths
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposePdfTest");
        Directory.CreateDirectory(tempDir);

        string plainPdf = Path.Combine(tempDir, "plain.pdf");
        string encryptedPdf = Path.Combine(tempDir, "encrypted.pdf");
        string decryptedPdf = Path.Combine(tempDir, "decrypted.pdf");

        // Create a simple PDF document (one blank page)
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save(plainPdf);
        }

        // Encrypt the PDF with a known owner password
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // PdfFileSecurity constructor expects input and output file paths
        PdfFileSecurity encryptor = new PdfFileSecurity(plainPdf, encryptedPdf);
        // Use AES-256 encryption (KeySize.x256) and allow printing as a privilege
        encryptor.EncryptFile(userPassword, ownerPassword, DocumentPrivilege.Print, KeySize.x256);
        encryptor.Close(); // Close the facade to release resources

        // Attempt to decrypt with an incorrect owner password using TryDecryptFile (should return false)
        const string wrongOwnerPassword = "wrongOwner";

        PdfFileSecurity decryptor = new PdfFileSecurity(encryptedPdf, decryptedPdf);
        bool decryptionResult = decryptor.TryDecryptFile(wrongOwnerPassword);
        decryptor.Close();

        // Verify that decryption failed
        if (!decryptionResult)
        {
            Console.WriteLine("Test Passed: Decryption failed as expected with an incorrect owner password.");
        }
        else
        {
            Console.WriteLine("Test Failed: Decryption succeeded unexpectedly with an incorrect owner password.");
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(plainPdf);
            File.Delete(encryptedPdf);
            File.Delete(decryptedPdf);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored – cleanup is best‑effort
        }
    }
}