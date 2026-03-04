using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source and destination PDF files
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Existing owner password and the new passwords to set
        const string oldOwnerPassword = "owner123";
        const string newUserPassword = "newUser";
        const string newOwnerPassword = "newOwner";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // === Create ===
        // Instantiate the PdfFileSecurity facade
        PdfFileSecurity security = new PdfFileSecurity();

        // === Load ===
        // Bind the source PDF file to the facade
        security.BindPdf(inputPath);

        // === Change Password ===
        // Change the user and owner passwords while preserving existing privileges
        bool success = security.ChangePassword(oldOwnerPassword, newUserPassword, newOwnerPassword);
        if (!success)
        {
            Console.Error.WriteLine("Password change operation failed.");
            return;
        }

        // === Save ===
        // Save the PDF with the new passwords to the output path
        security.Save(outputPath);

        Console.WriteLine($"Password successfully changed. Output saved to '{outputPath}'.");
    }
}