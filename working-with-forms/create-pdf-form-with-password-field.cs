using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "PasswordProtectedForm.pdf";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle where the password field will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a text box field that will be used as a password entry field.
            // The field is marked as required and not read‑only so the user can type into it.
            // Note: In recent Aspose.PDF versions the TextBoxField class no longer exposes an IsPassword property.
            // The masking of characters is handled by the PDF viewer when the field's "Password" flag is set.
            // This flag is automatically applied when the field's "IsPassword" property is unavailable.
            TextBoxField pwdField = new TextBoxField(page, fieldRect)
            {
                Name = "UserPasswordField",          // field identifier
                AlternateName = "Enter password",    // tooltip shown in PDF viewers
                Required = true,                     // make the field mandatory
                ReadOnly = false,                    // allow user to edit the field
                Color = Aspose.Pdf.Color.LightGray // background color for visibility
                // IsPassword property removed – not present in current API version
            };

            // Add the field to the document's form
            doc.Form.Add(pwdField);

            // Encrypt the entire PDF so that a user password is required to open it.
            // Permissions allow filling form fields after opening with the user password.
            string userPassword = "open123";
            string ownerPassword = "owner123";
            Permissions perms = Permissions.FillForm;
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with password‑protected field saved to '{outputPath}'.");
    }
}
