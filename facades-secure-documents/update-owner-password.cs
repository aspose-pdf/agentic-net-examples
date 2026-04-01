using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "protected.pdf";
        const string outputPath = "updated_owner.pdf";
        const string ownerPassword = "oldOwner";
        const string newOwnerPassword = "newOwner";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        PdfFileSecurity security = new PdfFileSecurity();
        security.BindPdf(inputPath);
        // Pass null for newUserPassword to keep the existing user password unchanged
        security.ChangePassword(ownerPassword, null, newOwnerPassword);
        security.Save(outputPath);

        Console.WriteLine($"Owner password updated and saved to '{outputPath}'.");
    }
}
