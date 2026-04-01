using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "protected.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            PdfFileSecurity fileSecurity = new PdfFileSecurity();
            fileSecurity.BindPdf(inputPath);

            DocumentPrivilege privilege = DocumentPrivilege.AllowAll;
            privilege.AllowAssembly = false;
            privilege.AllowFillIn = true;

            fileSecurity.SetPrivilege(userPassword, ownerPassword, privilege);
            fileSecurity.Save(outputPath);

            Console.WriteLine($"PDF saved with updated privileges to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
