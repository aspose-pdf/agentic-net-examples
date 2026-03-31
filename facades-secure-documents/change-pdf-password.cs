using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted_input.pdf";
        const string outputPath = "encrypted_output.pdf";
        const string ownerPassword = "owner123";
        const string newUserPassword = "newuser456";
        const string newOwnerPassword = "newowner789";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath))
        {
            bool success = fileSecurity.ChangePassword(ownerPassword, newUserPassword, newOwnerPassword);
            if (success)
            {
                Console.WriteLine("User password changed successfully while preserving encryption settings.");
            }
            else
            {
                Console.WriteLine("Failed to change the password.");
            }
        }
    }
}