using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Existing owner password and the new passwords to set
        const string oldOwnerPassword = "oldOwner";
        const string newUserPassword  = "newUser";
        const string newOwnerPassword = "newOwner";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the PdfFileSecurity facade with input and output files
            PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

            // Change both user and owner passwords in a single call
            // This overload keeps the original security settings.
            fileSecurity.ChangePassword(oldOwnerPassword, newUserPassword, newOwnerPassword);

            Console.WriteLine("Password change completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during password change: {ex.Message}");
        }
    }
}