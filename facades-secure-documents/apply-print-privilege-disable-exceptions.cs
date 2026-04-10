using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize PdfFileSecurity with source and destination files
            using (PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath))
            {
                // Disable internal exception handling so that any error throws an exception
                fileSecurity.AllowExceptions = false;

                // Set the desired privilege (e.g., allow printing only)
                fileSecurity.SetPrivilege(userPassword, ownerPassword, DocumentPrivilege.Print);

                // SetPrivilege writes the result to outputPath; no additional Save call required
                Console.WriteLine($"Privilege applied successfully. Output saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            if (ex.InnerException != null)
                Console.Error.WriteLine($"Inner exception: {ex.InnerException.Message}");
        }
    }
}