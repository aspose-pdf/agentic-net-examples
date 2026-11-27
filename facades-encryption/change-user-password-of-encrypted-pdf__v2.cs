using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath      = "encrypted_input.pdf";
        const string outputPath     = "encrypted_output.pdf";
        const string oldUserPassword = "user123";
        const string ownerPassword   = "owner123";
        const string newUserPassword = "newUser123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the encrypted PDF
            PdfFileSecurity security = new PdfFileSecurity();
            security.BindPdf(inputPath);

            // Change only the user password; keep the owner password unchanged
            security.ChangePassword(oldUserPassword, ownerPassword, newUserPassword);

            // Save the updated PDF
            security.Save(outputPath);

            Console.WriteLine($"User password updated. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}