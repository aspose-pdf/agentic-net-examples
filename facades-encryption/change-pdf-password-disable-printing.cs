using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted_input.pdf";
        const string outputPath = "changed_password.pdf";
        const string currentUserPassword = "oldUser";
        const string currentOwnerPassword = "oldOwner";
        const string newUserPassword = "newUser";
        // Note: PdfFileSecurity.ChangePassword overload does not accept a new owner password.
        // The owner password will be set to the same value as the new user password.

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the encrypted PDF (no‑password overload)
        PdfFileSecurity security = new PdfFileSecurity();
        security.BindPdf(inputPath);

        // Use a predefined privilege that disables printing (and all other actions).
        // If you need finer control, start from DocumentPrivilege.AllowAll and modify the
        // properties, but the class does not expose a public parameter‑less constructor.
        DocumentPrivilege privilege = DocumentPrivilege.ForbidAll;

        // Change the user password, apply the privilege and request 256‑bit encryption.
        security.ChangePassword(
            currentUserPassword,
            currentOwnerPassword,
            newUserPassword,
            privilege,
            KeySize.x256);

        // Save the updated PDF
        security.Save(outputPath);

        Console.WriteLine($"Password changed and saved to '{outputPath}'.");
    }
}
