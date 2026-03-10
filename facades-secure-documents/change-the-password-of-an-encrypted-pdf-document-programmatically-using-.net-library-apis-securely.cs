using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source (encrypted) PDF and the destination PDF
        const string inputPath  = @"C:\Docs\encrypted_input.pdf";
        const string outputPath = @"C:\Docs\changed_password_output.pdf";

        // Original owner password and the new passwords to set
        const string originalOwnerPassword = "oldOwnerPass";
        const string newUserPassword       = "newUserPass";
        const string newOwnerPassword      = "newOwnerPass";

        // Verify that the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Create the PdfFileSecurity facade with input and output file names
            PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath);

            // Change the passwords while preserving the existing security settings
            bool success = security.ChangePassword(originalOwnerPassword, newUserPassword, newOwnerPassword);

            if (success)
                Console.WriteLine($"Password changed successfully. Output saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Password change failed (method returned false).");
        }
        catch (Exception ex)
        {
            // Any exception thrown by the facade (e.g., wrong password, I/O errors) is caught here
            Console.Error.WriteLine($"Error during password change: {ex.Message}");
        }
    }
}