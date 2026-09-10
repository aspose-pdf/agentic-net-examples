using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "protected_input.pdf";
        const string outputPath = "owner_updated_output.pdf";
        const string currentOwnerPassword = "oldOwnerPass";
        const string newOwnerPassword     = "newOwnerPass";

        // Verify input file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the facade and bind the source PDF
            PdfFileSecurity security = new PdfFileSecurity();
            security.BindPdf(inputPath);

            // Change only the owner password.
            // Pass null for newUserPassword to keep the existing user password unchanged.
            // The method preserves the original privilege settings.
            bool changed = security.ChangePassword(currentOwnerPassword, null, newOwnerPassword);
            if (!changed)
            {
                Console.Error.WriteLine("Failed to change the owner password.");
                return;
            }

            // Save the result to a new file
            security.Save(outputPath);
            Console.WriteLine($"Owner password updated successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}