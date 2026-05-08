using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source and the resulting PDF
        const string inputPath = "protected.pdf";
        const string outputPath = "updated_owner.pdf";

        // Current owner password and the new owner password to set
        const string currentOwnerPassword = "oldOwnerPassword";
        const string newOwnerPassword = "newOwnerPassword";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileSecurity facade
        PdfFileSecurity security = new PdfFileSecurity();

        // Bind the source PDF file
        security.BindPdf(inputPath);

        // Change only the owner password.
        // Pass null for newUserPassword to keep the existing user password unchanged.
        // Privileges are preserved automatically.
        bool changed = security.ChangePassword(currentOwnerPassword, null, newOwnerPassword);
        if (!changed)
        {
            Console.Error.WriteLine("Failed to change the owner password.");
            return;
        }

        // Save the updated PDF to the output path
        security.Save(outputPath);

        // Release resources
        security.Close();

        Console.WriteLine($"Owner password updated successfully. Output saved to '{outputPath}'.");
    }
}