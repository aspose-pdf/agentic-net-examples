using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF (must be encrypted with an owner password)
        const string inputPath = "input.pdf";
        // Output PDF with the new passwords
        const string outputPath = "output.pdf";

        // Existing owner password of the source PDF
        const string oldOwnerPassword = "oldOwner";
        // New passwords to be set
        const string newUserPassword = "newUser";
        const string newOwnerPassword = "newOwner";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileSecurity works on input and output files; it implements IDisposable.
            using (PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath))
            {
                // Change both user and owner passwords in a single call.
                // Returns true on success.
                bool changed = fileSecurity.ChangePassword(oldOwnerPassword, newUserPassword, newOwnerPassword);

                if (changed)
                {
                    Console.WriteLine($"Passwords changed successfully. Output saved to '{outputPath}'.");
                }
                else
                {
                    Console.Error.WriteLine("Failed to change passwords.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}