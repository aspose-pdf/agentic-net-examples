using System;
using System.IO;
using Aspose.Pdf.Facades;

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

        // Start from a privilege that allows everything, then adjust.
        DocumentPrivilege privilege = DocumentPrivilege.AllowAll;
        privilege.AllowAssembly = false; // disable document assembly
        privilege.AllowFillIn   = true;  // enable form filling

        // Apply the privilege using the Facades API.
        PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath);
        bool result = security.SetPrivilege(privilege);

        if (!result)
        {
            Console.Error.WriteLine("Failed to set document privileges.");
        }
        else
        {
            Console.WriteLine($"Privileges set successfully. Output saved to '{outputPath}'.");
        }
    }
}