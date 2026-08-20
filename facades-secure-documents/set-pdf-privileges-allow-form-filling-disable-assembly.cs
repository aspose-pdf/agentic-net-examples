using System;
using System.IO;
using Aspose.Pdf.Facades;   // Contains DocumentPrivilege and PdfFileSecurity

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Start with all privileges allowed, then customize.
        DocumentPrivilege privilege = DocumentPrivilege.AllowAll;

        // Disable document assembly (inserting, deleting, rotating pages, etc.).
        privilege.AllowAssembly = false;

        // Ensure form filling is allowed (true by default in AllowAll, but set explicitly for clarity).
        privilege.AllowFillIn = true;

        // Apply the privilege settings and write the result to the output file.
        PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);
        fileSecurity.SetPrivilege(privilege);

        Console.WriteLine($"Privileges applied. Output saved to '{outputPath}'.");
    }
}