using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath = "protected.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";
        const string wrongPassword = "wrongPass";

        // -----------------------------------------------------------------
        // 1. Create a PDF with a password‑protected form field and encrypt it
        // -----------------------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Define a rectangle for the password box field
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // Create the password box field using TextBoxField (PasswordBoxField has no public ctor)
            TextBoxField pwdField = new TextBoxField(page, rect)
            {
                PartialName = "SecretPwd",
                Value = "SecretValue" // initial value (will be encrypted)
            };
            doc.Form.Add(pwdField);

            // Encrypt the document (user password required to open)
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF
            doc.Save(pdfPath);
        }

        // -----------------------------------------------------------------
        // 2. Attempt to open the PDF with an incorrect password
        // -----------------------------------------------------------------
        try
        {
            using (Document wrongDoc = new Document(pdfPath, wrongPassword))
            {
                // If the password were accepted, we could try to read the field.
                // This line should never be reached.
                Console.WriteLine("Unexpectedly opened with wrong password.");
            }
        }
        catch (InvalidPasswordException)
        {
            Console.WriteLine("Access denied: incorrect password prevents opening the document.");
        }

        // -----------------------------------------------------------------
        // 3. Open the PDF with the correct password and read the field value
        // -----------------------------------------------------------------
        try
        {
            using (Document correctDoc = new Document(pdfPath, userPassword))
            {
                // Retrieve the password box field by its partial name
                if (correctDoc.Form["SecretPwd"] is TextBoxField field)
                {
                    Console.WriteLine($"Field '{field.PartialName}' value: {field.Value}");
                }
                else
                {
                    Console.WriteLine("PasswordBoxField not found.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
