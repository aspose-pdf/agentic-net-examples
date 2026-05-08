using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "protected.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";
        const string fieldName = "SecretField";

        // Create a PDF with a text box field (used as a password field) and encrypt it
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Define the field rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a text box field, set its name and value
            // PasswordBoxField has no public constructor, so we use TextBoxField instead
            TextBoxField pwdField = new TextBoxField(page, rect);
            pwdField.PartialName = fieldName;
            pwdField.Value = "SecretValue";

            // Add the field to the document's form collection (this also registers the widget on the page)
            doc.Form.Add(pwdField);

            // Encrypt the document using AES-256
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF
            doc.Save(pdfPath);
        }

        // Attempt to open the PDF with an incorrect password
        try
        {
            using (Document wrongDoc = new Document(pdfPath, "wrong"))
            {
                // If opening succeeds (it shouldn't), try to read the field value
                var field = (TextBoxField)wrongDoc.Form[fieldName];
                Console.WriteLine("Unexpected access: field value = " + field.Value);
            }
        }
        catch (InvalidPasswordException)
        {
            // Expected outcome: access is denied
            Console.WriteLine("Access denied: incorrect password prevented opening the document.");
        }

        // Open the PDF with the correct user password and read the field value
        try
        {
            using (Document correctDoc = new Document(pdfPath, userPassword))
            {
                var field = (TextBoxField)correctDoc.Form[fieldName];
                Console.WriteLine("Field value with correct password: " + field.Value);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error while opening with correct password: " + ex.Message);
        }
    }
}
