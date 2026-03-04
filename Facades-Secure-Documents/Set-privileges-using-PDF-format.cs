using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_privileged.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the PdfFileSecurity facade with source and destination files
            PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

            // Choose a predefined privilege (e.g., allow printing only)
            DocumentPrivilege privilege = DocumentPrivilege.Print;

            // Apply the privilege; empty user/owner passwords are used,
            // the owner password will be generated automatically
            bool result = fileSecurity.SetPrivilege(privilege);

            if (result)
                Console.WriteLine($"Privileges applied successfully. Output saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Failed to apply privileges.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}