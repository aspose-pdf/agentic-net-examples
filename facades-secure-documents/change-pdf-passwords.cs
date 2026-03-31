using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string currentOwnerPassword = "owner123";
        const string newUserPassword = "newUser456";
        const string newOwnerPassword = "newOwner789";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath))
            {
                bool result = fileSecurity.ChangePassword(currentOwnerPassword, newUserPassword, newOwnerPassword);
                Console.WriteLine(result ? "Password changed successfully." : "Password change failed.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
