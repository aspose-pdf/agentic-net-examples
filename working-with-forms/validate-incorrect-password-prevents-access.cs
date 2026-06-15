using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string tempPdf = "temp.pdf";
        const string encryptedPdf = "encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";
        const string wrongPassword = "wrong";

        // Create a PDF with a text box field that will hold a secret value
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the field rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // Create a TextBoxField (used as a password field)
            TextBoxField pwdField = new TextBoxField(page, rect);
            pwdField.PartialName = "pwdField"; // set the field name
            pwdField.Value = "SecretValue";    // secret content

            // Optional: visual masking can be added via JavaScript; Aspose.Pdf does not expose a direct mask property.
            doc.Form.Add(pwdField);
            doc.Save(tempPdf);
        }

        // Encrypt the PDF with a user and owner password
        using (Document doc = new Document(tempPdf))
        {
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
            doc.Save(encryptedPdf);
        }

        // Attempt to open the encrypted PDF with an incorrect password
        try
        {
            using (Document doc = new Document(encryptedPdf, wrongPassword))
            {
                // If the password were correct, we could read the field value
                TextBoxField field = (TextBoxField)doc.Form["pwdField"];
                Console.WriteLine("Field value: " + field.Value);
            }
        }
        catch (InvalidPasswordException)
        {
            // Expected outcome: access is denied due to wrong password
            Console.WriteLine("Access denied: incorrect password.");
        }
        finally
        {
            // Clean up the temporary unencrypted file
            if (File.Exists(tempPdf))
                File.Delete(tempPdf);
        }
    }
}
