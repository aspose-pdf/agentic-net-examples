using System;
using System.IO;
using Aspose.Pdf;
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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (PdfFileSecurity fileSecurity = new PdfFileSecurity())
        {
            fileSecurity.BindPdf(inputPath);
            bool success = fileSecurity.SetPrivilege(userPassword, ownerPassword, DocumentPrivilege.Print);
            if (!success)
            {
                Console.Error.WriteLine("Failed to set privilege on the PDF.");
                return;
            }
            fileSecurity.Save(outputPath);
        }

        Console.WriteLine($"Password protected PDF saved to '{outputPath}'.");
    }
}
