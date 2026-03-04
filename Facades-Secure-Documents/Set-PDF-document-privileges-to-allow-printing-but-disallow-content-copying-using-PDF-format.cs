using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileSecurity and DocumentPrivilege

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

        // Create PdfFileSecurity facade with source and destination files
        using (PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath))
        {
            // Start from the predefined Print privilege (allows printing)
            DocumentPrivilege privilege = DocumentPrivilege.Print;

            // Explicitly ensure copying is disabled
            privilege.AllowCopy = false;

            // Apply the privilege settings; this writes the secured PDF to outputPath
            bool success = fileSecurity.SetPrivilege(privilege);
            if (!success)
            {
                Console.Error.WriteLine("Failed to set document privileges.");
            }
            else
            {
                Console.WriteLine($"Privileges applied: printing allowed, copying disallowed. Output saved to '{outputPath}'.");
            }
        }
    }
}