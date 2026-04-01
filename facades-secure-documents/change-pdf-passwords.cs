using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";
        string currentOwnerPassword = "oldOwner";
        string newUserPassword = "newUser";
        string newOwnerPassword = "newOwner";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        PdfFileSecurity security = new PdfFileSecurity();
        security.BindPdf(inputPath);
        // Change both user and owner passwords in a single call
        bool result = security.ChangePassword(currentOwnerPassword, newUserPassword, newOwnerPassword);
        if (!result)
        {
            Console.Error.WriteLine("Failed to change passwords.");
            return;
        }
        security.Save(outputPath);
        Console.WriteLine($"Passwords changed successfully. Saved to '{outputPath}'.");
    }
}