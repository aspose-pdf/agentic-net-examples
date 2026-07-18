using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string ownerPassword = "owner123";
        const string newUserPassword = "newUser456";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the facade and bind the source PDF
        Aspose.Pdf.Facades.PdfFileSecurity fileSecurity = new Aspose.Pdf.Facades.PdfFileSecurity();
        fileSecurity.BindPdf(inputPath);

        // Change the user password while preserving the original owner password and encryption settings
        bool success = fileSecurity.ChangePassword(ownerPassword, newUserPassword, ownerPassword);
        if (!success)
        {
            Console.Error.WriteLine("Failed to change the password.");
            fileSecurity.Close();
            return;
        }

        // Save the updated PDF
        fileSecurity.Save(outputPath);
        fileSecurity.Close();

        Console.WriteLine($"User password changed successfully. Saved to '{outputPath}'.");
    }
}