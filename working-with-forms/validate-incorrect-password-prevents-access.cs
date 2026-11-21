using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string originalPath = "original.pdf";
        const string encryptedPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";
        const string wrongPassword = "wrong";

        // -----------------------------------------------------------------
        // 1. Create a simple PDF containing some secret text.
        // -----------------------------------------------------------------
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            TextFragment tf = new TextFragment("Secret data inside PDF.");
            page.Paragraphs.Add(tf);
            doc.Save(originalPath);
        }

        // -----------------------------------------------------------------
        // 2. Encrypt the PDF with a user password.
        //    Use CryptoAlgorithm.AESx256 as required by the encryption rule.
        // -----------------------------------------------------------------
        using (Document doc = new Document(originalPath))
        {
            Permissions perms = Permissions.PrintDocument; // minimal permissions
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
            doc.Save(encryptedPath);
        }

        // -----------------------------------------------------------------
        // 3. Attempt to open the encrypted PDF with an incorrect password.
        //    Expect InvalidPasswordException – this proves access is blocked.
        // -----------------------------------------------------------------
        try
        {
            using (Document doc = new Document(encryptedPath, wrongPassword))
            {
                // If the document opens, try to read its content (should not happen).
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages.Accept(absorber);
                Console.WriteLine("Unexpectedly accessed content: " + absorber.Text);
            }
        }
        catch (InvalidPasswordException)
        {
            Console.WriteLine("Access denied with wrong password as expected.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error: " + ex.Message);
        }

        // -----------------------------------------------------------------
        // 4. Open the encrypted PDF with the correct password and read the content.
        // -----------------------------------------------------------------
        using (Document doc = new Document(encryptedPath, userPassword))
        {
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages.Accept(absorber);
            Console.WriteLine("Access granted. Content: " + absorber.Text);
        }
    }
}