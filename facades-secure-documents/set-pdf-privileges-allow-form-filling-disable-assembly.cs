using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileSecurity, DocumentPrivilege

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

        // Start from a predefined privilege that allows everything,
        // then adjust the required settings.
        DocumentPrivilege privilege = DocumentPrivilege.AllowAll;
        privilege.AllowAssembly = false;   // disable document assembly (insert/delete/rotate pages, etc.)
        privilege.AllowFillIn   = true;    // enable filling of form fields

        // Apply the privilege settings and write the protected PDF.
        // PdfFileSecurity works directly with file paths.
        PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);
        bool success = fileSecurity.SetPrivilege(privilege);

        if (success)
            Console.WriteLine($"Privileges applied successfully. Output saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to set document privileges.");
    }
}