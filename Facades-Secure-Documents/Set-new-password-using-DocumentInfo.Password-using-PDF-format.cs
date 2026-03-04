using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the destination PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Existing owner password of the source PDF
        const string currentOwnerPassword = "oldOwner";

        // New passwords to be set
        const string newUserPassword  = "newUser";
        const string newOwnerPassword = "newOwner";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileSecurity facade with input and output files
        PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

        // Change the password while preserving the original security settings
        bool changed = fileSecurity.ChangePassword(currentOwnerPassword, newUserPassword, newOwnerPassword);

        if (changed)
            Console.WriteLine("Password successfully changed.");
        else
            Console.Error.WriteLine("Failed to change password.");
    }
}