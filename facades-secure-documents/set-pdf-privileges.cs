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

        // Initialize the security facade with input and output files
        PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

        // Start from a privilege that allows everything, then restrict copying
        DocumentPrivilege privilege = DocumentPrivilege.AllowAll;
        privilege.AllowCopy = false;   // Disallow copying
        privilege.AllowPrint = true;   // Ensure printing is allowed

        // Apply the privilege settings
        fileSecurity.SetPrivilege(privilege);

        Console.WriteLine($"Privileges applied: printing allowed, copying disallowed. Saved to '{outputPath}'.");
    }
}
