using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source (protected) PDF and the destination PDF.
        const string inputPath  = "protected_input.pdf";
        const string outputPath = "reprotected_output.pdf";

        // Current owner password of the source PDF.
        const string currentOwnerPassword = "oldOwnerPass";

        // New passwords to be applied.
        const string newUserPassword = "newUserPass";
        const string newOwnerPassword = "newOwnerPass";

        try
        {
            // Use the parameter‑less constructor (the overload with destination is obsolete).
            PdfFileSecurity security = new PdfFileSecurity();

            // Bind the source PDF.
            security.BindPdf(inputPath);

            // Change the password while preserving the original security settings.
            bool changed = security.ChangePassword(
                ownerPassword:   currentOwnerPassword,
                newUserPassword: newUserPassword,
                newOwnerPassword:newOwnerPassword);

            if (changed)
            {
                // Save the re‑protected PDF to the desired output path.
                security.Save(outputPath);
                Console.WriteLine($"Password changed successfully. Output saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("Password change failed without exception (unexpected).");
            }
        }
        catch (Exception ex)
        {
            // General error handling (covers invalid password and other issues).
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
