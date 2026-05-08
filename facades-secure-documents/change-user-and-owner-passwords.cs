using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source and destination PDF files
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Existing owner password and the new passwords to set
        const string currentOwnerPassword = "owner123";
        const string newUserPassword      = "newUser123";
        const string newOwnerPassword     = "newOwner123";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileSecurity facade with input and output files
        PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

        // Change both user and owner passwords in a single call
        // Overload: ChangePassword(string ownerPassword, string newUserPassword, string newOwnerPassword)
        fileSecurity.ChangePassword(currentOwnerPassword, newUserPassword, newOwnerPassword);

        // Persist the changes to the output PDF
        fileSecurity.Save(outputPath);

        Console.WriteLine($"Passwords updated successfully. Output saved to '{outputPath}'.");
    }
}