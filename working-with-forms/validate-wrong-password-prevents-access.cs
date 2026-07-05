using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string plainPdf = "plain.pdf";
        const string encryptedPdf = "protected.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";
        const string wrongPassword = "wrongpass";

        // -----------------------------------------------------------------
        // 1. Create a simple PDF with a password‑masked form field.
        // -----------------------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a page.
            Page page = doc.Pages.Add();

            // Create a text box field that will act as a password field.
            // PasswordBoxField has no public constructor, so we use TextBoxField.
            TextBoxField pwdField = new TextBoxField(page, new Rectangle(100, 600, 300, 630))
            {
                PartialName = "PwdField",
                Value = "SecretValue"
            };

            // Register the field with the document's form collection (not directly with page annotations).
            doc.Form.Add(pwdField);

            // Save the unencrypted PDF (optional, for inspection).
            doc.Save(plainPdf);
        }

        // -----------------------------------------------------------------
        // 2. Encrypt the PDF with a user and owner password.
        // -----------------------------------------------------------------
        using (Document doc = new Document(plainPdf))
        {
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
            doc.Save(encryptedPdf);
        }

        // -----------------------------------------------------------------
        // 3. Attempt to open the encrypted PDF with an incorrect password.
        //    Expect InvalidPasswordException.
        // -----------------------------------------------------------------
        try
        {
            // This line throws InvalidPasswordException because the password is wrong.
            using (Document protectedDoc = new Document(encryptedPdf, wrongPassword))
            {
                // If, for any reason, the document opens, try to read the field value.
                // This should never be reached with a wrong password.
                var field = (TextBoxField)protectedDoc.Form["PwdField"];
                Console.WriteLine($"Field value: {field.Value}");
            }
        }
        catch (InvalidPasswordException)
        {
            // Correct behavior: access is denied.
            Console.WriteLine("Access denied: incorrect password prevents reading the protected field.");
        }
        catch (Exception ex)
        {
            // Any other exception is unexpected.
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
