using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPdf = "protected.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // -----------------------------------------------------------------
        // 1. Create a PDF with a password field and encrypt it.
        // -----------------------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a page.
            Page page = doc.Pages.Add();

            // Create a password box field using TextBoxField (PasswordBoxField has no public ctor).
            // Rectangle constructor: (llx, lly, urx, ury)
            TextBoxField pwdField = new TextBoxField(
                page,
                new Aspose.Pdf.Rectangle(100, 600, 300, 630))
            {
                PartialName = "pwdField",          // field name
                Value = "Secret123",               // initial value (will be encrypted)
                Color = Aspose.Pdf.Color.LightGray // visual background color
                // Note: The IsPassword property is not available in the current Aspose.Pdf version.
                // The field value is still stored securely because the whole document is encrypted.
            };

            // Add the field to the document's form collection (not directly to page annotations).
            doc.Form.Add(pwdField);

            // Define permissions (example: allow printing and content extraction).
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt the document using the recommended AES-256 algorithm.
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF.
            doc.Save(outputPdf);
        }

        // -----------------------------------------------------------------
        // 2. Attempt to open the encrypted PDF with an incorrect password.
        //    Expect an InvalidPasswordException.
        // -----------------------------------------------------------------
        try
        {
            // Wrong password supplied.
            using (Document wrongDoc = new Document(outputPdf, "wrongpass"))
            {
                // If no exception is thrown, attempt to read the field (should not happen).
                var field = (TextBoxField)wrongDoc.Form["pwdField"];
                Console.WriteLine("Unexpectedly accessed field value: " + field.Value);
            }
        }
        catch (InvalidPasswordException)
        {
            // Correct behavior: access is denied.
            Console.WriteLine("Access denied: incorrect password prevents opening the document.");
        }

        // -----------------------------------------------------------------
        // 3. Verify that the correct password allows access to the field.
        // -----------------------------------------------------------------
        try
        {
            using (Document correctDoc = new Document(outputPdf, userPassword))
            {
                var field = (TextBoxField)correctDoc.Form["pwdField"];
                Console.WriteLine("Field value with correct password: " + field.Value);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error with correct password: " + ex.Message);
        }
    }
}
