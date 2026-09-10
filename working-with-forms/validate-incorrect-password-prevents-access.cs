using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath   = "protected.pdf";
        const string userPwd   = "user123";
        const string ownerPwd  = "owner123";
        const string wrongPwd  = "wrongpwd";

        // -----------------------------------------------------------------
        // Create a PDF with a password box field and encrypt it.
        // -----------------------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a page.
            Page page = doc.Pages.Add();

            // Create a text box field that will act as a password field (partial name "pwdField").
            TextBoxField pwdField = new TextBoxField(
                page,
                new Aspose.Pdf.Rectangle(100, 600, 300, 650))
            {
                PartialName = "pwdField",
                // The value is stored in the PDF; in a real UI it would be masked.
                Value = "SecretValue"
            };

            // Add the field to the document's form.
            doc.Form.Add(pwdField);

            // Encrypt the document using AES-256.
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(userPwd, ownerPwd, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF.
            doc.Save(pdfPath);
        }

        // -----------------------------------------------------------------
        // Attempt to open the encrypted PDF with an incorrect password.
        // Expect InvalidPasswordException.
        // -----------------------------------------------------------------
        try
        {
            using (Document wrongDoc = new Document(pdfPath, wrongPwd))
            {
                // If the document opens (it shouldn't), try to read the field.
                var field = (TextBoxField)wrongDoc.Form["pwdField"];
                Console.WriteLine("Unexpectedly accessed field value: " + field.Value);
            }
        }
        catch (InvalidPasswordException ex)
        {
            Console.WriteLine("Access denied with wrong password: " + ex.Message);
        }

        // -----------------------------------------------------------------
        // Open the PDF with the correct user password and read the field.
        // -----------------------------------------------------------------
        try
        {
            using (Document correctDoc = new Document(pdfPath, userPwd))
            {
                var field = (TextBoxField)correctDoc.Form["pwdField"];
                Console.WriteLine("Field value with correct password: " + field.Value);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error: " + ex.Message);
        }
    }
}
