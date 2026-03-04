using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // existing PDF
        const string outputPath = "output.pdf";  // PDF with new passwords
        const string currentOwnerPassword = "oldOwner";   // current owner password
        const string newUserPassword     = "newUser";    // new user password
        const string newOwnerPassword    = "newOwner";   // new owner password

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the facade
            using (PdfFileSecurity security = new PdfFileSecurity())
            {
                // Bind the source PDF (loads the file)
                security.BindPdf(inputPath);

                // Change the password; returns true on success
                bool success = security.ChangePassword(currentOwnerPassword, newUserPassword, newOwnerPassword);
                if (!success)
                {
                    Console.Error.WriteLine("Failed to change password.");
                    return;
                }

                // Save the PDF with the new security settings
                security.Save(outputPath);
            }

            Console.WriteLine($"Password changed successfully. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}