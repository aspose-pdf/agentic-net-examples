using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath   = "PasswordProtectedForm.pdf";
        const string userPassword = "user123";   // password required to open the PDF
        const string ownerPassword = "owner123"; // password that grants full edit rights

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle for the form field (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a text box field that will be used as a password input field
            TextBoxField passwordField = new TextBoxField(page, fieldRect);
            passwordField.PartialName = "PasswordField"; // field name
            passwordField.MaxLen = 20;                    // maximum characters

            // Add the field to the document's form collection
            doc.Form.Add(passwordField);

            // Set permissions so that a user who opens the PDF with the user password
            // can view/print but cannot modify the document.
            // The owner password will allow full editing.
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with password protection saved to '{outputPath}'.");
    }
}