using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "PasswordProtectedForm.pdf";

        // Define passwords
        const string userPassword  = "user123";   // password required to open the PDF
        const string ownerPassword = "owner123";  // full‑access password

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle where the password field will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a TextBoxField (used for password entry) on the page
            TextBoxField pwdField = new TextBoxField(page, fieldRect)
            {
                Name          = "UserPassword",          // internal field name
                PartialName   = "UserPassword",          // displayed name (tooltip)
                AlternateName = "Enter password",        // tooltip shown in Acrobat
                MaxLen        = 20,                       // maximum characters
                Required      = true,                     // make it a required field
                ReadOnly      = false                     // user can type into it
            };

            // If the library version supports it, enable password masking
            // (some versions expose the IsPassword property). Guard with a compile‑time check.
#if NET6_0_OR_GREATER
            if (pwdField.GetType().GetProperty("IsPassword") != null)
                pwdField.GetType().GetProperty("IsPassword")?.SetValue(pwdField, true);
#endif

            // Add the field to the document's form collection
            doc.Form.Add(pwdField);

            // Optional: set default appearance for all form fields (font, size, color)
            doc.Form.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Encrypt the document so that it can be opened only with the user password.
            // Permissions allow filling form fields after opening with the user password.
            Permissions perms = Permissions.FillForm;
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with password field saved to '{outputPath}'.");
    }
}
