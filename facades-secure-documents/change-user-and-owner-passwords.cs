using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string currentOwnerPassword = "oldOwner";
        const string newUserPassword = "newUser";
        const string newOwnerPassword = "newOwner";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Create the PdfFileSecurity facade with source and destination files
            PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

            // Change both user and owner passwords in a single call
            bool changed = fileSecurity.ChangePassword(currentOwnerPassword, newUserPassword, newOwnerPassword);

            Console.WriteLine(changed ? "Passwords changed successfully." : "Password change failed.");

            // Release resources held by the facade
            fileSecurity.Close();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}