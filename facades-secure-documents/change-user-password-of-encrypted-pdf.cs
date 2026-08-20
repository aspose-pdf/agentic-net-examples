using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted_input.pdf";
        const string outputPath = "encrypted_output.pdf";
        const string ownerPassword = "owner123";          // original owner password
        const string newUserPassword = "newUser456";     // desired new user password
        const string newOwnerPassword = null;            // null generates a random owner password

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfFileSecurity handles encryption settings internally; ChangePassword keeps them unchanged.
        using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
        {
            bool changed = security.ChangePassword(ownerPassword, newUserPassword, newOwnerPassword);
            if (!changed)
            {
                Console.Error.WriteLine("Failed to change the password.");
                return;
            }
        }

        Console.WriteLine($"User password updated successfully. Output saved to '{outputPath}'.");
    }
}