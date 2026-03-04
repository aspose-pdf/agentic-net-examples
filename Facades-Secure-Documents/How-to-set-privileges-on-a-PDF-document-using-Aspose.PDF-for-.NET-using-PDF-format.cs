using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileSecurity and DocumentPrivilege are defined here

class SetPdfPrivileges
{
    static void Main()
    {
        // Input PDF file (must exist)
        const string inputPath  = @"D:\input.pdf";
        // Output PDF file (will be created/overwritten)
        const string outputPath = @"D:\output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the PdfFileSecurity facade with source and destination files
            PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath);

            // Example: allow only printing (all other operations are forbidden)
            // DocumentPrivilege provides several predefined privileges (Print, AllowAll, ForbidAll, etc.)
            // You can also customize a privilege instance before passing it.
            DocumentPrivilege privilege = DocumentPrivilege.Print;

            // Apply the privilege settings.
            // This method creates the output file with the specified security settings.
            security.SetPrivilege(privilege);

            Console.WriteLine($"Privileges applied successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while setting privileges: {ex.Message}");
        }
    }
}