using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Existing owner password of the PDF
        const string currentOwnerPassword = "oldOwner";

        // New passwords to apply
        const string newUserPassword = "newUser";
        const string newOwnerPassword = "newOwner";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the facade with source and destination files
            PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

            // Change the password while keeping the original permissions
            bool success = fileSecurity.ChangePassword(currentOwnerPassword, newUserPassword, newOwnerPassword);
            if (!success)
            {
                Console.Error.WriteLine("Failed to change password.");
                return;
            }

            // Example of resetting permissions (optional):
            // bool successWithPriv = fileSecurity.ChangePassword(
            //     currentOwnerPassword,
            //     newUserPassword,
            //     newOwnerPassword,
            //     DocumentPrivilege.Print,   // allow printing only
            //     KeySize.x256);             // 256‑bit encryption
            // if (!successWithPriv) { /* handle failure */ }

            Console.WriteLine($"Password updated successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}