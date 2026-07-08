using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the facade with source and destination files
        using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
        {
            // Disable internal exception handling so that failures throw
            security.AllowExceptions = false;

            // Apply a privilege (e.g., allow printing only). This will throw on failure.
            security.SetPrivilege(userPassword, ownerPassword, DocumentPrivilege.Print);
        }

        Console.WriteLine($"Privilege applied and saved to '{outputPath}'.");
    }
}