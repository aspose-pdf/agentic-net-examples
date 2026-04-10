using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the security facade with input and output files
        PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath);

        // Enable detailed exception capture (no exceptions will be thrown, they are stored in LastException)
        security.AllowExceptions = true;

        // Attempt encryption; returns true on success, false on failure
        bool success = security.TryEncryptFile(
            userPassword,
            ownerPassword,
            DocumentPrivilege.Print,
            KeySize.x256);

        if (success)
        {
            Console.WriteLine($"Encryption succeeded. Output saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Encryption failed.");
            Exception ex = security.LastException;
            if (ex != null)
            {
                LogException(ex);
            }
        }

        // Release resources
        security.Close();
    }

    // Recursively logs exception details, including inner exceptions
    static void LogException(Exception ex, int depth = 0)
    {
        string indent = new string(' ', depth * 2);
        Console.Error.WriteLine($"{indent}Exception Type: {ex.GetType().FullName}");
        Console.Error.WriteLine($"{indent}Message: {ex.Message}");
        if (ex.InnerException != null)
        {
            Console.Error.WriteLine($"{indent}Inner Exception:");
            LogException(ex.InnerException, depth + 1);
        }
    }
}