using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a privilege object that allows printing but disallows copying
        DocumentPrivilege privilege = DocumentPrivilege.AllowAll;
        privilege.AllowPrint = true;   // enable printing
        privilege.AllowCopy  = false;  // disable copying

        // Apply the privilege using PdfFileSecurity (facade API)
        using (PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath))
        {
            bool result = fileSecurity.SetPrivilege(privilege);
            if (!result)
            {
                Console.Error.WriteLine("Failed to set document privileges.");
                return;
            }
        }

        Console.WriteLine($"Document saved with updated privileges to '{outputPath}'.");
    }
}