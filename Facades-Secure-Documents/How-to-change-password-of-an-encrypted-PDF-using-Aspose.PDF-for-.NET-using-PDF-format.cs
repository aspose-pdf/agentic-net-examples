using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source (encrypted) PDF and the destination PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Existing owner password and the new passwords you want to set
        const string oldOwnerPassword = "oldOwnerPassword";
        const string newUserPassword  = "newUserPassword";
        const string newOwnerPassword = "newOwnerPassword";

        // Verify that the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileSecurity works directly on files; it does not require loading a Document object.
        // The constructor takes the source file and the target file.
        using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
        {
            // ChangePassword keeps the original security settings (privileges) and updates the passwords.
            bool changed = security.ChangePassword(oldOwnerPassword, newUserPassword, newOwnerPassword);

            if (changed)
                Console.WriteLine($"Password changed successfully. Output saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Failed to change the password.");
        }
    }
}