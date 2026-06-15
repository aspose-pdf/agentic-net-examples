using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Step 1: Create a simple PDF file.
        string samplePath = "sample.pdf";
        using (Document sampleDoc = new Document())
        {
            // Add a single blank page (1‑based indexing).
            sampleDoc.Pages.Add();
            sampleDoc.Save(samplePath);
        }

        // Step 2: Encrypt the PDF with an owner password.
        string encryptedPath = "encrypted.pdf";
        string ownerPassword = "ownerpass";
        string userPassword = ""; // No user password.
        using (Document docToEncrypt = new Document(samplePath))
        {
            Permissions perms = Permissions.PrintDocument;
            docToEncrypt.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
            docToEncrypt.Save(encryptedPath);
        }

        // Step 3: Attempt to decrypt using an incorrect owner password.
        string wrongPassword = "wrongpass";
        // The second argument is the output file for the decrypted PDF.
        PdfFileSecurity fileSecurity = new PdfFileSecurity(encryptedPath, "decrypted.pdf");
        bool decryptionResult = fileSecurity.TryDecryptFile(wrongPassword);

        // Step 4: Verify that decryption failed.
        Console.WriteLine("Decryption with incorrect password succeeded: " + decryptionResult);
    }
}