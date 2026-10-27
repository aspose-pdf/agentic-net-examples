using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string originalPath   = "original.pdf";
        const string encryptedPath  = "protected.pdf";
        const string correctUserPwd = "user123";
        const string correctOwnerPwd = "owner123";
        const string wrongUserPwd   = "wrongPwd";

        // ------------------------------------------------------------
        // 1. Create a simple PDF with some text
        // ------------------------------------------------------------
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            TextFragment tf = new TextFragment("Secret content that should be protected.");
            page.Paragraphs.Add(tf);

            // --------------------------------------------------------
            // 2. Encrypt the document using AES-256
            // --------------------------------------------------------
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(correctUserPwd, correctOwnerPwd, perms, CryptoAlgorithm.AESx256);
            doc.Save(encryptedPath);
        }

        // ------------------------------------------------------------
        // 3. Attempt to open the encrypted PDF with an incorrect password
        // ------------------------------------------------------------
        try
        {
            // This will throw InvalidPasswordException because the password is wrong
            using (Document wrongDoc = new Document(encryptedPath, wrongUserPwd))
            {
                // If no exception, attempt to read (should not happen)
                TextAbsorber absorber = new TextAbsorber();
                wrongDoc.Pages.Accept(absorber);
                Console.WriteLine("Unexpectedly accessed content: " + absorber.Text);
            }
        }
        catch (InvalidPasswordException)
        {
            Console.WriteLine("Access denied: incorrect password prevented opening the document.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error: " + ex.Message);
        }

        // ------------------------------------------------------------
        // 4. Open the encrypted PDF with the correct password and verify access
        // ------------------------------------------------------------
        try
        {
            using (Document correctDoc = new Document(encryptedPath, correctUserPwd))
            {
                // Decrypt the document (optional, Decrypt() takes no parameters)
                correctDoc.Decrypt();

                // Extract text to confirm successful access
                TextAbsorber absorber = new TextAbsorber();
                correctDoc.Pages.Accept(absorber);
                Console.WriteLine("Access granted: extracted text -> " + absorber.Text);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to open with correct password: " + ex.Message);
        }
    }
}