using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a privilege object: allow form filling, disallow document assembly
        DocumentPrivilege privilege = DocumentPrivilege.ForbidAll;
        privilege.AllowFillIn = true;      // enable form filling
        privilege.AllowAssembly = false;   // disable assembly (insert/delete/rotate pages)

        // Apply the privilege settings using PdfFileSecurity
        PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath);
        bool success = security.SetPrivilege(privilege);

        if (!success)
        {
            Console.Error.WriteLine("Failed to set document privileges.");
        }
        else
        {
            Console.WriteLine($"Privileges applied successfully. Output saved to '{outputPath}'.");
        }
    }
}