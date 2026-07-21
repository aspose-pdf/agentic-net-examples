using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Added for DefaultAppearance

class Program
{
    static void Main()
    {
        const string outputPath = "PasswordProtectedForm.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // -----------------------------------------------------------------
        // Step 1: Create a PDF with a text box field (used as a password field)
        // -----------------------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Define the field rectangle
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Use TextBoxField – PasswordBoxField has no public constructor
            TextBoxField pwdField = new TextBoxField(page, fieldRect)
            {
                Name = "UserPassword",
                PartialName = "UserPassword",
                AlternateName = "Enter password",
                ReadOnly = false,
                // Set default appearance (font, size, color)
                DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black)
            };

            // Add the field to the document's form collection
            doc.Form.Add(pwdField);

            // Save the intermediate PDF (unencrypted) to a temporary file
            const string tempPath = "temp_unencrypted.pdf";
            doc.Save(tempPath);
        }

        // -----------------------------------------------------------------
        // Step 2: Encrypt the PDF so that opening/editing requires a password
        // -----------------------------------------------------------------
        using (Document encryptedDoc = new Document("temp_unencrypted.pdf"))
        {
            // Allow printing and filling form fields after authentication
            Permissions perms = Permissions.PrintDocument | Permissions.FillForm;

            // Encrypt using AES‑256
            encryptedDoc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the final password‑protected PDF
            encryptedDoc.Save(outputPath);
        }

        // Clean up the temporary file
        if (File.Exists("temp_unencrypted.pdf"))
        {
            File.Delete("temp_unencrypted.pdf");
        }

        Console.WriteLine($"PDF with a password‑protected field saved to '{outputPath}'.");
    }
}
