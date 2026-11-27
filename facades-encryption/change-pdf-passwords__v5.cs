using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath   = "encrypted_input.pdf";
        const string outputPath  = "reprotected_output.pdf";
        const string oldUserPwd  = "oldUserPassword";
        const string newUserPwd  = "newUserPassword";
        const string newOwnerPwd = "newOwnerPassword";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfFileSecurity does not implement IDisposable, so no using block is required.
        PdfFileSecurity security = new PdfFileSecurity();

        // Bind the encrypted PDF. The BindPdf method accepts only the file path.
        security.BindPdf(inputPath);

        // Change the passwords. ChangePassword expects three arguments:
        //   1. The current (old) user password.
        //   2. The new user password.
        //   3. The new owner password.
        // No encryption algorithm or key size is supplied, so the default settings are kept.
        security.ChangePassword(oldUserPwd, newUserPwd, newOwnerPwd);

        // Save the PDF with the new passwords.
        security.Save(outputPath);

        Console.WriteLine($"Passwords updated and saved to '{outputPath}'.");
    }
}
