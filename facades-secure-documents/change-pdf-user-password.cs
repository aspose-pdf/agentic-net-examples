using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string outputPath = "updated.pdf";
        const string ownerPassword = "owner123";
        const string newUserPassword = "newuser456";
        const string newOwnerPassword = "newowner789";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        try
        {
            PdfFileSecurity fileSecurity = new PdfFileSecurity();
            fileSecurity.BindPdf(inputPath);
            bool changed = fileSecurity.ChangePassword(ownerPassword, newUserPassword, newOwnerPassword);
            if (!changed)
            {
                Console.Error.WriteLine("Failed to change password.");
                return;
            }
            fileSecurity.Save(outputPath);
            Console.WriteLine("Password changed successfully. Output saved to '" + outputPath + "'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error: " + ex.Message);
        }
    }
}
