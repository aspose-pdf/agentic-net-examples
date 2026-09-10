using System;
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

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the facade with source and destination files
        PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

        // Disable internal exception handling so that errors are thrown
        fileSecurity.AllowExceptions = false;

        try
        {
            // Modify the privilege; this will throw if it fails
            fileSecurity.SetPrivilege(userPassword, ownerPassword, DocumentPrivilege.Print);
            Console.WriteLine("Privilege modification succeeded.");
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"PdfException: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}