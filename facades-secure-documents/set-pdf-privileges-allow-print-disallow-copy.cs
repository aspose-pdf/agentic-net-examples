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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a privilege that allows printing but disallows copying.
        DocumentPrivilege privilege = DocumentPrivilege.ForbidAll;
        privilege.AllowPrint = true;   // enable printing
        privilege.AllowCopy = false;   // disable copying

        // Initialize the facade with input and output files.
        using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
        {
            // Apply the privilege (no user/owner passwords needed).
            bool result = security.SetPrivilege(privilege);
            if (!result)
            {
                Console.Error.WriteLine("Failed to set document privileges.");
                return;
            }
            // The facade saves the output file automatically.
        }

        Console.WriteLine($"Document saved with updated privileges to '{outputPath}'.");
    }
}