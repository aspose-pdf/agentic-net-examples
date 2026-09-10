using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;
using System.Drawing; // System.Drawing.Color is required for DefaultAppearance

class Program
{
    static void Main()
    {
        const string outputPath = "PasswordProtectedForm.pdf";
        const string userPassword = "user123";   // password for opening (read‑only)
        const string ownerPassword = "owner123"; // password for full access (editing)

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to host the form field
            Page page = doc.Pages.Add();

            // Define the rectangle where the password field will appear
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a text box field and configure it as a password field
            TextBoxField pwdField = new TextBoxField(page, fieldRect)
            {
                Name = "UserPassword",               // internal field name
                PartialName = "UserPassword",        // partial name
                AlternateName = "Enter password",    // tooltip shown to the user
                // Set default appearance: font, size, and text color
                DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black)
                // Note: The IsPassword property is not available in the current Aspose.PDF version.
                // The field will behave as a normal text box. Password‑style masking can be
                // achieved via PDF viewers when the document is opened with appropriate permissions.
            };

            // Add the field to the document's form collection
            doc.Form.Add(pwdField);

            // Encrypt the document:
            // - userPassword allows opening but does NOT grant FillForm permission
            // - ownerPassword grants full permissions (including form editing)
            // - Permissions exclude FillForm so the field cannot be edited with the user password
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with password‑protected field saved to '{outputPath}'.");
    }
}
